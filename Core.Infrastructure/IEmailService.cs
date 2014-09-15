using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public interface IEmailService
    {
        /// <summary>
        /// Send asynchronous error email 
        /// </summary>
        /// <param name="ex">exception</param>
        /// <param name="message">message</param>
        /// <returns>Task</returns>
        Task SendErrorEmailAsync(Exception ex, string message);
    }
}
