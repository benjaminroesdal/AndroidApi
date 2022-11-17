using System.Drawing.Imaging;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace NativeAppApi
{
    public class ImageManager
    {
        public void SavePicture(string imageString)
        {
            string filePath = $"Images/{RndNumber()}.jpg";
            var formattedString = imageString.Replace(" ", "+");
            File.WriteAllBytes(filePath, Convert.FromBase64String(formattedString));
        }

        public ImageReponse GetAll()
        {
            var imageResp = new ImageReponse();
            imageResp.Images = new List<Image>();
            var pictures = Directory.GetFiles("Images", "*.*", SearchOption.AllDirectories).ToList();
            foreach (var picture in pictures)
            {
                var byteArray = ReadPicture(picture);
                var tempString = Convert.ToBase64String(byteArray);
                imageResp.Images.Add(new Image { ImageBase64= tempString });
            }
            return imageResp;
        }

        public byte[] ReadPicture(string fileName)
        {
            // Load file meta data with FileInfo
            FileInfo fileInfo = new FileInfo(fileName);

            // The byte[] to save the data in
            byte[] data = new byte[fileInfo.Length];

            // Load a filestream and put its content into the byte[]
            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }

            // Post byte[] to database
            return data;
        }



        public string ImageToBase64()
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(@"IMG_2003.png");
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        public int RndNumber()
        {
            Random rnd = new Random();
            var randomNum = rnd.Next(1, 1000);
            return randomNum;
        }
    }
}

public class ImageReponse
{
    public List<Image> Images { get; set; }
}

public class Image
{
    public string ImageBase64 { get; set; }
}
