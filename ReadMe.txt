-----------
Definitions
-----------

----
Word
----
LowerCase
Must contain 1 vowel
	2 or more if the length is greater then 3
Vowels appear in alphabetical order
No capitals
No punctuation

----
Line
----
All lines end with /n
Length is the sum of the characters

------
Column
------
Position of a character on the line

-------------
Letter Moment
-------------
Product of the letter's distance from the the column and the letter's value in the alphabet (a being 1 and z as 26)

-----------
Word Moment
-----------
Sum of all its constituent letter moments

-----------
Line Moment
-----------
Sum of all its constituent word moments

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

----------
FillAdjust
----------
Formatted as fill
All lines must end on the wrap column
	Uses spaces as padding
Spaces are evenly placed wherever possible
	In the case where spaces are uneven they are more concentrated between the words with the most vowels

----------
LineMoment
----------
Formatted as fill
Seeks to reduce absolute value of the line moment as much as possible
Words can be seperated by one or more spaces
	Most seperated words should be at the front of the line


-------
FillSet
-------
Formatted as fill
Word order does not need to be upheld
Number of lines should be minimal

-----------------
XML Input Options
-----------------

------
Format
------
Fill
FillSoft
FillAdjust
LineMoment
FillSet

----
Wrap
----
Positive numerical value

--------
WrapSoft
--------
Positive numerical value
Less then the wrap

------------
ColumnMoment
------------
Numerical value

-----
Words
-----
Alphanumerical characters
Data set used