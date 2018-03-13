using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBook.Common
{
    public class Logger
    {

        public enum LogCategoryEnum
        {
            Information,
            /// <summary>
            ///可以只記Error , Warning 讓log檔少一點 
            /// </summary>
            Error,
            Warning
        }

        static log4net.ILog log4netInstance;
        public Logger()
        {


        }
        public static void Write(LogCategoryEnum logCatogroy, string context)
        {
            log4netInstance = log4net.LogManager.GetLogger("Looger");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Common.ConfigTool.GetAppsetting("log4netConfPath")));
            switch (logCatogroy)
            {
                case LogCategoryEnum.Information:
                    log4netInstance.Info(context);
                    break;
                case LogCategoryEnum.Error:
                    log4netInstance.Error(context);
                    break;
                case LogCategoryEnum.Warning:
                    log4netInstance.Warn(context);
                    break;
                default:
                    break;
            }

        }
    }
}
