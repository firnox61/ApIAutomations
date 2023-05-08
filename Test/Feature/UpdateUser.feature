Feature: UpdateUser

	The user value update

@tag1
Scenario: [Update user with valid inputs]
	Given [User payload "CreateUser.json"]
	When [Send request to Update user]
	Then [Validate user is update]

