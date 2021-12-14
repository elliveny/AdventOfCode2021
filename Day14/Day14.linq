<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day14\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day14\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/14

long GetMostCommonMinusLeastCommon(string polymer, Dictionary<string, long> polymerPairs) {
	var totals = new Dictionary<string, long>();
	totals.Add(polymer[polymer.Length - 1].ToString(), 1);
	foreach (var key in polymerPairs.Keys) {
		var letter = key[0].ToString();
		if (totals.ContainsKey(letter)) {
			totals[letter] += polymerPairs[key];
		} else {
			totals.Add(letter, polymerPairs[key]);
		}
	}

	var mostCommon = totals.Max(t => t.Value);
	var leastCommon = totals.Min(t => t.Value);
	return mostCommon - leastCommon;
}

var input = File.ReadAllLines(@".\input");
var polymer = input[0];

var rules = new Dictionary<string, string>();
var lineNo = 2;
while (lineNo < input.Length) {
	var ruleParts = input[lineNo].Replace(" -> ", ",").Split(',');
	rules.Add(ruleParts[0], ruleParts[1]);
	lineNo++;
}

var polymerPairs = new Dictionary<string, long>();
for (var i = 0; i < polymer.Length - 1; i++) {
	var pair = polymer.Substring(i, 2);
	if (polymerPairs.ContainsKey(pair)) {
		polymerPairs[pair]++;
	} else {
		polymerPairs.Add(pair, 1);
	}
}


for (var step = 1; step <= 40; step++) {
	var newPolymerPairs = new Dictionary<string, long>();
	foreach (var key in polymerPairs.Keys) {
		if (rules.ContainsKey(key)) {
			var pair = key[0] + rules[key];
			if (newPolymerPairs.ContainsKey(pair)) {
				newPolymerPairs[pair] += polymerPairs[key];
			} else {
				newPolymerPairs.Add(pair, polymerPairs[key]);
			}
			pair = rules[key] + key[1];
			if (newPolymerPairs.ContainsKey(pair)) {
				newPolymerPairs[pair] += polymerPairs[key];
			} else {
				newPolymerPairs.Add(pair, polymerPairs[key]);
			}
		} else {
			newPolymerPairs.Add(key, polymerPairs[key]);
		}
	}
	polymerPairs = newPolymerPairs;
	if (step == 10) {
		Console.WriteLine($"Part 1: mostCommon-leastCommon={GetMostCommonMinusLeastCommon(polymer, polymerPairs)}");
	}
}
Console.WriteLine($"Part 2: mostCommon-leastCommon={GetMostCommonMinusLeastCommon(polymer, polymerPairs)}");

