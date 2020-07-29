Feature: Account

Scenario: Login with valid credential
	Given a talent user has set an account
	Given the user is on Login page
	When the user log in with the valid credential
	Then dashboard page should be shown

Scenario: Login with invalid credential
	Given a talent user has set an account
	Given the user is on Login page
	When the user log in with invalid credential
	Then dashboard page should not be shown