using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;
using System.Net.Mime;

namespace Test
{
    public class FlashDriveConnect : IFlashDriveConnect
    {
        public FlashDriveConnect()
        {
        }

       
        public string[] GetAllFilesFromFolder(string FilePath)
        {
           return Directory.GetFiles(FilePath, "*", SearchOption.AllDirectories);
        }

        public string GetFileName(string FilePath)
        {
            FileInfo fileInfo = new FileInfo(FilePath);
            return fileInfo.Name;
        }

        public void SendFilesOnGoogleDrive(string FilePath, DriveService driveService)
        {
            String[] files = GetAllFilesFromFolder(FilePath);
            FilesResource.CreateMediaUpload request;
            foreach(var File in files)
            {
                var fileMadata = new File();
                fileMadata.Name = GetFileName(File);
                fileMadata.Parents = new List<string> { "1G809CryOD4arL5wNtOdPm4w9N91chlW_" };
                string ContentType = GetMimeType(File);
                using(var Stream = new FileStream(File, FileMode.Open))
                {
                    request = driveService.Files.Create(fileMadata, Stream,ContentType);
                    request.Upload();
                }
            }
        }

        public void ZipFiles(string FilePath)
        {
            throw new NotImplementedException();
        }

        public string GetMimeType(string FilePath)
        {
            FileInfo info = new FileInfo(FilePath);
            
            switch(info.Extension)
            {
                case ".avi":
                        return "";
                case ".bmp":
                    return "image/bmp";

                case ".csv":
                    return "text/csv";

                case ".doc":
                    return "application/msword";

                case ".gif":
                    return "";

                case ".htm":
                    return "application/html";

                case ".html":
                    return "application/html";

                case ".jpg":
                    return "image/jpeg";

                case ".jpeg":
                    return "image/jpeg";

                case ".zip":
                    return "application/zip";
            }
            return "";
        }

    }
}
