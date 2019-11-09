using System;
using Google.Apis.Drive.v3;

namespace Test
{
    public interface IFlashDriveConnect
    {
        string[] GetAllFilesFromFolder(string FilePath);
        string GetFileName(string FilePath);
        void ZipFiles(string FilePath);
        void SendFilesOnGoogleDrive(string FilePath, DriveService driveService);
    }
}
