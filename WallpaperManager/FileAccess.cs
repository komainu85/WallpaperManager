using MikeRobbins.WallpaperManager.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using MikeRobbins.WallpaperManager.Contracts;
using Sitecore.Diagnostics;

namespace MikeRobbins.WallpaperManager
{
    public class FileAccess : IFileAccess
    {
        private const string _wallpaperDirectory = @"sitecore\shell\Themes\Backgrounds";

        private readonly IDataAccess _iDataAccess;
        private readonly IImageRepository _imageRepository;

        public FileAccess(IDataAccess iDataAccess, IImageRepository imageRepository)
        {
            _iDataAccess = iDataAccess;
            _imageRepository = imageRepository;
        }

        public FileInfo[] GetFiles()
        {
            return GetWallpaperDirectory().GetFiles();
        }

        public FileInfo GetFile(string fileName)
        {
            return GetWallpaperDirectory().GetFiles().FirstOrDefault(x => x.Name == fileName);
        }

        public void CreateFile(Wallpaper wallpaper)
        {
            var mediaItem = _iDataAccess.GetMediaItem(wallpaper.itemId);

            Image image = Image.FromStream(mediaItem.GetMediaStream());

            Image resizedImage = _imageRepository.ResizeImage(image);

            var mediaStream = _imageRepository.CovertImageToStream(resizedImage, mediaItem);

            try
            {
                using (var fileStream = File.Create(GetWallpaperDirectory() + "\\" + mediaItem.DisplayName + "." + mediaItem.Extension))
                {
                    mediaStream.Seek(0, SeekOrigin.Begin);
                    mediaStream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error(ex.Message, this);
                throw ex;
            }

        }

        public void DeleteFile(Wallpaper wallpaper)
        {
            var fileToDelete = GetFile(wallpaper.itemId);

            Assert.IsNotNull(fileToDelete, "Wallpaper not found");

            try
            {
                fileToDelete.Delete();
            }
            catch (IOException ioException)
            {
                throw ioException;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DirectoryInfo GetWallpaperDirectory()
        {
            var directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(@"\") + _wallpaperDirectory);
            return directory;
        }
    }
}