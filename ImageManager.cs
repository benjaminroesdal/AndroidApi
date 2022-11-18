using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using NativeAppApi.Models;

namespace NativeAppApi
{
    public class ImageManager
    {
        /// <summary>
        /// Saves picture to local folder with random int name.
        /// </summary>
        /// <param name="imageString"></param>
        public void SavePicture(string imageString)
        {
            string filePath = $"Images/{RndNumber()}.jpg";
            //Replacing spaces with + -- this was an issue when posting base64 as querystring, it would sometimes newline which would create a space, and invalidated the data as base64.
            var formattedString = imageString.Replace(" ", "+");
            File.WriteAllBytes(filePath, Convert.FromBase64String(formattedString));
        }

        /// <summary>
        /// Reads all images in images folder locally. 
        /// </summary>
        /// <returns>ImageResponse object with array of images.</returns>
        public ImageResponse GetAll()
        {
            var imageResp = new ImageResponse();
            imageResp.Images = new List<ImageObj>();
            var pictures = Directory.GetFiles("Images", "*.*", SearchOption.AllDirectories).ToList();
            foreach (var picture in pictures)
            {
                var byteArray = ReadPicture(picture);
                var tempString = Convert.ToBase64String(byteArray);
                imageResp.Images.Add(new ImageObj() { ImageBase64= tempString });
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

        public int RndNumber()
        {
            Random rnd = new Random();
            var randomNum = rnd.Next(1, 1000);
            return randomNum;
        }
    }
}
