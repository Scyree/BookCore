using Microsoft.AspNetCore.Http;

namespace ExploreBooks.Models.ManageViewModels
{
    public class ChangePictureViewModel
    {
        public string Folder { get; set; }

        public string ImageName { get; set; }

        public IFormFile Image { get; set; }

        public string StatusMessage { get; set; }
    }
}
