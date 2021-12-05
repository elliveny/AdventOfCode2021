<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day01\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day01\testinput</Reference>
  <Namespace>System.Net</Namespace>
</Query>

var input = File.ReadAllLines(@".\input");
int numIncreases = 0;
for (int i = 1; i < input.Length; i++) {
	int val1 = int.Parse(input[i - 1]);
	int val2 = int.Parse(input[i]);
	if (val2 > val1) numIncreases++;
}
Console.WriteLine(numIncreases);