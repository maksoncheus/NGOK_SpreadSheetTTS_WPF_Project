using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace NGOK_SpreadSheetTTS_WPF_Project.Models
{
    public static class SpreadSheetModel
    {
        public static (string, DataTable) GetSheet(int groupID)
        {
            groupID += 3;
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            //string range = $"{Initializaton.sheet}!BJ:BU";
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
            string weekDay = date.DayOfWeek switch
            {
                DayOfWeek.Monday => "Понедельник",
                DayOfWeek.Tuesday => "Вторник",
                DayOfWeek.Wednesday => "Среда",
                DayOfWeek.Thursday => "Четверг",
                DayOfWeek.Friday => "Пятница",
                DayOfWeek.Saturday => "Суббота",
                DayOfWeek.Sunday => "Воскресенье",
                _ => ""

            };
            DataTable table = new DataTable();
            for (int iterator = 1; iterator <= 12; iterator++)
                table.Columns.Add("col" + iterator.ToString());
            if (String.IsNullOrEmpty(range))
            {
                Console.WriteLine("Похоже, сегодня выходной...");
                return ("", table);
            }
            else
            {
                string tempString = "";
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        Initializaton.service.Spreadsheets.Values.Get(Initializaton.SpreadsheetId, range);
                var response = request.Execute();
                IList<IList<object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                    if (values.Count > groupID)
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
                return (weekDay, table);
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
