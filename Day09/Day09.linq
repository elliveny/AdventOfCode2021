<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day09\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day09\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/9

var input = File.ReadAllLines(@".\input");
int[,] grid = new int[input[0].Length, input.Length];
var xSize = input[0].Length;
var ySize = input.Length;
for (var y = 0; y < ySize; y++) {
	for (var x = 0; x < xSize; x++) {
		grid[x, y] = int.Parse(input[y].Substring(x, 1));
	}
}

int GetBasinSize(int x, int y) {
	var basinCoords = new HashSet<string>();
	return ExpandBasin(x, y, basinCoords);
}

int ExpandBasin(int x, int y, HashSet<string> basinCoords) {
	var coords = $"{x},{y}";
	if (basinCoords.Contains(coords)) return basinCoords.Count();
	basinCoords.Add(coords);
	if (x > 0 && grid[x - 1, y] != 9) ExpandBasin(x - 1, y, basinCoords);
	if (x < xSize - 1 && grid[x + 1, y] != 9) ExpandBasin(x + 1, y, basinCoords);
	if (y > 0 && grid[x, y - 1] != 9) ExpandBasin(x, y - 1, basinCoords);
	if (y < ySize - 1 && grid[x, y + 1] != 9) ExpandBasin(x, y + 1, basinCoords);
	return basinCoords.Count();
}

int part1TotalRiskLevel = 0;
var basinSizes = new List<int>();
for (var y = 0; y < ySize; y++) {
	for (var x = 0; x < xSize; x++) {
		bool isLowPoint = true;
		if (x > 0) isLowPoint = grid[x, y] < grid[x - 1, y];
		if (isLowPoint && x < xSize - 1) isLowPoint = grid[x, y] < grid[x + 1, y];
		if (isLowPoint && y > 0) isLowPoint = grid[x, y] < grid[x, y - 1];
		if (isLowPoint && y < ySize - 1) isLowPoint = grid[x, y] < grid[x, y + 1];
		if (isLowPoint) {
			part1TotalRiskLevel += grid[x, y] + 1;
			basinSizes.Add(GetBasinSize(x, y));
		}
	}
}
var top3BasinSizes = basinSizes.OrderByDescending(s => s).Take(3).ToArray();

Console.WriteLine($"Part 1: Total Risk Level={part1TotalRiskLevel}");
Console.WriteLine($"Part 2: Top 3 Basin Sizes={top3BasinSizes[0] * top3BasinSizes[1] * top3BasinSizes[2]}");