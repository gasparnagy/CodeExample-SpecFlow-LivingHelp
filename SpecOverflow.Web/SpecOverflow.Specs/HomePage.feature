Feature: Home Page

Scenario: All questions should be displayed on the home most viewed on top
	Given the following questions registered
		| Title                | Views | Votes |
		| Who am I             | 13    | 4     |
		| Yet another question | 42    | 2     |
	When I go to the home page
	Then the following questions should be displayed in this order
		| Title                | Views | Votes |
		| Yet another question | 42    | 2     |
		| Who am I             | 13    | 4     |

