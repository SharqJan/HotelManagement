using System;

namespace SMSC.Core.Logger.Interfaces
{
    public interface ILog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(Exception ex, string message);
    }
}
