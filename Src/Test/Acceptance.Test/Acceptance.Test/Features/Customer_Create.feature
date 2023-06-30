Feature: Customer_Update

    Scenario: Updated Customers   
        When I Update  with Full
          | Id | FirstName        | LastName           | DateOfBirth | PhoneNumber | Email           | BankAccountNumber |
          |  24| Test512441234785 | Test012410dd664244 | 2015-01-17  | 09121234785 | aaafaa@gmail.com | 123456214455      |
        Then Update of this customers are  successful