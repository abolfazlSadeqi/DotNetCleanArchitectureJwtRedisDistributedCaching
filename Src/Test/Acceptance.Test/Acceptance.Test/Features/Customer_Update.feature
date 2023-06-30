Feature: Customer_Create

    Scenario: created Customers   
        When I create  with Full
          | FirstName     | LastName        | DateOfBirth | PhoneNumber | Email               | BankAccountNumber   |
          | Test512441234785 | Test012410dd664244     | 2015-01-17  | 09121234785 | aaaaa@gmail.com | 123456214455 |
        Then create of this customers are  successful