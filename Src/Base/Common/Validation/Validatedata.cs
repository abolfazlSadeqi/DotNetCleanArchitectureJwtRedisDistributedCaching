using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Common.Validation
{
    public static class Validatedata
    {
        public static bool IsValidValidateBankAccountNumber(string bankAccountNumberValue)
        {
            string? bankAccountNumber = bankAccountNumberValue.ToString();
            if (string.IsNullOrEmpty(bankAccountNumber)) return false;

            Regex regex = new("^[0-9]{9,18}$");

            return regex.IsMatch(bankAccountNumber);
        }

        public static bool IsValidphoneNumber(string phoneNumber)
        {
            string? _phoneNumber = phoneNumber.ToString();
            if (string.IsNullOrEmpty(_phoneNumber)) return false;

            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();

            try
            {
                var phoneNumberInstance = phoneNumberUtil.Parse(_phoneNumber, "IR");
                return phoneNumberUtil.IsValidNumber(phoneNumberInstance);
            }
            catch (Exception ex)
            {
                return false;
            }



        }

       

        public static bool IsValidEmailValue(string emailValue)
        {
            string? email = emailValue.ToString();
            if (string.IsNullOrEmpty(email)) return false;

            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            //string emailRegex = string.Format("{0}{1}",
            //        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
            //        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            return regex.IsMatch(email);



        }
    }

}
