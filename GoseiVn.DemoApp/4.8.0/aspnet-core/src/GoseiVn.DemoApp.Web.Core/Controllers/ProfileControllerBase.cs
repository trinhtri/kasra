using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using GoseiVn.DemoApp.Controllers.Dto;
using GoseiVn.DemoApp.Estimates.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Abp.Web.Models;
using Microsoft.AspNetCore.Http;

namespace GoseiVn.DemoApp.Controllers
{
   public class ProfileControllerBase: DemoAppControllerBase
    {
        private readonly IAppFolders _appFolders;
        private const int MaxProfilePictureSize = 5242880; //5MB
        private readonly string[] ValidFileTypes = { "image/jpeg", "image/png" };

        protected ProfileControllerBase(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        public List<UploadProfilePictureOutput> UploadProfilePicture()
        {
            //try
            //{ 
            var result = new List<UploadProfilePictureOutput>();
            foreach (var profilePictureFile in Request.Form.Files)
            {
                if (profilePictureFile == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error_Message"), L("ProfilePicture_Change_Error_Details"));
                }

                if (profilePictureFile.Length > MaxProfilePictureSize)
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit_Message"), L("File_Warn_SizeLimit_Details", 5));
                }

                if (!ValidFileTypes.Contains(profilePictureFile.ContentType))
                {
                    throw new UserFriendlyException(L("File_Warn_FileType_Message"), L("ProfilePicture_Warn_FileType_Details"));
                }

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp profile pictures
                //AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "userProfileImage_" + AbpSession.GetUserId());

                //Save new picture
                var fileInfo = new FileInfo(profilePictureFile.FileName);
                //var tempFileName = "image_" + Guid.NewGuid() + fileInfo.Extension;
                var tempFileName = profilePictureFile.FileName;
                var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
                System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                using (var bmpImage = new Bitmap(tempFilePath))
                {
                    var item = new UploadProfilePictureOutput
                    {
                        FileName = tempFileName,
                        Size = profilePictureFile.Length/1024
                    };
                    result.Add(item);
                }
            }

            return result;
            //var profilePictureFile = Request.Form.Files.First();


            //}
            //catch (UserFriendlyException ex)
            //{
            //    return new UploadProfilePictureOutput
            //    {
            //        ErrorInfo = new ErrorInfo(ex.Message, ex.Details)
            //    };
            //}
        }

        public ActionResult DownloadTempFile(FileDto file)
        {
            var filePath = Path.Combine(_appFolders.TempFileDownloadFolder, file.FileToken);
            if (!System.IO.File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            return File(fileBytes, file.FileType, file.FileName);
        }
    }
}
