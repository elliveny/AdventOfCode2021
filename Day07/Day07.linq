<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day07\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day07\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/7

var input = File.ReadAllLines(@".\input");
var inputPosition = input[0].Split(',').Select(v => int.Parse(v)).ToArray();
var maxPosition = inputPosition.Max();
var positionCounts = new int[maxPosition + 1];
foreach (var position in inputPosition) {
	positionCounts[position]++;
}

int leastFuelPart1 = int.MaxValue;
int leastFuelPositionPart1 = -1;
int leastFuelPart2 = int.MaxValue;
int leastFuelPositionPart2 = -1;
for (int checkPosition = 0; checkPosition < maxPosition + 1; checkPosition++) {
	var checkPositionTotalFuelPart1 = 0;
	var checkPositionTotalFuelPart2 = 0;
	for (int otherPosition = 0; otherPosition < maxPosition + 1; otherPosition++) {
		var fuelUsedPart1 = Math.Abs(checkPosition - otherPosition);
		checkPositionTotalFuelPart1 += positionCounts[otherPosition] * fuelUsedPart1;
		
		int fuelUsedPart2 = 0;
		var distancePart2 = Math.Abs(checkPosition - otherPosition);
		for (var distanceStep = distancePart2; distanceStep > 0; distanceStep--) fuelUsedPart2 += distanceStep;
		checkPositionTotalFuelPart2 += positionCounts[otherPosition] * fuelUsedPart2;
	}
	if (checkPositionTotalFuelPart1 < leastFuelPart1) {
		leastFuelPart1 = checkPositionTotalFuelPart1;
		leastFuelPositionPart1 = checkPosition;
	}
	if (checkPositionTotalFuelPart2 < leastFuelPart2) {
		leastFuelPart2 = checkPositionTotalFuelPart2;
		leastFuelPositionPart2 = checkPosition;
	}
}
Console.WriteLine($"Part 1: Least fuel is {leastFuelPart1} from position {leastFuelPositionPart1}");
Console.WriteLine($"Part 2: Least fuel is {leastFuelPart2} from position {leastFuelPositionPart2}");
