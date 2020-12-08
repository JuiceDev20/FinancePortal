using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancePortal.Models
{
    public class Household
    {
        public Household()
        {
            Attachments = new HashSet<HouseholdAttachment>();
            BankAccounts = new HashSet<HouseholdBankAccount>();
            Categories = new HashSet<HouseholdCategory>();
            CategoryItems = new HashSet<CategoryItem>();
            Invitations = new HashSet<HouseholdInvitation>();
            Notifications = new HashSet<HouseholdNotification>();
            Occupants = new HashSet<FPUser>();

        }

        public int Id { get; set; }

        [StringLength(35)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Greeting { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        public virtual ICollection<HouseholdAttachment> Attachments { get; set; }

        public virtual ICollection<HouseholdBankAccount> BankAccounts { get; set; }

        public virtual ICollection<HouseholdCategory> Categories { get; set;}

        public virtual ICollection<CategoryItem> CategoryItems { get; set; }

        public virtual ICollection<HouseholdInvitation> Invitations { get; set; }

        public virtual ICollection<HouseholdNotification> Notifications { get; set; }

        public virtual ICollection<FPUser> Occupants { get; set; }


    }
}
