<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day03\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day03\testinput</Reference>
</Query>

var input = File.ReadAllLines(@".\input");

// Part 1
var numBits = input[0].Length;
var topBitValue = (1 << numBits - 1);
var inputValues = input.Select(line => Convert.ToInt32(line, 2)).ToArray();

var gamma = GetMostCommonBits(inputValues, numBits);
var epsilon = gamma ^ ((topBitValue << 1) - 1);
Console.WriteLine($"Part 1: power consumption = gamma x epsilon = {gamma} x {epsilon} = {gamma * epsilon}");

// Part 2
var oxygenGeneratorValues = inputValues;
var co2ScrubberValues = inputValues;
var mask = 0;
for (int bitNum = numBits; bitNum > 0; bitNum--) {
	mask = (1 << bitNum - 1);
	if (oxygenGeneratorValues.Length > 1) {
		var mostCommonOxygenGeneratorMask = GetMostCommonBits(oxygenGeneratorValues, numBits) & mask;
		oxygenGeneratorValues = oxygenGeneratorValues.Where(v => (v & mask) == (mostCommonOxygenGeneratorMask)).ToArray();
	}
	if (co2ScrubberValues.Length > 1) {
		var mostCo2ScrubberMask = (GetMostCommonBits(co2ScrubberValues, numBits) ^ ((topBitValue << 1) - 1)) & mask;
		co2ScrubberValues = co2ScrubberValues.Where(v => (v & mask) == (mostCo2ScrubberMask)).ToArray();
	}
}
Console.WriteLine($"Part 2: life support rating = oxygenGenerator x co2Scrubber = {oxygenGeneratorValues[0]} x {co2ScrubberValues[0]} = {oxygenGeneratorValues[0] * co2ScrubberValues[0]}");

// Functions
int GetMostCommonBits(int[] inputValues, int numBits) {
	var bitTotals = new int[numBits];
	for (var i = 0; i < inputValues.Length; i++) {
		for (int bitNum = 0; bitNum < numBits; bitNum++) {
			bitTotals[bitNum] += (inputValues[i] & (1 << bitNum)) > 0 ? 1 : -1;
		}
	}

	var mostCommonBits = 0;
	for (int bitNum = 0; bitNum < numBits; bitNum++) {
		if (bitTotals[bitNum] > 0 || (bitTotals[bitNum] == 0)) {
			mostCommonBits |= (1 << bitNum);
		}
	}
	return mostCommonBits;
}