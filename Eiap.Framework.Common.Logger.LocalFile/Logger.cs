﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eiap.Framework.Common.Logger.LocalFile
{
    public class Logger : ILogger
    {
        private readonly ILoggerConfigeration _LoggerConfigeration;
        private object lockobj = new object();
        public Logger(ILoggerConfigeration loggerConfigeration)
        {
            _LoggerConfigeration = loggerConfigeration;
        }

        public void Debug(string message, string logKey, int logSource = 0, string logName = null)
        {
            LogMessage logMessage = GetLogMessage(message, logKey, LogLevel.DEBUG, logName, logSource);
            SaveLogMessage(logMessage);
        }

        public void Error(string message, string logKey, int logSource = 0, string logName = null)
        {
            LogMessage logMessage = GetLogMessage(message, logKey, LogLevel.ERROR, logName, logSource);
            SaveLogMessage(logMessage);
        }

        public void Fatal(string message, string logKey, int logSource = 0, string logName = null)
        {
            LogMessage logMessage = GetLogMessage(message, logKey, LogLevel.FATAL, logName, logSource);
            SaveLogMessage(logMessage);
        }

        public void Info(string message, string logKey, int logSource = 0, string logName = null)
        {
            LogMessage logMessage = GetLogMessage(message, logKey, LogLevel.INFO, logName, logSource);
            SaveLogMessage(logMessage);
        }

        public void Warn(string message, string logKey, int logSource = 0, string logName = null)
        {
            LogMessage logMessage = GetLogMessage(message, logKey, LogLevel.WARN, logName, logSource);
            SaveLogMessage(logMessage);
        }

        private LogMessage GetLogMessage(string message, string logKey, LogLevel logLevel, string logName, int logSource)
        {
            Guid logBodyKey = Guid.NewGuid();
            LogMessage logmessage = new LogMessage
            {
                LogHead = new LogHead
                {
                    Id = Guid.NewGuid(),
                    LogBodyKey = logBodyKey,
                    LogDateTime = DateTime.Now,
                    LogKey = logKey,
                    LogLevel = logLevel,
                    //TODO:后续完善
                    LogName = logName,
                    LogSource = logSource,
                    ApplicationName = "",
                    ServerIp = "",
                    ModulesName = ""
                },
                LogBody = new LogBody
                {
                    Id = logBodyKey,
                    LogBodyContent = message
                }
            };
            return logmessage;
        }

        private void SaveLogMessage(LogMessage logMessage)
        {
            string logpathformat = _LoggerConfigeration.LogPathFormat;
            long logsize = _LoggerConfigeration.LogSize;
            string logpath = logpathformat
                .Replace("{AppCode}", logMessage.LogHead.LogName)
                .Replace("{LogLevel}",logMessage.LogHead.LogLevel.ToString())
                .Replace("{YYYY}", DateTime.Now.Year.ToString())
                .Replace("{MM}", DateTime.Now.Month.ToString())
                .Replace("{DD}", DateTime.Now.Day.ToString())
                .Replace("{HH}", DateTime.Now.Hour.ToString())
                .Replace("{mm}", DateTime.Now.Minute.ToString());
            lock (lockobj)
            {
                if (!Directory.Exists(Path.GetDirectoryName(logpath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logpath));
                }
                FileInfo fileinfo = new FileInfo(logpath);
                if (fileinfo.Exists && fileinfo.Length >= logsize)
                {
                    logpath = logpath.Replace(".log", "_" + DateTime.Now.Ticks.ToString() + ".log");
                }
                File.AppendAllText(logpath, logMessage.ToString(), Encoding.UTF8);
            }
        }
    }
}
