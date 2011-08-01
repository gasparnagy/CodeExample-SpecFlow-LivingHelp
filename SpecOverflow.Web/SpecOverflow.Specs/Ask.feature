Feature: Ask Question

Background: 
	Given the following questions registered
		| Title                | Views | Votes |
		| Who am I             | 13    | 4     |
		| Yet another question | 42    | 2     |

Scenario: New questions should be added to the question list
	When I ask a new question with
		| Title        | Body                |
		| New question | This is what i need |
	Then the question should appear at the end of the question list as
		| Title        | Views | Votes |
		| New question | 0     | 0     |

Scenario: The title and the body are required fields for the question
	When I ask a new question with
		| Title | Body |
		|       |      |
	Then the following validation errors should be displayed
		| Message          |
		| title is missing |
		| body is missing  |
