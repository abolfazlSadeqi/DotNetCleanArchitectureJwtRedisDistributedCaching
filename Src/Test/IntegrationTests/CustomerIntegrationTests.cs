using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetCustomeresWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<PublicApi.Startup>>
{
    private readonly HttpClient httpClient;

    public IntegrationTests(WebApplicationFactory<PublicApi.Startup> factory)
    {
        httpClient = factory.CreateClient();
    }


    [Fact]
    public async Task DeleteCustomer()
    {
        // arrange


        int id = 17;

        //act
        var result = await httpClient.DeleteAsync("/api/Customers/Delete/" + id);
        string resultContent = await result.Content.ReadAsStringAsync();


        //Assert
        Assert.NotNull(resultContent);


    }



    [Fact]
    public async Task CreateCustomer()
    {
        // arrange



        string Firstname = "TEst_FirtName1";
        string Lastname = "TEst_Lastname";
        string Email = "TEst_Email_d1d@gmail.com";
        string PhoneNumber = "09159889630";
        string BankAccountNumber = "123456789678";
        DateTime DateOfBirth = DateTime.Now.Date;

        CreateCustomerCommand command = new CreateCustomerCommand();
        command.Firstname = Firstname;
        command.Lastname = Lastname;
        command.Email = Email;
        command.PhoneNumber = PhoneNumber;
        command.BankAccountNumber = BankAccountNumber;
        command.DateOfBirth = DateOfBirth;


        // act
        var jsonString = JsonConvert.SerializeObject(command);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var result = await httpClient.PostAsync("/api/Customers/Create", content);
        string resultContent = await result.Content.ReadAsStringAsync();


        //Assert
        Assert.NotNull(resultContent);
        Assert.True(int.Parse(resultContent) > 1);

    }


    [Fact]
    public async Task UpdateCustomer()
    {
        // arrange


        int Id = 17;

        string Firstname = "TEst_FirtName1";
        string Lastname = "TEst_Lastname";
        string Email = "aaadfffdf44aa@adfda.com";
        string PhoneNumber = "09159889630";
        string BankAccountNumber = "912345679678";
        DateTime DateOfBirth = DateTime.Now.Date;

        UpdateCustomerCommand command = new UpdateCustomerCommand();
        command.Firstname = Firstname;
        command.Lastname = Lastname;
        command.Email = Email;
        command.PhoneNumber = PhoneNumber;
        command.BankAccountNumber = BankAccountNumber;
        command.DateOfBirth = DateOfBirth;
        command.Id = Id;


        // act
        var jsonString = JsonConvert.SerializeObject(command);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        var result = await httpClient.PutAsync("/api/Customers/Update", content);
        string resultContent = await result.Content.ReadAsStringAsync();


        //Assert
        Assert.Equal(resultContent, Id.ToString());

    }




    [Fact]
    public async Task GetById()
    {
        //arrange 
        int Id = 11;
        var response = await httpClient.GetAsync("api/Customers/Get/" + Id);

        // Act
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var terms = System.Text.Json.JsonSerializer.Deserialize<CustomerDto>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        //Assert
        Assert.Equal(Id, terms.Id);

    }




}