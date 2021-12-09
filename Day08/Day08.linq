<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day08\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day08\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/8

var input = File.ReadAllLines(@".\input");

var digits = new char[10][];
digits[0] = "abcefg".ToCharArray();
digits[1] = "cf".ToCharArray();
digits[2] = "acdeg".ToCharArray();
digits[3] = "acdfg".ToCharArray();
digits[4] = "bcdf".ToCharArray();
digits[5] = "abdfg".ToCharArray();
digits[6] = "abdefg".ToCharArray();
digits[7] = "acf".ToCharArray();
digits[8] = "abcdefg".ToCharArray();
digits[9] = "abcdfg".ToCharArray();

int part1Total = 0;
int part2Total = 0;
foreach (var line in input) {
	var signalInputSet = line.Split('|')[0].Split(' ').Where(signalInput => signalInput.Trim() != "").ToArray();
	var outputValueSet = line.Split('|')[1].Split(' ').Where(outputValue => outputValue.Trim() != "").ToArray();

	part1Total += outputValueSet.Where(vs => vs.Length == digits[1].Length || vs.Length == digits[4].Length || vs.Length == digits[7].Length || vs.Length == digits[8].Length).Count();

	var mapping = new string[10];
	// Unique lengths
	mapping[1] = signalInputSet.Where(vs => vs.Length == digits[1].Length).First();
	mapping[4] = signalInputSet.Where(vs => vs.Length == digits[4].Length).First();
	mapping[7] = signalInputSet.Where(vs => vs.Length == digits[7].Length).First();
	mapping[8] = signalInputSet.Where(vs => vs.Length == digits[8].Length).First();
	// 9 has 6 segments, with one more segment than 7 and 4 combined
	mapping[9] = signalInputSet.Where(vs => vs.Length == 6 && !mapping.Contains(vs) && vs.Except(mapping[4]).Except(mapping[7]).Count() == 1).First();
	// 3 has 5 segments, all of which are in 7...
	mapping[3] = signalInputSet.Where(vs => vs.Length == 5 && mapping[7].Except(vs).Count() == 0).First();
	// 2 and 5 both have 5 segments, all of 5 is in 9
	mapping[5] = signalInputSet.Where(vs => vs.Length == 5 && !mapping.Contains(vs) && vs.Except(mapping[9]).Count() == 0).First();
	// The last remaining 5 segment shape must be 2
	mapping[2] = signalInputSet.Where(vs => vs.Length == 5 && !mapping.Contains(vs)).First();
	// 6 segment shapes remain - 0 and 6. 0 contains all segments from 7, where 6 does not
	mapping[0] = signalInputSet.Where(vs => vs.Length == 6 && !mapping.Contains(vs) && mapping[7].Except(vs).Count() == 0).First();
	// What remains must be 6
	mapping[6] = signalInputSet.Where(vs => !mapping.Contains(vs)).First();

	var mappedValue = mapping.Select((value, index) => new { value, index }).Where(m => outputValueSet[0].Except(m.value.ToCharArray()).Count() == 0 && outputValueSet[0].Length == m.value.Length).Select(m => m.index).First() * 1000
		+ mapping.Select((value, index) => new { value, index }).Where(m => outputValueSet[1].Except(m.value.ToCharArray()).Count() == 0 && outputValueSet[1].Length == m.value.Length).Select(m => m.index).First() * 100
		+ mapping.Select((value, index) => new { value, index }).Where(m => outputValueSet[2].Except(m.value.ToCharArray()).Count() == 0 && outputValueSet[2].Length == m.value.Length).Select(m => m.index).First() * 10
		+ mapping.Select((value, index) => new { value, index }).Where(m => outputValueSet[3].Except(m.value.ToCharArray()).Count() == 0 && outputValueSet[3].Length == m.value.Length).Select(m => m.index).First();
	part2Total += mappedValue;
}
Console.WriteLine($"Part 1: {part1Total}");
Console.WriteLine($"Part 2: {part2Total}");
