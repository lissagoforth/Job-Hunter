using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Hunter_Trapper_Keeper.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
