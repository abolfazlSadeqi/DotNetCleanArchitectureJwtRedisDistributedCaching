using Application.Common.Interfaces;
using Application.Customers.Commands.CreateCustomer;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using Shouldly;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using System.Collections;
using Application.Customers.Commands.UpdateCustomer;

namespace Test.Application
{
    public class ValidationUpdateCustomerUnitTest
    {

          public ValidationUpdateCustomerUnitTest()
        {

            
        }


        [Fact]
        public void Test_ValidateRule()
        {
            //arrange

            UpdateCustomerCommand request = new UpdateCustomerCommand();

            request.Firstname = "TEst_FirtName";
            request.Lastname = "TEst_Lastname";
            request.Email = "TEst_Email_dd@Gmail.com";
            request.PhoneNumber = "09121111111";
            request.BankAccountNumber = "1234567898";
            request.DateOfBirth = DateTime.Now.Date;


            //act
            UpdateCustomerCommandValidator validations = new UpdateCustomerCommandValidator();
            var _result = validations.Validate(request);


            //assert
            Assert.NotNull(_result);
            Assert.True(_result.IsValid);

            Assert.Equal(_result.Errors.Count(), 0);

        }


      

    }

   
}