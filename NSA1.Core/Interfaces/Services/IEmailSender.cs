using NSA1.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Interfaces.Services
{
    public interface IEmailSender
    {
        Task SendWelcomeEmailAsync(WelcomeRequest request);
        Task DiffDeviceLoginAsync(WelcomeRequest request);
        Task ForgotPasswordEmail(ForgotPasswordEmail request);
    }
}
