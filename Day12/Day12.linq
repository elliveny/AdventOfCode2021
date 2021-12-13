<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day12\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day12\testinput</Reference>
  <Reference Relative="testinput2">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day12\testinput2</Reference>
  <Reference Relative="testinput3">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day12\testinput3</Reference>
</Query>

// https://adventofcode.com/2021/day/12

var input = File.ReadAllLines(@".\input");
var graph = new Graph();
foreach (var line in input) {
	var lineParts = line.Split('-');
	graph.AddEdge(lineParts[0], lineParts[1]);
}

var part1Paths = graph.FindPaths("start", "end", false).ToArray();
var part2Paths = graph.FindPaths("start", "end", true).ToArray();

Console.WriteLine($"Part 1: {part1Paths.Length} paths found");
Console.WriteLine($"Part 2: {part2Paths.Length} paths found");

class Graph {
	Dictionary<string, List<string>> nodeConnections = new Dictionary<string, List<string>>();

	public void AddEdge(string from, string to) {
		if (!nodeConnections.ContainsKey(from)) {
			nodeConnections.Add(from, new List<string>() { to });
		} else {
			nodeConnections[from].Add(to);
		}
		if (!nodeConnections.ContainsKey(to)) {
			nodeConnections.Add(to, new List<string>() { from });
		} else {
			nodeConnections[to].Add(from);
		}
	}

	public IEnumerable<List<string>> FindPaths(string startNode, string endNode, bool part2Rules) {
		return FindPaths(new List<string>() { startNode }, startNode, startNode, endNode, part2Rules);
	}

	private IEnumerable<List<string>> FindPaths(List<string> currentPath, string currentNode,string startNode, string endNode, bool part2Rules) {
		string[] nextNodes;
		if (part2Rules) {
			var twiceVisitedSmallCave = currentPath.Where(node => node.ToLower().Equals(node)).GroupBy(node => node).Where(node => node.Count()==2).Select(node => node.Key).FirstOrDefault();
			if (twiceVisitedSmallCave != null) {
				nextNodes = nodeConnections[currentNode].Where(nextNode => !nextNode.Equals(nextNode.ToLower()) || (nextNode.Equals(nextNode.ToLower()) && !currentPath.Contains(nextNode))).ToArray();
			} else {
				nextNodes = nodeConnections[currentNode].Where(nextNode => !nextNode.Equals(startNode)).ToArray();
			}
		} else {
			nextNodes = nodeConnections[currentNode].Where(nextNode => !nextNode.Equals(nextNode.ToLower()) || (nextNode.Equals(nextNode.ToLower()) && !currentPath.Contains(nextNode))).ToArray();
		}
		foreach (var nextNode in nextNodes) {
			var thisPath = new List<string>();
			thisPath.AddRange(currentPath);
			thisPath.Add(nextNode);
			if (!nextNode.Equals(endNode)) {
				foreach(var path in FindPaths(thisPath, nextNode, startNode, endNode, part2Rules)) {
					yield return path;	
				}
			} else {
				yield return thisPath;
			}
		}
	}
}