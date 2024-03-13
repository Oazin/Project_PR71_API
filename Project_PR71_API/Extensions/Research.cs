namespace Project_PR71_API.Extensions
{
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using LinqKit;

    public static class Research
    {

        public static IQueryable<TEntity> CreateResearch<TEntity>(this IQueryable<TEntity> source, string searchTerms) 
        {
            if (searchTerms != "")
            {
                Expression<Func<TEntity, bool>> query = PredicateBuilder.New<TEntity>();

                char[] parentheses = { '(', ')' };
                IList<int> parentheseIndexes = new List<int>();
                for (int i = searchTerms.IndexOfAny(parentheses); i > -1; i = searchTerms.IndexOfAny(parentheses, i + 1))
                {
                    parentheseIndexes.Add(i);
                }

                if (parentheseIndexes.Count > 0)
                {
                    query = aux(query, parentheseIndexes, searchTerms);
                }
                else
                {
                    query = BuildRequest<TEntity>(searchTerms);
                }
                return source.Where(query);

            }
            else return source;
        }

        /// <summary>
        /// Builds AND and OR expression
        /// </summary>
        /// <param name="searchTerms">The searched terms</param>
        /// <returns>The filter definition</returns>
        private static Expression<Func<TEntity, bool>> BuildRequest<TEntity>(string searchTerms)
        {
            ExpressionStarter<TEntity> expression = PredicateBuilder.New<TEntity>();

            // Split the searchTerms by '&' and '|' and keep the operators
            Regex exp = new Regex("([&\\|]?[^&\\|]+)", RegexOptions.None, TimeSpan.FromMilliseconds(10000));
            MatchCollection matches = exp.Matches(searchTerms);
            foreach (Match m in matches)
            {
                string value = m.Value.Trim();
                if (value.Contains('&'))
                {
                    value = value.Remove(0, 1).Trim();
                    expression = expression.And(BuildExpressions<TEntity>(value));
                }
                else if (value.Contains('|'))
                {
                    value = value.Remove(0, 1).Trim();
                    expression = expression.Or(BuildExpressions<TEntity>(value));
                }
                else
                {
                    expression = expression.Start(BuildExpressions<TEntity>(value));
                }
            }

            return expression;
        }

        private static Expression<Func<TEntity, bool>> aux<TEntity>(Expression<Func<TEntity, bool>> queryExpression, IList<int> parentheseIndexes, string searchTerms)
        {
            char operation = ' ';
            string expression;
            bool getOperator = false;

            // Retrieve the expression part before the first parenthese
            if (parentheseIndexes[0] > 0)
            {
                expression = searchTerms.Substring(0, parentheseIndexes[0]).Trim();

                // Retrieve the latest caractere
                operation = expression[^1];
                if (operation == '&' || operation == '|')
                {
                    // Remove the two latest caractere from the expression
                    expression = expression[0..^2];
                }

                queryExpression = BuildRequest<TEntity>(expression);
                getOperator = false;
            }
            // Loop on the parentheses
            int i;
            int closeIndex = 0;
            for (i = 0; i < parentheseIndexes.Count; i += 2)
            {
                int openIndex = parentheseIndexes[i];
                closeIndex = parentheseIndexes[i + 1];
                expression = searchTerms.Substring(openIndex + 1, closeIndex - openIndex - 1);

                // Need to get the operator
                if (getOperator)
                {
                    operation = searchTerms[openIndex - 2];
                }

                if (operation == '&')
                {
                    // Concatenate the existing query expression with the method 'And' 
                    queryExpression = queryExpression.And(BuildRequest<TEntity>(expression));
                }
                else if (operation == '|')
                {
                    // Concatenate the existing query expression with the method 'Or'
                    queryExpression = queryExpression.Or(BuildRequest<TEntity>(expression));
                }
                else
                {
                    // Initialise the query expression
                    queryExpression = BuildRequest<TEntity>(expression);
                    getOperator = true;
                }
            }

            // Retrieve the expression part after the latest parenthese
            if (closeIndex != 0 && closeIndex < (searchTerms.Length - 1))
            {
                operation = searchTerms[closeIndex + 2];
                expression = searchTerms.Substring(closeIndex + 3, searchTerms.Length - closeIndex - 3).Trim();
                if (operation == '&')
                {
                    // Concatenate the existing query expression with the method 'And'
                    queryExpression = queryExpression.And(BuildRequest<TEntity>(expression));
                }
                else if (operation == '|')
                {
                    // Concatenate the existing query expression with the method 'Or'
                    queryExpression = queryExpression.Or(BuildRequest<TEntity>(expression));
                }
            }
            return queryExpression;
        }

        private static Expression<Func<TEntity, bool>> BuildExpressions<TEntity>(string searchTerm, bool caseSensitive = false)
        {
            ConstantExpression constantExpression;
            MethodInfo methodInfo;
            Expression methodCallExpression;

            // Start the lambda expression by declaring the parameter
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "e");
            string[] keyvalue = searchTerm.Split(' ');
            string columnName = keyvalue[0];
            string operation = keyvalue[1];
            object value;

            // https://stackoverflow.com/a/278702/2045161
            Expression memberExpression = BuildMemberExpression(columnName, parameterExpression);

            if ("null".Equals(keyvalue[2].ToLower()))
            {
                constantExpression = Expression.Constant(null);
                switch (operation)
                {
                    case "!=":
                        methodCallExpression = Expression.NotEqual(memberExpression, constantExpression);
                        break;
                    default:
                        methodCallExpression = Expression.Equal(memberExpression, constantExpression);
                        break;
                }
            }
            else
            {
                switch (memberExpression.Type.Name)
                {
                    case "Int32":
                        value = int.Parse(keyvalue[2]);

                        // Cast the value to int and store it inside a constant
                        constantExpression = Expression.Constant(value, typeof(int));
                        break;
                    case "Boolean":
                        value = "true".Equals(keyvalue[2]);

                        // Cast the value to bool and store it inside a constant
                        constantExpression = Expression.Constant(value, typeof(bool));
                        break;
                    case "DateTime":
                        value = DateTime.Parse(keyvalue[2], new CultureInfo("en-US"));

                        // Cast the value to DateTime and store it inside a constant
                        constantExpression = Expression.Constant(value, typeof(DateTime));
                        break;
                    default:
                        value = keyvalue[2].Replace("#_#", " ");

                        // Cast the value to string and store it inside a constant
                        constantExpression = Expression.Constant(value, typeof(string));

                        // Take into acount the case sensitivity 
                        if (!caseSensitive)
                        {
                            // Find the ToLower method from the member expression type
                            methodInfo = memberExpression.Type.GetMethod("ToLower", Array.Empty<Type>());
                            memberExpression = Expression.Call(memberExpression, methodInfo);
                        }

                        break;
                }
                switch (operation)
                {
                    case "=":
                        // Find the Equals method from the member expression type which takes the memberExpression type as parameter 
                        methodInfo = memberExpression.Type.GetMethod("Equals", new[] { memberExpression.Type });
                        break;
                    case "<=":
                    case "=<":
                        // Find the LessThanOrEqual method from the expression type which takes two parameters. Their type must be of Expression type
                        methodInfo = typeof(Expression).GetMethod("LessThanOrEqual", new[] { typeof(Expression), typeof(Expression) });
                        break;
                    case "<":
                        // Find the LessThan method from the expression type which takes two parameters. Their type must be of Expression type
                        methodInfo = typeof(Expression).GetMethod("LessThan", new[] { typeof(Expression), typeof(Expression) });
                        break;
                    case ">=":
                    case "=>":
                        // Find the GreaterThanOrEqual method from the expression type which takes two parameters. Their type must be of Expression type
                        methodInfo = typeof(Expression).GetMethod("GreaterThanOrEqual", new[] { typeof(Expression), typeof(Expression) });
                        break;
                    case ">":
                        // Find the GreaterThan method from the expression type which takes two parameters. Their type must be of Expression type
                        methodInfo = typeof(Expression).GetMethod("GreaterThan", new[] { typeof(Expression), typeof(Expression) });
                        break;
                    default:
                        // Find the Contains method from the expression type which takes two parameters. Their type must be of Expression type
                        methodInfo = memberExpression.Type.GetMethod("Contains", new[] { memberExpression.Type });
                        break;
                }
                // Create the method to call using the specified parameters
                if (methodInfo.IsStatic)
                {
                    methodCallExpression = methodInfo.Invoke(null, new[] { memberExpression, constantExpression }) as Expression;
                }
                else
                {
                    methodCallExpression = Expression.Call(memberExpression, methodInfo, constantExpression);
                }
            }

            // Build the final lambda expression: e => e.ColonneName.Method(value)
            Expression<Func<TEntity, bool>> condition = Expression.Lambda<Func<TEntity, bool>>(methodCallExpression, parameterExpression);
            return condition;
        }

        private static Expression BuildMemberExpression(string columnName, ParameterExpression parameterExpression)
        {
            Expression memberExpression = parameterExpression;
            foreach (string member in columnName.Split('.'))
            {
                memberExpression = Expression.PropertyOrField(memberExpression, member);
            }

            return memberExpression;
        }
    }
}
