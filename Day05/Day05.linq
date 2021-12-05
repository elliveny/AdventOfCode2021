<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day05\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day05\testinput</Reference>
</Query>

var input = File.ReadAllLines(@".\input");
var inputCoords = new List<int[]>();
foreach (var line in input) {
	inputCoords.Add(line.Replace("->", ",").Split(',').Select(v => int.Parse(v)).ToArray());
}

Console.WriteLine($"Part 1: {CountOverlaps(inputCoords, true)} with at least two lines overlapping");
Console.WriteLine($"Part 2: {CountOverlaps(inputCoords, false)} with at least two lines overlapping");


int[][] GetLineCoords(int startX, int endX, int startY, int endY) {
	var deltaX = endX - startX;
	var deltaY = endY - startY;
	double xInc, yInc;
	if (Math.Abs(deltaX) > Math.Abs(deltaY)) {
		xInc = Math.Sign(deltaX);
		yInc = (double)deltaY / (double)deltaX;
	} else {
		xInc = (double)deltaX / Math.Abs((double)deltaY);
		yInc = Math.Sign(deltaY);
	}

	// Draw the line
	double x = startX;
	double y = startY;
	var lineXY = new List<int[]>() { new int[] { (int)x, (int)y } };
	while ((int)x != endX || (int)y != endY) {
		x += xInc;
		y += yInc;
		lineXY.Add(new int[] { (int)x, (int)y });
	}
	return lineXY.ToArray();
}

int CountOverlaps(List<int[]> inputCoords, bool onlyHorizonalAndVertical) {
	var grid = new Dictionary<string, int>();
	foreach (var coord in inputCoords) {
		if (!onlyHorizonalAndVertical || (coord[0] == coord[2] || coord[1] == coord[3])) {
			foreach (var lineCoords in GetLineCoords(coord[0], coord[2], coord[1], coord[3])) {
				var coordStr = $"{lineCoords[0]},{lineCoords[1]}";
				if (grid.ContainsKey(coordStr)) {
					grid[coordStr]++;
				} else {
					grid.Add(coordStr, 1);
				}
			};
		}
	}
	return grid.Values.Where(overlaps => overlaps > 1).Count();
}