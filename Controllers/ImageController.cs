using Microsoft.AspNetCore.Mvc;

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
        public ImageReponse GetAll()
        {
            var result = _imageManager.GetAll();
            return result;
        }
    }
}

public class testDto
{
    public string imageString { get; set; }
}
