using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;
using File1 = System.IO.File;
using System.Net.Mime;
using System.IO.Compression;

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
            DirectoryInfo directoryInfo= new DirectoryInfo(FilePath);
            foreach(var file in directoryInfo.GetFiles("*",SearchOption.AllDirectories))
            {
                string CompressedFile = ZipFile(file);
                FileInfo fileInfo = new FileInfo(CompressedFile);
                var fileMadata = new File();
                fileMadata.Name = fileInfo.Name;
                fileMadata.Parents = new List<string> { "1G809CryOD4arL5wNtOdPm4w9N91chlW_" };
                string ContentType = "application / gzip";
                using (var Stream = new FileStream(fileInfo.FullName, FileMode.Open))
                {
                    request = driveService.Files.Create(fileMadata, Stream, ContentType);
                    request.Upload();
                }
                File1.Delete(fileInfo.FullName);
            }
        }

        public string  ZipFile(FileInfo file)
        {
            using (FileStream fileOrigin = file.OpenRead())
            {
                using (FileStream compressedFile = File1.Create($"{file.FullName}1.gz"))
                {
                    using (GZipStream zipStream = new GZipStream(compressedFile, CompressionLevel.Optimal))
                    {
                        fileOrigin.CopyTo(zipStream);
                    }

                }
            }
            return $"{file.FullName}1.gz";
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
