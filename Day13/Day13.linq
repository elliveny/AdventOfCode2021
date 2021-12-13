<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day13\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day13\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/13

void DrawCoords(List<XY> coords) {
	var minX = coords.Select(c => c.X).Min();
	var maxX = coords.Select(c => c.X).Max();
	var minY = coords.Select(c => c.Y).Min();
	var maxY = coords.Select(c => c.Y).Max();
	for (var y = minY; y <= maxY; y++) {
		for (var x = minX; x <= maxX; x++) {
			Console.Write(coords.Contains(new XY(x, y)) ? "#" : " ");
		}
		Console.WriteLine();
	}
}

var input = File.ReadAllLines(@".\input");
var lineNo=0;
var coords=new List<XY>();
while(!input[lineNo].Equals("")) {
	coords.Add(new XY(input[lineNo]));
	lineNo++;
}

lineNo++;
var instructionCount=0;
while (lineNo < input.Length) {
	var instructionParts=input[lineNo].Split('=');
	if (instructionParts[0].Equals("fold along y")) {
		coords = DoYFold(coords, int.Parse(instructionParts[1]));
	} else {
		coords = DoXFold(coords, int.Parse(instructionParts[1]));
	}
	instructionCount++;
	if (instructionCount==1) Console.WriteLine($"Part 1: Visible dots={coords.Count()}");
	lineNo++;
}
Console.WriteLine("Part 2 (view using monotype font):");
DrawCoords(coords);

List<XY> DoXFold(List<XY> coords, int foldLineX) {
	var newCoords = new List<XY>();
	foreach (var c in coords) {
		if (c.X < foldLineX) {
			if (!newCoords.Contains(c)) newCoords.Add(c);
		} else {
			var transposedC = new XY() { X = foldLineX - (c.X - foldLineX), Y = c.Y };
			if (!newCoords.Contains(transposedC)) newCoords.Add(transposedC);
		}
	}
	return newCoords;
}

List<XY> DoYFold(List<XY> coords, int foldLineY) {
	var newCoords=new List<XY>();
	foreach (var c in coords) {
		if (c.Y < foldLineY) {
			if (!newCoords.Contains(c)) newCoords.Add(c);
		} else {
			var transposedC = new XY() { X = c.X, Y = foldLineY - (c.Y - foldLineY) };
			if (!newCoords.Contains(transposedC)) newCoords.Add(transposedC);
		}
	}
	return newCoords;
}

struct XY {
	public XY(string line) {
		var lineParts=line.Split(',');
		this.X=int.Parse(lineParts[0]);
		this.Y=int.Parse(lineParts[1]);
	}

	public XY(int x, int y) {
		X = x;
		Y = y;
	}

	public int X { get; set; }
	public int Y { get; set; }

	public override string ToString() {
		return $"{X},{Y}";
	}
}
