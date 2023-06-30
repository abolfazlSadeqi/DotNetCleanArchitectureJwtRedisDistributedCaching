using Acceptance.Test.Model;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Acceptance.Test.StepDefinitions
{
    [Binding]
    public class Customer_UpdateStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly HttpClient httpClient;

        public Customer_UpdateStepDefinitions(ScenarioContext scenarioContext)
        {

            httpClient = new HttpClient();

            string baseAddress = Setting.APiAddress;

            httpClient.BaseAddress = new Uri(baseAddress);

            _scenarioContext = scenarioContext;
        }


        [When(@"I Update  with Full")]
        public void WhenIUpdateWithFull(Table table)
        {
            var listcustomers = table.CreateSet<UpdateCustomerCommand>();

            _scenarioContext.Add("UpdatedCustomer", listcustomers);
        }

        [Then(@"Update of this customers are  successful")]
        public void ThenUpdateOfThisCustomersAreSuccessful()
        {
            var UpdatedCustomer = _scenarioContext.Get<List<UpdateCustomerCommand>>("UpdatedCustomer");

            // act
            foreach (var itemcommand in UpdatedCustomer)
            {
                var jsonString = JsonConvert.SerializeObject(itemcommand);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var result = httpClient.PutAsync("/api/Customers/Update", content);
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

                Assert.Equal(itemcommand.Email, terms.Email);
            }
        }
    }
}
