Feature: CreateUser
	Create a new user
@mytag
	Scenario: Create a new user with valid inputs
	Given User payload "CreateUser.json"
	When Send request to create user
	Then Validate user is created
