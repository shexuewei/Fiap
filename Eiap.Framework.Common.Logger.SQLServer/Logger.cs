using Eiap.Framework.Base.Dependency.SXW;
using Eiap.Framework.Common.DataAccess.SQL;
using Eiap.Framework.Common.DataMapping.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eiap.Framework.Common.Logger.SQLServer
{
    public class Logger //: ILogger
    {
        private readonly ISQLCommandMapping<LogHead, Guid> _headCommand;
        private readonly ISQLCommandMapping<LogBody, Guid> _bodyCommand;

        public Logger(ISQLCommandMapping<LogHead, Guid> headCommand, ISQLCommandMapping<LogBody, Guid> bodyCommand)
        {
            ISQLCommandDataAccessConnection _Con = (ISQLCommandDataAccessConnection)DependencyManager.Instance.Resolver<ISQLCommandDataAccessConnection>();
            _headCommand = headCommand;
            _bodyCommand = bodyCommand;
            _headCommand.SQLDataAccessConnection = _Con;
            _bodyCommand.SQLDataAccessConnection = _Con;
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

        private LogMessage GetLogMessage(string message, string logKey, int logLevel, string logName, int logSource)
        {
            Guid logBodyKey = Guid.NewGuid();
            LogMessage logmessage = new LogMessage
            {
                LogHead = new LogHead
                {
                    Id = Guid.NewGuid(),
                    LogBodyKey = logBodyKey,
                    LogDay = DateTime.Now.Day,
                    LogDateTime = DateTime.Now,
                    LogHour = DateTime.Now.Hour,
                    LogKey = logKey,
                    LogLevel = logLevel,
                    LogMillisecond = DateTime.Now.Millisecond,
                    LogMinute = DateTime.Now.Minute,
                    LogMonth = DateTime.Now.Month,
                    LogSecond = DateTime.Now.Second,
                    LogYear = DateTime.Now.Year,
                    //TODO:后续完善
                    LogName = logName,
                    LogSource = logSource,
                    ApplicationName = "",//AppDomain.CurrentDomain.ApplicationIdentity.FullName,
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
            SaveLogHead(logMessage.LogHead);
            SaveLogBody(logMessage.LogBody);
        }

        private void SaveLogHead(LogHead logHead)
        {
            try
            {
                _headCommand.SQLDataAccessConnection.Create();
                _headCommand.SQLDataAccessConnection.DBOpen();
                _headCommand.InsertEntity(logHead);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                _headCommand.SQLDataAccessConnection.DBClose();
            }
        }

        private void SaveLogBody(LogBody logBody)
        {
            try
            { 
            _bodyCommand.SQLDataAccessConnection.Create();
            _bodyCommand.SQLDataAccessConnection.DBOpen();
            _bodyCommand.InsertEntity(logBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _bodyCommand.SQLDataAccessConnection.DBClose();
            }
        }
    }
}
