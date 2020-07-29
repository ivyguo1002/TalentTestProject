@talent
Feature: Profile
	As a talent, 
	I should be able edit and save my profile.

@skills
Scenario: Display a skill
	Given post a skill through the skill api
	And a talent user is on the profile page
	Then the profile page displays the skill

@skills
Scenario: Add a skill
	Given a talent user is on the profile page
	When the user add a skill
	Then the skill is added

@skills@edit
Scenario: Edit a Skill
	Given post a skill through the skill api
	And a talent user is on the profile page
	When the user updates the skill
	Then the skill is updated

@skills@delete
Scenario: Delete a Skill
	Given post a skill through the skill api
	And a talent user is on the profile page
	When the user deletes the skill
	Then the skill is deleted