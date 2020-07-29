@talent
Feature: Dashboard


Scenario: Navigate to Profile Page
	Given a talent user is on Dashboard Page
	When the user clicks on Profile menu
	Then the user is navigated to profile page

Scenario: Nagivate to Explore Jobs Page
	Given a talent user is on Dashboard Page
	When the user clicks on Explore Jobs menu
	Then the user is navigated to Explore Jobs page

