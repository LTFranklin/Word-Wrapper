-----------
Definitions
-----------

----
Word
----
LowerCase
Must contain 1 vowel
	2 or more if the length is greater then 3
vowels appear in alphabetical order

----
Line
----
All lines end with /n
Length is the sum of the characters

------
Column
------
Position of a character on the line

-------
Methods
-------

----
Fill
----
Same order as the input
Spaces between words on the same line
Lines cannot be longer then the wrap column
	Unless a word's length goes over the wrap alone

--------
FillSoft
--------
Same conditions as fill
Soft wrap is less then or equal to the wrap column
Lines cannot be longer then the soft wrap
	Unless there is only one word
	Unless the line postion is less then or equal to half the number of lines on the page