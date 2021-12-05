<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day02\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day02\testinput</Reference>
</Query>

var input = File.ReadAllLines(@".\testinput");

var instructions = input.Select(line => line.Split(" "))
						.Select(lineparts => new Instruction() { Command = lineparts[0], Amount = int.Parse(lineparts[1])});
int depth = instructions.Where(i => i.Command.Equals("down")).Select(i => i.Amount).Sum();
depth -= instructions.Where(i => i.Command.Equals("up")).Select(i => i.Amount).Sum();
int horizonalPosition = instructions.Where(i => i.Command.Equals("forward")).Select(i => i.Amount).Sum();
Console.WriteLine($"horizonalPosition x depth = {horizonalPosition} x {depth} = {horizonalPosition * depth}");

class Instruction {
	public string Command { get; set; }
	public int Amount { get; set; }
}
