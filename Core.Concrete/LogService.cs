using Core.Infrastructure;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Concrete
{
    public class LogService:ILogService
    {
        private ILog _log;
        private readonly string _connectionString;
        private readonly IEmailService _emailService;
        public LogService(string connectionString, IEmailService emailService)
        {
            _connectionString = connectionString;
            _emailService = emailService;
        }

        public string PortalInfo { get; set; }
        public ILog Log
        {
            get { return ConfigureLog(); }
        }

        public ILogger Logger { get { return _log.Logger; } }

        public void Debug(object message)
        {
            if (Log.IsDebugEnabled)
                Log.Debug(message);
        }

        public void Debug(object message, Exception ex)
        {
            if (!Log.IsDebugEnabled) return;
            message = string.Format("{0}\r\n{1}\r\nMessage: {2}\r\n", "Portal Debug:", PortalInfo, message);
            Log.Debug(message, ex);
        }

        public void Info(object message)
        {
            if (Log.IsInfoEnabled)
                Log.Info(message);
        }

        public void Info(object message, Exception ex)
        {
            if (!Log.IsInfoEnabled) return;
            message = string.Format("{0}\r\n{1}\r\nMessage: {2}\r\n", "Portal Debug:", PortalInfo, message);
            Log.Info(message, ex);
        }

        public void Warn(object message)
        {
            if (Log.IsWarnEnabled)
                Log.Warn(message);
        }

        public void Warn(object message, Exception ex)
        {
            if (!Log.IsWarnEnabled) return;
            Log.Warn(string.Format("{0}\r\n{1}\r\nMessage: {2}\r\n", "Portal Debug:", PortalInfo, message), ex);
        }

        public void Error(object message)
        {
            if (Log.IsErrorEnabled)
                Log.Error(message);
        }

        public void Error(object message, Exception ex)
        {
            if (Debugger.IsAttached) Debugger.Break();

            if (!Log.IsErrorEnabled) return;        
            var msg = string.Format("{0}\r\n{1} \r\n \r\nMessage: {2}\r\n", "Portal Error:", PortalInfo, message);
            Log.Error(msg, ex);
            _emailService.SendErrorEmailAsync(ex, msg);
        }

        public void Fatal(object message)
        {
            if (Log.IsFatalEnabled)
                Log.Fatal(message);
        }

        public void Fatal(object message, Exception ex)
        {
            if (Log.IsFatalEnabled)
                Log.Fatal(string.Format("{0}\r\n{1}\r\nMessage: {2}\r\n", "Portal Error:", PortalInfo, message), ex);
        }

        private ILog ConfigureLog()
        {
            log4net.Config.XmlConfigurator.Configure();
            var stackTrace = new StackTrace();
            var callingType = stackTrace.GetFrame(2).GetMethod().DeclaringType;
            if (callingType != null)
                _log = LogManager.GetLogger(callingType.FullName);
            var builder = new EntityConnectionStringBuilder(_connectionString) { Metadata = null };
            var conn = new System.Data.SqlClient.SqlConnection(builder.ProviderConnectionString);
            PortalInfo =
                string.Format("Portal Name: {0}\r\nDB Server: {1}\r\nDB Name: {2}\r\nEnvironment: {3}\r\nPortal Root: {4}\r\n",
                    "JPS_Project", conn.DataSource, conn.Database, Environment.MachineName,
                    AppDomain.CurrentDomain.BaseDirectory);
            return _log;
        }
    }
}
