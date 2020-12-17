using FinancePortal.Data;
using FinancePortal.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Threading.Tasks;
using Transaction = FinancePortal.Models.Transaction;

namespace FinancePortal.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailSender _emailSender;

        public NotificationService(ApplicationDbContext context, IEmailSender emailSender)
        {
            _dbContext = context;
            _emailSender = emailSender;
           
        }
        public async Task NotifyOverdraftAsync(Transaction transaction, HouseholdBankAccount householdBankAccount, decimal oldBalanace)
        {
            if (householdBankAccount.CurrentBalance < 0 && oldBalanace > 0)
            {
                var user = (await _dbContext.Users.FindAsync(transaction.FPUserId));
                //Step 1. Create a new Notification record
                var householdNotification = new HouseholdNotification
                {
                    Created = DateTime.Now,
                    HouseholdId = householdBankAccount.HouseholdId,
                    IsRead = false,
                    Subject = $"Your {householdBankAccount.Name} account has been overdrafted.",
                    Body = $"{user.FullName} overdrafted the {householdBankAccount.Name} {householdBankAccount.AccountType} account on {transaction.Created:MMM dd,yyyy} by {Math.Abs(householdBankAccount.CurrentBalance):#.00} as a result of a transaction in the amount of {transaction.Amount:#.00}"
                                   
                };

                _dbContext.Add(householdNotification);
                await _dbContext.SaveChangesAsync();
                await _emailSender.SendEmailAsync(user.Email, householdNotification.Subject, householdNotification.Body);

            }
        }
    }
}
