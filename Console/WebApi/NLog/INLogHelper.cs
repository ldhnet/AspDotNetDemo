using System;

namespace WebApi.NLog
{
    public interface INLogHelper
    {
        void LogError(Exception ex);
    }
}
