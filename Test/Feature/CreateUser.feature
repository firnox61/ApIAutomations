Feature: CreateUser
	Create a new user
@mytag
Scenario: Create a new user with valid inputs
	Given User with name "Peter"
	And user with job "Manager"
	When Send request to create user
	Then Validate user is created