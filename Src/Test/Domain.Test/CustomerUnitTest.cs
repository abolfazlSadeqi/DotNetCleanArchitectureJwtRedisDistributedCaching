using Domain.Entities;

namespace Domain.Test
{
    public class CustomerUnitTest
    {
        [Fact]
        public void Test_Create()
        {
            //arrange
            Customer customer = new();

            string Firstname = "TEst_FirtName";
            string Lastname = "TEst_Lastname";
            string Email = "TEst_Email_dd";
            string PhoneNumber = "09121111111";
            string BankAccountNumber = "1234567898";
            DateTime DateOfBirth = DateTime.Now.Date;

            //act
            var _CreateCustomer = customer.CreateCustomer(Firstname, Lastname, DateOfBirth, PhoneNumber, Email, BankAccountNumber);


            //assert
            Assert.NotNull(_CreateCustomer);
            Assert.True(

              _CreateCustomer.Firstname.Equals(Firstname) &&

                  _CreateCustomer.Lastname.Equals(Lastname)
              && _CreateCustomer.DateOfBirth.Equals(DateOfBirth)
                   && _CreateCustomer.Email.Equals(Email)
                  && _CreateCustomer.PhoneNumber.Equals(PhoneNumber)
                    && _CreateCustomer.BankAccountNumber.Equals(BankAccountNumber)
                );
        }


        [Fact]
        public void Test_Update()
        {
            //arrange
            Customer customer = new();

            string Firstname = "TEst_FirtName";
            string Lastname = "TEst_Lastname";
            string Email = "TEst_Email_dd";
            string PhoneNumber = "09121111111";
            string BankAccountNumber = "1234567898";
            DateTime DateOfBirth = DateTime.Now.Date;

            //act
            var _UpdateCustomer = customer.UpdateCustomer(customer, Firstname, Lastname, DateOfBirth, PhoneNumber, Email, BankAccountNumber);


            //assert
            Assert.NotNull(_UpdateCustomer);
            Assert.True(_UpdateCustomer.Firstname.Equals(  Firstname) &&
  
                  _UpdateCustomer.Lastname.Equals(Lastname)
              && _UpdateCustomer.DateOfBirth.Equals( DateOfBirth) 
                   && _UpdateCustomer.Email.Equals(Email)
                  &&  _UpdateCustomer.PhoneNumber.Equals(PhoneNumber)
                    &&    _UpdateCustomer.BankAccountNumber.Equals(BankAccountNumber)
                );
        }


    }
}