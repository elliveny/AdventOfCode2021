<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day02\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day02\testinput</Reference>
</Query>

var input = File.ReadAllLines(@".\input");
var instructions = input.Select(line => new Instruction(line));
var state = new SubmarineState();
instructions.ToList().ForEach(i => i.DoCommand(state));
Console.WriteLine(state.ToString());

class Instruction {
	public string Command { get; set; }
	public int Amount { get; set; }

	public Instruction(string input) {
		var inputParts = input.Split(" ");
		Command = inputParts[0];
		Amount = int.Parse(inputParts[1]);
	}

	internal void DoCommand(SubmarineState state) {
		switch (Command) {
			case "down":
				//down X increases your aim by X units.
				state.Aim += Amount;
				break;
			case "up":
				//up X decreases your aim by X units.
				state.Aim -= Amount;
				break;
			case "forward":
				// forward X does two things:
				//It increases your horizontal position by X units.
				state.HorizonalPosition += Amount;
				//It increases your depth by your aim multiplied by X.
				state.Depth += state.Aim * Amount;
				break;
		}
	}
}

class SubmarineState {
	public int Aim { get; set; }
	public int Depth { get; set; }
	public int HorizonalPosition { get; set; }

	public SubmarineState() {
		Aim = 0;
		Depth = 0;
		HorizonalPosition = 0;
	}

	public override string ToString() {
		return $"HorizonalPosition x Depth = {HorizonalPosition} x {Depth} = {HorizonalPosition * Depth}";
	}
}
