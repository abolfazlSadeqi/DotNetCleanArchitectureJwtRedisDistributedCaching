using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Validation
{
    public class ValidateBankAccountNumberAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage("bank account number");
        }

        public override bool IsValid(object? bankAccountNumberValue)
        {

            return bankAccountNumberValue == null ? false : Validatedata.IsValidValidateBankAccountNumber(bankAccountNumberValue.ToString());

        }
    }

}
