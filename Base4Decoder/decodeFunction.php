<?php

// get the q parameter from URL
$q = $_REQUEST["q"];

$result = "";

// function to decode the encoded Base4 input. 
if ($q !== "") {
    $arrayString = str_split($q);
	
	function ConvertStringToBinary($strToReplace)
	{
		if ($strToReplace == "A") {
		return "00";
		} elseif ($strToReplace == "B") {
		return "01";
		} elseif ($strToReplace == "C") {
		return "10";
		} elseif ($strToReplace == "D") {
		return "11";
		}
	}
	function ConvertBinaryToCharecter($binToReplace)
	{
		$temporaryValue = bindec($binToReplace);
		return chr($temporaryValue);
	}
		
	$stringToBinaryArray = array_map("ConvertStringToBinary", $arrayString);
	$convertArrayToString = implode(",",$stringToBinaryArray);
	$removeCommas = str_replace(array(','), '' , $convertArrayToString);
	$myArray = [];
	
	if(strlen($removeCommas)%8 == 0){
		$stringValue = $removeCommas;
		$myArray = str_split($stringValue, 8);
		$binaryArrayToCharecters = array_map("ConvertBinaryToCharecter",$myArray);
		$convertToString = implode(",",$binaryArrayToCharecters);
		$result = str_replace(array(','), '' , $convertToString);
	}else{
		$result = "Please enter valid code";
	}
}
// Output 
echo $result;
?>
