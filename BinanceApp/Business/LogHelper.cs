using Observer.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceApp.Business
{
    public static class LogHelper
    {
        private static int appId;
        public static void Initialize(int applicationId)
        {
            appId = applicationId;
            var log4NetConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log4NetConfig.xml");
            Uri configAddress = new Uri(log4NetConfigPath);
            Logger.Initialize(configAddress);
        }
        public static void SendDebug(Object sender, string message, params KeyValuePair<string, string>[] keyValuePairs)
        {
            Logger.Send(sender.GetType(), Logger.CriticalityLevel.Debug, appId, 0, message,null, keyValuePairs);
        }
        public static void SendInfo(Object sender, string message, params KeyValuePair<string, string>[] keyValuePairs)
        {
            Logger.Send(sender.GetType(), Logger.CriticalityLevel.Info, appId, 0, message, null, keyValuePairs);
        }
        public static void SendWarn(Object sender, string message, Exception ex, params KeyValuePair<string, string>[] keyValuePairs)
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.AddRange(keyValuePairs);
            if(ex!=null && ex.StackTrace!=null)
            {
                pairs.Add( new KeyValuePair<string, string>("StackTrace", ex.StackTrace.ToString()));
            }
            Logger.Send(sender.GetType(), Logger.CriticalityLevel.Warn, appId, 0, message, ex, pairs.ToArray());
        }
        public static void SendError(Object sender, string message, Exception ex, params KeyValuePair<string, string>[] keyValuePairs)
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            pairs.AddRange(keyValuePairs);
            if (ex != null && ex.StackTrace != null)
            {
                pairs.Add(new KeyValuePair<string, string>("StackTrace", ex.StackTrace.ToString()));
            }
            Logger.Send(sender.GetType(), Logger.CriticalityLevel.Error, appId, 0, message, ex, pairs.ToArray());
        }
    }
}
