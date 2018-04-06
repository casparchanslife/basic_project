using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace Learn.Log
{
    public class LoggingUtil
    {        
        private string _category = String.Empty;
        private static int _EventID = -1;
        private enum LogPriority { Verbose = 2, Information = 4, Warning = 6, Error = 8 };
        private static Dictionary<string, LoggingUtil> _loggerList = new Dictionary<string, LoggingUtil>();

        private LoggingUtil(string category)
        {
            this._category = category;
        }

        public static LoggingUtil GetLogger()
        {
            return GetLogger(String.Empty);
        }

        public static LoggingUtil GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        public static LoggingUtil GetLogger(string category)
        {
            if (!_loggerList.ContainsKey(category))
            {
                _loggerList.Add(category, new LoggingUtil(category));
            }
            return _loggerList[category];
        }

        public void Debug()
        {
            Log(2, TraceEventType.Verbose, LogPriority.Verbose, _category, null, null);
        }
        public void Debug(object message)
        {
            Log(2, TraceEventType.Verbose, LogPriority.Verbose, _category, message, null);
        }
        public void Debug(object message, Exception ex)
        {
            Log(2, TraceEventType.Verbose, LogPriority.Verbose, _category, message, ex);
        }
        public void Debug(int tid, object message)
        {
            Log(2, TraceEventType.Verbose, LogPriority.Verbose, _category, "TID(" + tid + ")->" + message, null);
        }
        public void Debug(int tid, object message, Exception ex)
        {
            Log(2, TraceEventType.Verbose, LogPriority.Verbose, _category, "TID(" + tid + ")->" + message, ex);
        }

        public void Info()
        {
            Log(2, TraceEventType.Information, LogPriority.Information, _category, null, null);
        }
        public void Info(object message)
        {
            Log(2, TraceEventType.Information, LogPriority.Information, _category, message, null);
        }
        public void Info(object message, Exception ex)
        {
            Log(2, TraceEventType.Information, LogPriority.Information, _category, message, ex);
        }
        public void Info(int tid, object message)
        {
            Log(2, TraceEventType.Information, LogPriority.Information, _category, "TID(" + tid + ")->" + message, null);
        }
        public void Info(int tid, object message, Exception ex)
        {
            Log(2, TraceEventType.Information, LogPriority.Information, _category, "TID(" + tid + ")->" + message, ex);
        }

        public void Warn()
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, null, null);
        }
        public void Warn(object message)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, message, null);
        }
        public void Warn(object message, Exception ex)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, message, ex);
        }
        public void Warn(int tid, object message)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, "TID(" + tid + ")->" + message, null);
        }
        public void Warn(int tid, object message, Exception ex)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, "TID(" + tid + ")->" + message, ex);
        }

        public void Error()
        {
            Log(2, TraceEventType.Error, LogPriority.Error, _category, null, null);
        }
        public void Error(object message)
        {
            Log(2, TraceEventType.Error, LogPriority.Error, _category, message, null);
        }
        public void Error(object message, Exception ex)
        {
            Log(2, TraceEventType.Error, LogPriority.Error, _category, message, ex);
        }
        public void Error(int tid, object message)
        {
            Log(2, TraceEventType.Error, LogPriority.Error, _category, "TID(" + tid + ")->" + message, null);
        }
        public void Error(int tid, object message, Exception ex)
        {
            Log(2, TraceEventType.Error, LogPriority.Error, _category, "TID(" + tid + ")->" + message, ex);
        }

        public void Statistic()
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, null, null);
        }
        public void Statistic(object message)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, message, null);
        }
        public void Statistic(object message, Exception ex)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, message, ex);
        }
        public void Statistic(int tid, object message)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, "TID(" + tid + ")->" + message, null);
        }
        public void Statistic(int tid, object message, Exception ex)
        {
            Log(2, TraceEventType.Warning, LogPriority.Warning, _category, "TID(" + tid + ")->" + message, ex);
        }

        private static void Log(int stackLevel, TraceEventType eventType, LogPriority priority, string category, object message, Exception ex)
        {
            StackFrame stackframe = new StackFrame(stackLevel, true);
            string source = string.Format("{0}.{1}", stackframe.GetMethod().DeclaringType, stackframe.GetMethod().Name);
            LogEntry logEntry = new LogEntry();

            string sessionId = "";
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                sessionId = string.Format("[{0}]", HttpContext.Current.Session.SessionID);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(sessionId);
            sb.Append(source).Append(": ");


            if (message == null && ex == null)
            {
                sb.Append("Log from [").Append(stackframe.GetFileName()).Append("] - line:").Append(stackframe.GetFileLineNumber());
            }
            else
            {
                if (message != null)
                {
                    sb.Append(message.ToString()).Append(" ");
                }
                if (ex != null)
                {
                    sb.Append(ex.ToString());
                }
            }

            logEntry.Message = sb.ToString();
            if (!String.IsNullOrEmpty(category))
            {
                logEntry.Categories.Add(category);
            }
            logEntry.Priority = (int)priority;
            logEntry.Severity = eventType;
            logEntry.EventId = _EventID;

            Logger.Write(logEntry);
        }


        /// <summary>
        /// SetLogFilePath
        /// </summary>
        /// <param name="listenerName">Learn Rolling File Listener</param>
        /// <param name="exeConfigName">Learn.ScheduleTask.exe.config</param>
        /// <param name="targetInterface">ScheduleTaskType</param>
        public static void SetLogFilePath(string listenerName, string exeConfigName, string targetInterface)
        {
            ConfigurationFileMap objConfigPath = new ConfigurationFileMap();

            // App config file path.
            string appPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            objConfigPath.MachineConfigFilename = appPath;

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + exeConfigName;

            Configuration entLibConfig = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            LoggingSettings loggingSettings = (LoggingSettings)entLibConfig.GetSection(LoggingSettings.SectionName);

            TraceListenerData traceListenerData = loggingSettings.TraceListeners.Get(listenerName);
            RollingFlatFileTraceListenerData objRollingFlatFileTraceListenerData = (RollingFlatFileTraceListenerData)traceListenerData;

            string filePath = objRollingFlatFileTraceListenerData.FileName;
            filePath = filePath.Insert(filePath.Length - 4, "." + targetInterface);

            objRollingFlatFileTraceListenerData.FileName = filePath;

            IUnityContainer container = new UnityContainer();
            container.AddNewExtension<EnterpriseLibraryCoreExtension>();

            // Configurator will read Enterprise Library configuration 
            // and set up the container
            UnityContainerConfigurator configurator = new UnityContainerConfigurator(container);

            var loggingXmlConfigSource = new SerializableConfigurationSource();
            loggingXmlConfigSource.Add(LoggingSettings.SectionName, loggingSettings);

            // Configure the container with our own custom logging
            EnterpriseLibraryContainer.ConfigureContainer(configurator, loggingXmlConfigSource);

            // Wrap in ServiceLocator
            IServiceLocator locator = new UnityServiceLocator(container);

            // Release lock(s) on existing file(s)
            EnterpriseLibraryContainer.Current.GetInstance<LogWriter>().Dispose();

            // And set Enterprise Library to use it
            EnterpriseLibraryContainer.Current = locator;
        }
    }
}
