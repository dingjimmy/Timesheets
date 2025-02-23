@DeploymentVerification
Feature: Verify Deployment was successfull (i.e smoke test)

    Scenario: The home page loads
        Given user has navigated to the Timesheets home page
        Then the copyright notice is displayed
    
    Scenario: The expected build has been deployed
        Given user has navigated to the Timesheets home page
        Then the correct build number is displayed