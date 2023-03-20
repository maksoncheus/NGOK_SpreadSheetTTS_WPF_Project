using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;

namespace NGOK_SpreadSheetTTS_WPF_Project.Models
{
    public static class SpreadSheetModel
    {
        public static (string, DataTable) GetSheet(int groupID)
        {
            groupID += 3;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            //string range = $"{Initializaton.sheet}!Z:AK";
            string range = date.DayOfWeek switch
            {
                DayOfWeek.Monday => $"{Initializaton.sheet}!B:M",
                DayOfWeek.Tuesday => $"{Initializaton.sheet}!N:Y",
                DayOfWeek.Wednesday => $"{Initializaton.sheet}!Z:AK",
                DayOfWeek.Thursday => $"{Initializaton.sheet}!AL:AW",
                DayOfWeek.Friday => $"{Initializaton.sheet}!AX:BI",
                DayOfWeek.Saturday => $"{Initializaton.sheet}!BJ:BU",
                _ => ""

            };
            if (String.IsNullOrEmpty(range))
            {
                Console.WriteLine("Похоже, сегодня выходной...");
                return ("", null);
            }
            else
            {
                DataTable table = new DataTable();
                table.Columns.Add("col1");
                table.Columns.Add("col2");
                table.Columns.Add("col3");
                table.Columns.Add("col4");
                table.Columns.Add("col5");
                table.Columns.Add("col6");
                table.Columns.Add("col7");
                table.Columns.Add("col8");
                table.Columns.Add("col9");
                table.Columns.Add("col10");
                table.Columns.Add("col11");
                table.Columns.Add("col12");
                string tempString = "";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        Initializaton.service.Spreadsheets.Values.Get(Initializaton.SpreadsheetId, range);
                var response = request.Execute();
                IList<IList<object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                    for(int i = 0; i < 12; i++)
                    {
                        if (i >= values[groupID].Count)
                            tempString += ' ';
                        else
                            tempString+=values[groupID][i].ToString();
                        if(i !=11)tempString+= "|";
                    }
                }
                table.Rows.Add(tempString.Split('|'));
                return ("", table);
            }
        }
        public static ObservableCollection<string> GetGroups()
        {
            ObservableCollection<string> groups = new ObservableCollection<string>();
            string range = $"{Initializaton.sheet}!A:A";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                        Initializaton.service.Spreadsheets.Values.Get(Initializaton.SpreadsheetId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                for (int i = 3; i < values.Count; i++)
                {
                    groups.Add(values[i][0].ToString());
                }
            }
            return groups;
        }
    }
}
