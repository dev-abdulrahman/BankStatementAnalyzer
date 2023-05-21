using log4net;
using System;

namespace BankStatementAnalyzer.WebUI.Common.logger
{
    internal class LoggerFacade<T> : ILoggerFacade<T> where T : class
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(T));
        private readonly string correlationId;

        public LoggerFacade()
        {
        }

        public LoggerFacade(string correlationId)
        {
            this.correlationId = correlationId;
        }

        public void Debug(object message)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Debug(message);
            }
        }

        public void Debug(object message, Exception exception)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Debug(message, exception);
            }
        }

        public void Error(object message)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Error(message);
            }
        }

        public void Error(object message, Exception exception)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Error(message, exception);
            }
        }

        public void Fatal(object message)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Fatal(message);
            }
        }

        public void Fatal(object message, Exception exception)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Fatal(message, exception);
            }
        }

        public void Info(object message)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Info(message);
            }
        }

        public void Info(object message, Exception exception)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Info(message, exception);
            }
        }

        public void Warn(object message)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Warn(message);
            }
        }

        public void Warn(object message, Exception exception)
        {
            using (LogicalThreadContext.Stacks["correlationId"].Push(correlationId))
            {
                logger.Warn(message, exception);
            }
        }
    }
}