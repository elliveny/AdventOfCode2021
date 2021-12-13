<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day11\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day11\testinput</Reference>
</Query>

// https://adventofcode.com/2021/day/11

var input = File.ReadAllLines(@".\input");
var world = new World(input[0].Length, input.Length);

for (var y = 0; y < input.Length; y++) {
	for (var x = 0; x < input[y].Length; x++) {
		var startingEnergy = int.Parse(input[y][x].ToString());
		world.AddOctopus(x, y, startingEnergy);
	}
}

bool foundAllFlash = false;
while (world.CurrentStep < 100 || !foundAllFlash) {
	world.NextStep();
	if (world.CurrentStep == 100) Console.WriteLine($"Part 1: Total flash count at step 100={world.TotalFlashCount}");
	if (world.AllOctopiFlashedThisStep()) {
		foundAllFlash = true;
		Console.WriteLine($"Part 2: All Flashed at step {world.CurrentStep}");
	}
}


class World {
	public World(int xSize, int ySize) {
		grid = new Octopus[xSize, ySize];
		gridXSize = xSize;
		gridYSize = ySize;
	}
	private int gridXSize;
	private int gridYSize;

	public Octopus[,] grid;
	public int CurrentStep { get; private set; }
	public int TotalFlashCount { get; private set; }
	public int FlashCountThisStep { get; private set; }
	public event EventHandler StepChange;

	public void NextStep() {
		this.CurrentStep++;
		FlashCountThisStep = 0;
		StepChange?.Invoke(this, null);
	}

	internal void AddOctopus(int x, int y, int startingEnergyLevel) {
		var octopus = new Octopus(this, x, y, startingEnergyLevel);
		grid[x, y] = octopus;
		octopus.Flash += OnOctopusFlash;
	}

	public bool AllOctopiFlashedThisStep() {
		return (this.FlashCountThisStep == gridXSize * gridYSize);
	}

	void OnOctopusFlash(object sender, EventArgs e) {
		var octopus = (Octopus)sender;
		TotalFlashCount++;
		FlashCountThisStep++;
		BoostAjacent(octopus.X - 1, octopus.Y - 1);
		BoostAjacent(octopus.X, octopus.Y - 1);
		BoostAjacent(octopus.X + 1, octopus.Y - 1);
		BoostAjacent(octopus.X - 1, octopus.Y);
		BoostAjacent(octopus.X + 1, octopus.Y);
		BoostAjacent(octopus.X - 1, octopus.Y + 1);
		BoostAjacent(octopus.X, octopus.Y + 1);
		BoostAjacent(octopus.X + 1, octopus.Y + 1);
	}

	private void BoostAjacent(int x, int y) {
		if (x >= 0 && x <= gridXSize - 1 && y >= 0 && y <= gridYSize - 1) {
			grid[x, y]?.Boost();
		}
	}

	internal void Draw() {
		for (var y = 0; y < gridYSize; y++) {
			for (var x = 0; x < gridYSize; x++) {
				Console.Write(grid[x, y].EnergyLevel);
			}
			Console.WriteLine();
		}
	}
}

class Octopus {
	public Octopus(World w, int x, int y, int startingEnergyLevel) {
		this.world = w;
		this.X = x;
		this.Y = y;
		this.EnergyBoost = startingEnergyLevel;
		this.world.StepChange += OnWorldStepChange;
	}

	public event EventHandler Flash;

	private World world;
	private int lastFlashStep = 0;

	public int X { get; set; }
	public int Y { get; set; }
	public int EnergyBoost { get; set; }
	public int EnergyLevel
	{
		get
		{
			return this.world.CurrentStep - lastFlashStep + this.EnergyBoost;
		}
	}

	void OnWorldStepChange(object sender, EventArgs e) {
		CheckEnergyLevel();
	}

	bool FlashedThisStep() {
		return this.lastFlashStep == world.CurrentStep;
	}

	internal void Boost() {
		if (!FlashedThisStep()) {
			this.EnergyBoost++;
			CheckEnergyLevel();
		}
	}

	public void CheckEnergyLevel() {
		if (this.EnergyLevel > 9) {
			this.EnergyBoost = 0;
			this.lastFlashStep = world.CurrentStep;
			Flash?.Invoke(this, null);
		}
	}
}
