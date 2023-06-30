using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Validation;
public class ValidateEmailAttribute : ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return base.FormatErrorMessage("email");
    }

    public override bool IsValid(object? emailValue)
    {
        return emailValue == null ? false : Validatedata.IsValidEmailValue(emailValue.ToString());

       
    }
}

