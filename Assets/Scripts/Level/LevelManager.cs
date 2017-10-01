using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelManager : MonoBehaviour {

	public Figure[,] Grid;
	
	/// <summary>
	/// Все ячейки на доске.
	/// </summary>
	public Cell[,] allCells;

	public GameObject WinPanel;
	public GameObject LosePanel;

	public VirusPool VirusPool;
	public ScoreManager Score;

	public BoardGraphics Graphics;

	/// <summary>
	/// Все игроки.
	/// </summary>
	private PlayerController[] players;
	
	/// <summary>
	/// Выделенная ячейка.
	/// </summary>
	private Figure selectedVirus;

	public AudioClip SelectAudio;
	public AudioClip MoveAudio;

	public PlayerController currentPlayer
	{
		get
		{
			return this.players[currentPlayerIndex];	
		}
	}

	public int currentPlayerIndex;

	public BattleState battleState;

	private int countLosePlayers;

	void Awake()
	{
		this.Graphics = GetComponent<BoardGraphics>();
	}

	// Use this for initialization
	void Start () {

		this.countLosePlayers = 0;
		currentPlayerIndex = 0;

		this.Score = GetComponent<ScoreManager>();
		this.VirusPool = GetComponent<VirusPool>();
	}

	private void StartPhase()
	{
		if (this.battleState != BattleState.Play)
			return;

		// Если больше нет свободных ячеек, подсчитываем очки
		if (!HasEmptyCells())
		{
			int best_score = 0;
			int you_score = 0;

			for (int i = 0; i < players.Length; i++)
			{
				if (players[i].IsVictim)
					you_score = players[i].Score;
				else if (!players[i].IsLose && players[i].Score > best_score)
					best_score = players[i].Score;
			}

			if (you_score > best_score)
				Win();
			else
				GameOver();

			return;
		}

		// Проверяем может ли ходить игрок
		if (!this.currentPlayer.CanPlayMore())
		{
			// Если это живой игрок, тогда пишем, что он проиграл
			if (this.currentPlayer.IsVictim)
			{
				GameOver();

				return;
			}
			// Если это компьютер то затемняем его ячейки и выключаем его
			else
			{
				this.countLosePlayers++;
				this.currentPlayer.Lose();

				if (this.countLosePlayers == players.Length - 1)
				{
					Win();
					return;
				}

				SwitchNextPlayer();
			}
		}

		if (!this.currentPlayer.IsVictim)
		{
			StartCoroutine(MoveAI());
		}
	}

	private IEnumerator MoveAI()
	{
		yield return StartCoroutine(this.currentPlayer.MoveAI());

		this.currentPlayer.StepCount++;

		SwitchNextPlayer();

		StartPhase();
	}



	public void SetPlayers(PlayerController[] players)
	{
		this.players = players;

		this.Score.SetPlayers(players);
	}

	private void SwitchNextPlayer()
	{
		currentPlayerIndex++;

		if (currentPlayerIndex == this.players.Length)
		{
			currentPlayerIndex = 0;
		}

		if (currentPlayer.IsLose)
			SwitchNextPlayer();
	}

	public void cell_Clicked(Cell cell)
	{
		if (this.battleState != BattleState.Play)
			return;

		if (this.currentPlayer.IsVictim)
		{
			// Если мы кликнули на ячейке игрока и она еще не была выделена, тогда
			// выделяем ячейку
			//if (cell.state == CellState.Busy && cell.player == this.players[currentPlayerIndex] &&
			//    (selectedCell == null || selectedCell != cell))
			//{
			//    OnPlayerCellClicked(cell);

			//    return;
			//}

			// Если была выделена ячейка, и мы кликнули на подсвеченную ячейку, захватываем подсвеченную ячейку
			if (this.selectedVirus != null && GetDistance(cell, this.selectedVirus) <= this.currentPlayer.MoveDistance)
			{
				StartCoroutine(OnClearCellClicked(cell));

				//NextPlayer();

				//EndPhase();

				return;
			}
		}
	}

	private IEnumerator OnClearCellClicked(Cell cell)
	{
		UnhighlightAll();

		GetComponent<AudioSource>().PlayOneShot(MoveAudio);

		// Копируем или перемещаем ячейки
		if (GetDistance(this.selectedVirus, cell) == this.currentPlayer.CaptureDistance)
			CopyVirus(this.selectedVirus, cell.Row, cell.Col);
		else
			MoveVirus(this.selectedVirus, cell.Row, cell.Col);

		this.currentPlayer.StepCount++;

		selectedVirus.Controller.Unselect();

		selectedVirus = null;

		// Находим все соседнии вражеские ячейки
		List<Figure> neighborViruses = GetNeighborViruses(cell.Row, cell.Col, this.currentPlayer.CaptureDistance);

		// Захватываем вирусы
		foreach (Figure neighborVirus in neighborViruses)
		{
			if (neighborVirus.Player != this.players[currentPlayerIndex])
			{
				yield return new WaitForSeconds(0.2f);

				CaptureVirus(this.currentPlayer, neighborVirus);
			}
		}

		SwitchNextPlayer();

		StartPhase();
	}

	public void OnPlayerVirusClicked(Figure virus)
	{
		// Снимаем подсветку с соседних ячеек
		UnhighlightAll();

		// Снимаем выделение со всех ячеек
		UnselectAll();

		// Выделяем текущий вирус

		virus.Controller.Select();

		this.selectedVirus = virus;

		GetComponent<AudioSource>().PlayOneShot(SelectAudio);

		// Подсвечиваем соседнии ячейки
		List<Cell> neighborCells = GetNeighborEmptyCells(virus.Row, virus.Col, this.currentPlayer.MoveDistance);

		foreach (Cell neighborCell in neighborCells)
			neighborCell.Highlight();
	}

	public void cell_MouseEnter(Cell cell)
	{
		if (cell == null)
			throw new ArgumentNullException("cell");

		if (this.battleState != BattleState.Play)
			return;

		if (this.Grid[cell.Row, cell.Col] == null && 
			this.selectedVirus != null && GetDistance(cell, this.selectedVirus) <= this.currentPlayer.MoveDistance)
			cell.Focus();
	}


	private void UnselectCell(Cell cell)
	{
		selectedVirus = null;
	}

	public void GameOver()
	{
		this.battleState = BattleState.GameOver;

		this.LosePanel.SetActive(true);
	}

	public void Win()
	{
		this.battleState = BattleState.YouWin;

		this.WinPanel.SetActive(true);

		int maxScore = this.players.Sum(x => x.Score);

		int emptyCellCount = GetEmptyCellCount();

		for (int i = 0; i < this.Grid.GetLength(0); i++)
		{
			for (int j = 0; j < this.Grid.GetLength(1); j++)
			{
				if (this.Grid[i, j] == null)
					emptyCellCount++;
			}
		}

		LevelStats stats = new LevelStats(true, GameManager.Instance.CurrentLevel, this.currentPlayer.Score,
			maxScore, this.currentPlayer.StepCount, emptyCellCount);

		EventAggregator.OnLevelWon(stats);
	}

	private int GetEmptyCellCount()
	{
		int emptyCellCount = 0;

		for (int i = 0; i < this.Grid.GetLength(0); i++)
		{
			for (int j = 0; j < this.Grid.GetLength(1); j++)
			{
				if (this.Grid[i, j] == null)
					emptyCellCount++;
			}
		}

		return emptyCellCount;
	}

	private bool HasEmptyCells()
	{
		for (int i = 0; i < this.Grid.GetLength(0); i++)
		{
			for (int j = 0; j < this.Grid.GetLength(1); j++)
			{
				if (this.Grid[i, j] == null)
					return true;
			}
		}

		return false;
	}

	public List<Cell> GetNeighborEmptyCells(byte row, byte col, byte distanse)
	{
		if (row < 0)
			throw new ArgumentOutOfRangeException("row");

		if (col < 0)
			throw new ArgumentOutOfRangeException("col");

		if (distanse <= 0)
			throw new ArgumentOutOfRangeException("distanse");

		List<Cell> list = new List<Cell>((distanse + 1) * (distanse + 1));

		for (int i = Math.Max(row - distanse, 0); i <= Math.Min(row + distanse, this.Grid.GetLength(0) - 1); i++)
		{
			for (int j = Math.Max(col - distanse, 0); j <= Math.Min(col + distanse, this.Grid.GetLength(1) - 1); j++)
			{
				if (!(row == i && col == j) && this.Grid[i, j] == null)
				{
					list.Add(allCells[i, j]);
				}
			}
		}

		return list;
	}

	public List<Figure> GetNeighborViruses(byte row, byte col, byte distanse)
	{
		if (row < 0)
			throw new ArgumentOutOfRangeException("row");

		if (col < 0)
			throw new ArgumentOutOfRangeException("col");

		if (distanse <= 0)
			throw new ArgumentOutOfRangeException("distanse");

		List<Figure> list = new List<Figure>((distanse + 1) * (distanse + 1));

		for (byte i = (byte)Math.Max(row - distanse, 0); i <= Math.Min(row + distanse, this.Grid.GetLength(0) - 1); i++)
		{
			for (byte j = (byte)Math.Max(col - distanse, 0); j <= Math.Min(col + distanse, this.Grid.GetLength(1) - 1); j++)
			{
				if (!(row == i && col == j))
				{
					Figure virus = GetVirus(i, j);

                    if (virus != null && virus.Player != this.currentPlayer)
						list.Add(this.Grid[i, j]);
				}
			}
		}

		return list;
	}

	public byte GetDistance(ICell fromCell1, ICell toCell)
	{
		if (fromCell1 == null)
			throw new ArgumentNullException("fromCell1");

		if (toCell == null)
			throw new ArgumentNullException("toCell");

		return (byte)Math.Max(Math.Abs(fromCell1.Row - toCell.Row), Math.Abs(fromCell1.Col - toCell.Col));
	}

	public List<Figure> GetPlayerViruses(PlayerController player)
	{
		if (player == null)
			throw new ArgumentNullException("player");

		List<Figure> viruses = new List<Figure>();

		for(byte r = 0; r < this.Grid.GetLength(0); r++)
		{
			for (byte c = 0; c < this.Grid.GetLength(1); c++)
			{
				Figure virus = GetVirus(r, c);

				if (virus != null && virus.Player == player)
					viruses.Add(virus);
			}
		}

		return viruses;
	}

	public void CaptureVirus(PlayerController player, Figure captureVirus)
	{
		if (player == null)
			throw new ArgumentNullException("virus");

		if (captureVirus == null)
			throw new ArgumentNullException("captureVirus");

		captureVirus.SetPlayer(player);
	}

	public void MoveVirus(Figure virus, byte row, byte col)
	{
		if (virus == null)
			throw new ArgumentNullException("virus");

		this.Grid[virus.Row, virus.Col] = null;

		virus.Controller.Move(row, col);

		this.Grid[row, col] = virus;
	}

	public void CopyVirus(Figure virus, byte row, byte col)
	{
		if (virus == null)
			throw new ArgumentNullException("virus");

		Figure newVirus = this.VirusPool.Get();

		newVirus.Initialize(virus.Player, row, col, virus.Type);

		this.Grid[newVirus.Row, newVirus.Col] = newVirus;
	}

	public Figure GetVirus(byte row, byte col)
	{
		Figure figure = this.Grid[row, col];

		if (figure != null && figure.IsVirus)
			return figure;

		return null;
	}

	public void UnselectAll()
	{
		for (byte r = 0; r < this.Grid.GetLength(0); r++)
		{
			for (byte c = 0; c < this.Grid.GetLength(1); c++)
			{
				Figure virus = GetVirus(r, c);

				if (virus != null)
					virus.Controller.Unselect();
			}
		}
	}

	public void UnhighlightAll()
	{
		for (int i = 0; i < this.allCells.GetLength(0); i++)
			for (int j = 0; j < this.allCells.GetLength(1); j++)
				this.allCells[i, j].Unhighlight();
	}
}
