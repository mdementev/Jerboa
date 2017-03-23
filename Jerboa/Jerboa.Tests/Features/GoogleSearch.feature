Feature: GoogleSearch
	In order to avoid silly mistakes
	I want to check google search

@Google.GoogleStart
@Common
Scenario: Search for jerboa wiki
	Given I search for jerboa
	Then Result is Jerboa - Wikipedia
