using Project_PR71_API.Models.ViewModel;

namespace Project_PR71_API.Services.IServices
{
    public interface ISavePostService
    {
        public bool AddSavePost(string email, int idPost);

        public bool HadSaved(string email, int idPost);
        
        public bool DeleteSavePost(string email, int idPost);

        public ICollection<SavePostViewModel>? GetSavePostByEmail(string email);
    }
}