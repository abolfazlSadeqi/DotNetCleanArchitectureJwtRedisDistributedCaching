
using Acceptance.Test.Model;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Acceptance.Test.StepDefinitions
{
    [Binding]
    public class Customer_CreateStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        private readonly HttpClient httpClient;


        public Customer_CreateStepDefinitions(ScenarioContext scenarioContext)
        {

            httpClient = new HttpClient();

            string baseAddress = Setting.APiAddress;

            httpClient.BaseAddress = new Uri(baseAddress);

            _scenarioContext = scenarioContext;
        }


        [When(@"I create  with Full")]
        public void WhenICreateWithFull(Table table)
        {

            // arrange

            var listcustomers = table.CreateSet<CreateCustomerCommand>();

            _scenarioContext.Add("CreatedCustomer", listcustomers);

        }

        [Then(@"create of this customers are  successful")]
        public void ThenCreateOfThisCustomersAreSuccessful()
        {

            var createdCustomer = _scenarioContext.Get<List<CreateCustomerCommand>>("CreatedCustomer");

            // act
            foreach (var itemcommand in createdCustomer)
            {
                var jsonString = JsonConvert.SerializeObject(itemcommand);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var result = httpClient.PostAsync("/api/Customers/Create", content);
                string resultContent = result.GetAwaiter().GetResult().Content.ReadAsStringAsync().Result;


                //Assert
                Assert.NotNull(resultContent);

                int Id = 0;
                int.TryParse(resultContent, out Id);
                Assert.True(Id > 0);




                var response = httpClient.GetAsync("api/Customers/Get/" + Id);

                // Act

                var stringResponse = response.GetAwaiter().GetResult().Content.ReadAsStringAsync().Result;
                var terms = System.Text.Json.JsonSerializer.Deserialize<CustomerDto>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                //Assert
                Assert.Equal(Id, terms.Id);
            }

        }
    }
}