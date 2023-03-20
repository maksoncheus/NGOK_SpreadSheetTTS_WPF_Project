using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System.IO;

namespace NGOK_SpreadSheetTTS_WPF_Project.Models
{
    public static class Initializaton
    {
        public static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        public static readonly string ApplicationName = "NGOK-SPREADSHEET";
        public static string sheet = "1 курс";
        public static readonly string SpreadsheetId = "1FiMov0r4UUDKT6A56NWMImpoUakDC2YDevgaOpJQ7Qc";
        public static SheetsService service;
        public static void Init()
        {
            GoogleCredential credential;
            using (var stream = new FileStream("spreadsheet_credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
    }
}
