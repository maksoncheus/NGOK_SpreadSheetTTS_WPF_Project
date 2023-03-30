using System.Data;
using System.Speech.Synthesis;

namespace NGOK_SpreadSheetTTS_WPF_Project.Models
{
    public static class SpeechModel
    {
        static SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        public static void Speech(string group, DataTable dataTable)
        {
            static string GetCounter(int cnt)
            {
                return cnt switch
                {
                    1 => "Первая",
                    2 => "Вторая",
                    3 => "Третья",
                    4 => "Четвёртая",
                    5 => "Пятая",
                    6 => "Шестая"
                };
            }
            synthesizer.Pause();
            synthesizer.SpeakAsyncCancelAll();
            synthesizer.Resume();
            synthesizer.SpeakAsync($"Группа {group}");
            if (dataTable.Rows.Count > 0)
                for (int i = 0; i < 12; i+=2)
                {
                    synthesizer.SpeakAsync($"{GetCounter(i / 2 + 1)} пара:");
                    synthesizer.SpeakAsync(
                        (dataTable.Rows[0][i].ToString() == " " && dataTable.Rows[0][i+1].ToString() == " ")
                        ? "Пары нет"
                        : $"Предмет: {dataTable.Rows[0][i].ToString()}, Аудитория: {dataTable.Rows[0][i + 1].ToString()}"
                    );
                }
            else synthesizer.SpeakAsync("Пар нет");
        }
    }
}
