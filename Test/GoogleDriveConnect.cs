using System;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using File = Google.Apis.Drive.v3.Data.File;
namespace Test
{
    public class GoogleDriveConnect
    {
        private static string[] Scopes = { DriveService.Scope.Drive };
        private static String ApplicationName = "FlashDriveConnect";

        private static UserCredential GetCredential()
        {
            using (var Stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string creadPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                creadPath = Path.Combine(creadPath, "driveApiCredentials", "drive-credentials.json");
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(Stream).Secrets,
                    Scopes,
                    "User",
                    CancellationToken.None,
                    new FileDataStore(creadPath, true)
                    ).Result;
            }

        }
        public static DriveService GetDriveService()
        {
            UserCredential credential = GetCredential();
            return new DriveService(
                new Google.Apis.Services.BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                }
                );
        }

    }
}
