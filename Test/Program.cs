using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;
namespace Test
{
    class MainClass
    {
        private static string[] Scopes = { DriveService.Scope.Drive };
        private static String ApplicationName = "FlashDriveConnect";





        public static void Main(string[] args)
        {
            DriveInfo[] drivesOnStart = DriveInfo.GetDrives();
            bool deviceIsInput = false;

            DriveService service = GoogleDriveConnect.GetDriveService();
            FlashDriveConnect flashDriveConnect = new FlashDriveConnect();
            while (true)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                if (drives.Length != drivesOnStart.Length && deviceIsInput == false)
                {
                    deviceIsInput = true;
                    DriveInfo last_driver = DriverClass.GetLastAddedDriver(drives,drivesOnStart);
                    Console.WriteLine("Drive found! Name:{0}", last_driver!=null ? $"{last_driver.Name}":"Empty!");
                    flashDriveConnect.SendFilesOnGoogleDrive(last_driver.Name, service);
                    Console.WriteLine("Files sent!");
                    drivesOnStart = drives;
                }
                if (drives.Length < drivesOnStart.Length && deviceIsInput == true)
                {
                    deviceIsInput = false;
                    Console.WriteLine("Device output!");
                    drivesOnStart = drives;
                }
                Thread.Sleep(3000);



            }

        }



    }
}

