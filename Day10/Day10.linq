<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day10\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day10\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/10

var input = File.ReadAllLines(@".\input");

var chunkChars = new List<char> { '(', '[', '{', '<', ')', ']', '}', '>' };
var chunkCharPart1Points = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };

var part1Points = 0;
var part2LinePoints = new List<long>();
var parseStack = new Stack<char>();
foreach (var line in input) {
	var corruptLine = false;
	foreach (var c in line.ToCharArray()) {
		if (chunkChars.IndexOf(c) < 4) {
			parseStack.Push(c); // Open char
		} else {
			var expectedCloseCharIndex = chunkChars.IndexOf(parseStack.Peek()) + 4;
			var expectedCloseChar = chunkChars[expectedCloseCharIndex];
			if (c == expectedCloseChar) {
				parseStack.Pop();
			} else {
				//Console.WriteLine($"{line} - Expected {expectedCloseChar} but found {c} instead");
				part1Points += chunkCharPart1Points[c];
				corruptLine = true;
				break;
			}
		}
	}

	if (!corruptLine) {
		// Deal with incompletes
		long lineScore=0;
		while (parseStack.Count > 0) {
			var openChar = parseStack.Pop();
			var closeChar = chunkChars[chunkChars.IndexOf(openChar) + 4];
			lineScore *= 5;
			lineScore += chunkChars.IndexOf(closeChar)-3;
		}
		part2LinePoints.Add(lineScore);
	} else {
		parseStack.Clear();
	}
}
var sortedPart2LinePoints = part2LinePoints.OrderBy(p => p).ToArray();
var part2Points = sortedPart2LinePoints[sortedPart2LinePoints.Length >> 1];

Console.WriteLine($"Part 1: Total points={part1Points}");
Console.WriteLine($"Part 2: Total points={part2Points}");