using Microsoft.AspNetCore.Mvc;
using NativeAppApi.Models;

namespace NativeAppApi.Controllers
{
    [ApiController]
    [Route("api/Image")]
    public class ImageController : Controller
    {
        private readonly ImageManager _imageManager;
        public ImageController()
        {
            _imageManager = new ImageManager();
        }

        [HttpPost]
        public IActionResult SavePicture([FromQuery]string imageString)
        {
            if (imageString == null || imageString == "null")
                return Content("Problem saving image, try again.");
            _imageManager.SavePicture(imageString);
            return Content("Image successfully saved");
        }

        [HttpGet]
        public ImageResponse GetAll()
        {
            var result = _imageManager.GetAll();
            return result;
        }
    }
}

