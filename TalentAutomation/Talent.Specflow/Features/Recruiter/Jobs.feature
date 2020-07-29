 @recruiter
Feature: Jobs
	As a recuiter/employer, 
	I should be able to filter, view and manage the jobs.

 @jobStatus
Scenario: Change job status
	Given a recruiter user is on the Jobs page 
	When the user click the job switch button on the first job card
	Then The first job status is updated