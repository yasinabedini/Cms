using Microsoft.AspNetCore.Http;
using System.IO.Pipelines;
using System.Net;

namespace FileManager.Tools;

public static class FileTools
{
    public static void SaveImage(IFormFile profileImage, string imageName, string whichFolder, bool thumbSave)
    {
        string imagePath = imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\{whichFolder}", imageName);

        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            profileImage.CopyTo(stream);
        }

        ////save ThumbNail
        //if (thumbSave)
        //{
        //    string thumbImagePath = Path.Combine(Directory.GetCurrentDirectory().Replace("Application\\Cmd.Application\\Tools\\", "EndPoints\\Cms.Endpoints.Site"), $"wwwroot\\img\\{whichFolder}\\Thumb", imageName);
        //    ImageConvertor imageConvertor = new ImageConvertor();
        //    imageConvertor.Image_resize(imagePath, thumbImagePath, 213);
        //}
    }
    public static bool DeleteFile(string folder, string imageName)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\{folder}", imageName);
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        else
        {
            return false;
        }
    }
}
