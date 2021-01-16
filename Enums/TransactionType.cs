using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancePortal.Enums
{
    public enum TransactionType
    {
        Deposit,
        PointOfSale,  //In-person purchase
        ACH,          //Online purchase or payments
        Withdrawal,
        Transfer      //Move funds btn accounts
    }
}
