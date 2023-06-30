using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceptance.Test.Model
{
    public class CreateCustomerCommand : IRequest<int>
    {

        public string Firstname { get; set; }



        public string Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }



        public string PhoneNumber { get; set; }



        public string Email { get; set; }


        public string BankAccountNumber { get; set; }
    }

}
