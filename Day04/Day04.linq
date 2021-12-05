<Query Kind="Statements">
  <Reference Relative="input">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day04\input</Reference>
  <Reference Relative="testinput">&lt;MyDocuments&gt;\LINQPad Queries\AdventOfCode2021\Day04\testinput</Reference>
</Query>

static Board[] ReadBoards(StreamReader inputStream) {
	var boards = new List<Board>();
	var lines = new List<string>();
	while (!inputStream.EndOfStream) {
		var line = inputStream.ReadLine();
		if (inputStream.EndOfStream) lines.Add(line);
		if (line.Equals("") || inputStream.EndOfStream) {
			if (lines.Count > 0) {
				boards.Add(new Board(lines.ToArray()));
				lines.Clear();
			}
		} else {
			lines.Add(line);
		}
	}
	return boards.ToArray();
}

var inputStream = File.OpenText(@".\input");
int[] draw = inputStream.ReadLine().Split(',').Select(v => int.Parse(v)).ToArray();
Board[] boards = ReadBoards(inputStream);
inputStream.Close();

// Part 1
Board part1WinningBoard = null;
Board part2LosingBoard = null;
int boardsLeft = boards.Length;
foreach (var d in draw) {
	for (int boardNum = 0; boardNum < boards.Length; boardNum++) {
		var board = boards[boardNum];
		if (!board.HasWon) {
			board.CrossOffNumber(d);
			if (part1WinningBoard == null && board.HasWon) {
				part1WinningBoard = board;
				int score = part1WinningBoard.GetScore();
				Console.WriteLine($"Part 1: Last draw={d}, winning board no={boardNum}, winning board score={score}. Final score={d * score}");
			}
			if (board.HasWon) boardsLeft--;
			if (boardsLeft == 0) {
				part2LosingBoard = board;
				int score = part2LosingBoard.GetScore();
				Console.WriteLine($"Part 2: Last draw={d}, last winning board no={boardNum}, last board score={score}. Final score={d * score}");
			}
		}
	}
}

// Functions/Classes
public class Board {
	private int xSize;
	private int ySize;
	private int[,] board;
	private bool[,] crossedOff;

	public int this[int x, int y]
	{
		get { return board[x, y]; }
	}

	public Board(string[] vs) {
		xSize = (vs[0].Length + 1) / 3;
		ySize = vs.Length;
		board = new int[xSize, ySize];
		crossedOff = new bool[xSize, ySize];
		for (var y = 0; y < ySize; y++) {
			for (var x = 0; x < xSize; x++) {
				board[x, y] = int.Parse(vs[y].Substring(x * 3, 2));
			}
		}
	}

	public void DrawBoard() {
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				Console.Write($" {(crossedOff[x, y] ? "XX" : board[x, y].ToString().PadLeft(2))}");
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}

	public void CrossOffNumber(int d) {	
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				if (board[x, y] == d && !crossedOff[x, y]) CrossOffXY(x, y);
			}
		}
	}

	private void CrossOffXY(int x, int y) {
		crossedOff[x, y] = true;
		// Check horizontal
		bool won = true;
		for (var checkX = 0; checkX < xSize; checkX++) won = won && crossedOff[checkX, y];
		if (!won) {
			// Check vertical
			won = true;
			for (var checkY = 0; checkY < ySize; checkY++) won = won && crossedOff[x, checkY];
		}
		this.HasWon = won;
	}

	/// <summary>Score is the 'sum of all unmarked numbers on that board'</summary>
	public int GetScore() {
		int score = 0;
		for (int y = 0; y < ySize; y++) {
			for (int x = 0; x < xSize; x++) {
				if (!crossedOff[x, y]) {
					score += board[x, y];
				}
			}
		}
		return score;
	}

	public bool HasWon { get; private set; }
}
