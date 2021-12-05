<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day01\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day01\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

var input = File.ReadAllLines(@".\input");
int numIncreases = 0;
for (int i = 3; i < input.Length; i++) {
	int val1 = int.Parse(input[i - 1]) + int.Parse(input[i - 2]) + int.Parse(input[i - 3]);
	int val2 = int.Parse(input[i]) + int.Parse(input[i - 1]) + int.Parse(input[i - 2]);
	if (val2 > val1) numIncreases++;
}
Console.WriteLine(numIncreases);