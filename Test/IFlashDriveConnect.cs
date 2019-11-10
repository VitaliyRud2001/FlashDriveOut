using System;
using System.IO;
using Google.Apis.Drive.v3;

namespace Test
{
    public interface IFlashDriveConnect
    {
        string[] GetAllFilesFromFolder(string FilePath);
        string GetFileName(string FilePath);
        string ZipFile(FileInfo file);
        void SendFilesOnGoogleDrive(string FilePath, DriveService driveService);
    }
}
