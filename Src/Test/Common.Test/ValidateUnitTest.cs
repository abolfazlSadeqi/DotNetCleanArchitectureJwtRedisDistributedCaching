using Common.Validation;

namespace Common.Test
{
    public class ValidateUnitTest
    {
        [Fact]
        public void Test_Email()
        {
            //arrange

            string Email = "Test123Iran@gmail.com";

            //act
            bool _Valid = Validatedata.IsValidEmailValue(Email);


            //assert
            Assert.NotNull(_Valid);
            Assert.True(_Valid);
        }


        [Fact]
        public void Test_BankAccountNumber()
        {
            //arrange

            string BankAccountNumber = "123452147";

            //act
            bool _Valid = Validatedata.IsValidValidateBankAccountNumber(BankAccountNumber);


            //assert
            Assert.NotNull(_Valid);
            Assert.True(_Valid);
        }


        [Fact]
        public void Test_phoneNumber()
        {
            //arrange

            string phoneNumber = "09121234567";

            //act
            bool _Valid = Validatedata.IsValidphoneNumber(phoneNumber);


            //assert
            Assert.NotNull(_Valid);
            Assert.True(_Valid);
        }

    }
}