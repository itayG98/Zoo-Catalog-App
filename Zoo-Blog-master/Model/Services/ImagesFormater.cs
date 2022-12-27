using Microsoft.AspNetCore.Http;

namespace Model.Services
{
    public static class ImagesFormater
    {
        public static byte[] ImageToBytesArrayFromLocalPath(string path)
        {
            if (path != null)
            {
                byte[] imagesFileData = Array.Empty<byte>();
                try { imagesFileData = File.ReadAllBytes(path); }
                catch (Exception e) {}
                return imagesFileData;
            }
            return default;
        }
        public static byte[] MemoryStreamToBytesArrayConverter(MemoryStream stream)
        {
            if (stream != null)
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return bytes;
            }
            return default;
        }

        public static byte[] FormFileToByteArray(FormFile formFile)
        {
            if (formFile != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                formFile.OpenReadStream().CopyTo(memoryStream);
                byte[] rawData= memoryStream.ToArray();
                return rawData;
            }
            return default;
        }

        public static byte[] IFormFileToByteArray(IFormFile formFile)
        {
            if (formFile != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                formFile.OpenReadStream().CopyTo(memoryStream);
                byte[] rawData = memoryStream.ToArray();
                return rawData;
            }
            return default;
        }

        public static FormFile GetFormFileFromPath(string path)
        {
            FormFile formFile;
            using (var stream = File.OpenRead(path))
            {
                formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };
            }
            return formFile;
        }

        public static Stream ImageToStreamFromLocalPath(string path)
        {
            Stream imagesFileData;
            try
            { return imagesFileData = File.OpenRead(path); }
            catch (Exception) { }
            return default;
        }

        public static MemoryStream BytesArrayToMemoryStreamConverter(byte[] imagesFileData)
        {
            if (imagesFileData != null)
                return new(imagesFileData); ;
            return default;
        }

        public static string FormatRawDataToImage(byte[] imagesFileData)
        {
            if (imagesFileData != null)
                return "data:image;base64," + Convert.ToBase64String(imagesFileData);
            return default;
        }

    }
}
