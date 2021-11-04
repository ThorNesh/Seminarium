using System.IO;

namespace WarsztatAPI.Tools
{
    public static class Logger
    {
        public static void WriteLog(string file,string title, string message)
        {
            DirectoryInfo dirInfo = new DirectoryInfo("Logs");
            if (!dirInfo.Exists) dirInfo.Create();
            using(StreamWriter sw = new StreamWriter($"Logs/{file}",true))
            {
                string newLine = $"================================{title}================================";
                sw.WriteLine(newLine);
                sw.WriteLine(message);
            }
        }
    }
}
