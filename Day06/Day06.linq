<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day06\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day06\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/6

var input = File.ReadAllLines(@".\testinput");
var inputList = input[0].Split(',').Select(v => int.Parse(v)).ToArray();

long[] juveniles = new long[] {0,0,0,0,0,0,0,0,0};
long[] adults = new long[] {0,0,0,0,0,0,0};
foreach(var v in inputList) {
	adults[v]++;
}

for (int day = 1; day <= 256 ; day++) {
	var juvenilesDayIndex = (day - 1) % 9;
	var adultsDayIndex = (day - 1) % 7;
	var spawningAdults = adults[adultsDayIndex];
	var juvenilesToAdults = juveniles[juvenilesDayIndex];
	adults[adultsDayIndex] += juvenilesToAdults;
	juveniles[juvenilesDayIndex] = spawningAdults + juvenilesToAdults;
	if (day == 80) {
		Console.WriteLine($"Part 1: Day {day} Total Fish={(long)juveniles.Sum() + (long)adults.Sum()}");
	}
	if (day == 256) {
		Console.WriteLine($"Part 2: Day {day} Total Fish={(long)juveniles.Sum() + (long)adults.Sum()}");
	}
}
