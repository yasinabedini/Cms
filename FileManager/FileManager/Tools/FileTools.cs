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

        //save ThumbNail
        if (thumbSave)
        {
            string thumbImagePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\thumb", imageName));
            ImageConvertor imageConvertor = new ImageConvertor();
            imageConvertor.Image_resize(imagePath, thumbImagePath, 250);
        }
    }

    public static void SaveGalleryImageThumb(string imageName)
    {
        string imagePath = imagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\attachment\\image", imageName);
     
        string thumbImagePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\img\\thumb", imageName));
        ImageConvertor imageConvertor = new ImageConvertor();
        imageConvertor.Image_resize(imagePath, thumbImagePath, 250);
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
