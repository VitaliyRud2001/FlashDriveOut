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
            DriveService service = GoogleDriveConnect.GetDriveService();
            FlashDriveConnect flashDriveConnect = new FlashDriveConnect();
            flashDriveConnect.SendFilesOnGoogleDrive("/home/vitaliyrud/Downloads/", service);
            Console.WriteLine("done!");
        }
       
    

    }
}
