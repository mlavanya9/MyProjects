
This project is developed using HTML,CSS, JavaScript and PHP.
This accepts any string as an input and gives the decoded form of the string using Base4 Decoding.

## Decoding:
Decoding is the opposite process of encoding-- the conversion of an encoded format back into the original sequence of characters.
function to decode the Base4 input. 
In the decoding part,str_split is used to convert the input string to an array. 
Functions used in php code:
1.ConvertStringToBinary($strToReplace)
2.ConvertBinaryToCharecter($binToReplace)
1.function ConvertStringToBinary($strToReplace)
This function is used replace a string to binary(in our project we are using capital A,B,C,D letters).
A = 00
B = 01
C = 10
D = 11 

2.function ConvertBinaryToCharecter($binToReplace)
After converting to binary,this function coverts binary to its respective ASCII and then to character.
bindec is the built in function of php where it converts binary to decimal and that is stored in some temporary value. 
If the string length(i.e) the total length of string is perfectly divided by 8, then we perform the remaining operations.
array_map will apply the call back to the elements in the array string.
implode is used to join the string.
When we get the binary ASCII number and replace them with characters, this implode is used to join all those characters with a comma(,).
str_replace is used to replace those comma's(,) with an empty space
