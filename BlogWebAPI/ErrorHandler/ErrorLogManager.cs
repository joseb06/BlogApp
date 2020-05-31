using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace BlogWebAPI
{
    public class ErrorLogManager
    {
        public void SaveError(object obj, Exception ex)
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string path = HttpContext.Current.Request.MapPath("~/log/" + date + ".txt");

            StreamWriter sw = new StreamWriter(path, true);

            StackTrace stacktrace = new StackTrace();
            sw.WriteLine(obj.GetType().FullName + " " + time);
            sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message);
            sw.WriteLine("");

            sw.Flush();
            sw.Close();
        }
    }
}