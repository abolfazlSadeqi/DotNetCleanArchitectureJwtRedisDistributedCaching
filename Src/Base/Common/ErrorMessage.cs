using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ErrorMessage
    {
        public static string MinimumLength { get { return " Minimum Length {0} is not Valid"; } }

        public static string MaximumLength { get { return " Maximum Length {0} is not Valid"; } }
        public static string NotEmpty { get { return " {0} IS Empty "; } }

        public static string EmailNotValid { get { return " Email Not Valid "; } }
        public static string BankAccountNumberNotValid { get { return " BankAccountNumber not Valid "; } }
        public static string PhoneNumberNotValid { get { return " PhoneNumber not Valid "; } }
        public static string ValidEmailunique { get { return "Email is not unique "; } }
        public static string ValidCustomersunique { get { return "Customers is not unique "; } }


    }
}
