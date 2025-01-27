@EndToEndTest
Feature: Playwright Test Home Page
      
    Scenario: The Home Page Title is Valid
        Given user has navigated to playwright home page
        Then the page title must contain the word 'playwright'
        
    Scenario: The Home Page Title is Valid2
        Given user has navigated to playwright home page
        Then the page title must contain the word 'playwright'
        
    Scenario: The 'Get Started' link navigates to Installation page
        Given user has navigated to playwright home page
        When they click on the 'get started' link
        Then the 'Installation' page is loaded