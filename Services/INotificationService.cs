using FinancePortal.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace FinancePortal.Services
{

    public interface INotificationService
    {
        public Task NotifyOverdraftAsync(Transaction transaction, HouseholdBankAccount householdBankAccount, decimal oldBalanace);
    }
}
