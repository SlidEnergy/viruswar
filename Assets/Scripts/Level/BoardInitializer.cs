using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BoardInitializer : MonoBehaviour
{

	private VirusPool virusPool;

	private RectTransform thisRectTransform;

	private LevelManager board;

	public GameObject[] score;

	private byte captureDistance = 1;

	void Awake()
	{
		this.thisRectTransform = GetComponent<RectTransform>();
		this.board = GetComponent<LevelManager>();
		this.virusPool = GetComponent<VirusPool>();
	}

	// Use this for initialization
	void Start()
	{

		LevelInfo level = GameManager.Instance.GetCurrentLevelInfo();

		FigureType[,] figures = Reverse(level.Figures);

		InitializeCellGrid(figures);

		// Создаем игроков

		PlayerController[] players = InitializePlayers(level.PlayerCount, (AILevel)level.EnemyDifficult, (byte)level.MoveDistance, captureDistance);

		Initialize((byte)level.BoardSize);

		for (int i = 0; i < figures.GetLength(0); i++)
		{
			for (int j = 0; j < figures.GetLength(1); j++)
			{
				FigureType figure = figures[i, j];

				if (figure == FigureType.Player1Virus || figure == FigureType.Player2Virus || figure == FigureType.Player3Virus || figure == FigureType.Player4Virus)
					InitializeVirus(players[(int)figure - 1], (byte)i, (byte)j, figure);

				if (figure == FigureType.Block)
					InitializeBlock((byte)i, (byte)j);
			}
		}
	}

	private FigureType[,] Reverse(FigureType[,] figures)
	{
		FigureType[,] array = new FigureType[figures.GetLength(0), figures.GetLength(1)];

		for (int i = 0; i < figures.GetLength(0); i++)
		{
			for (int j = 0; j < figures.GetLength(1); j++)
			{
				array[figures.GetLength(0) - 1 - i, j] = figures[i, j];
			}
		}

		return array;
	}

	public void Initialize(byte boardSize)
	{
		byte figureSize = (byte)(thisRectTransform.rect.width / boardSize);

		virusPool.SetFigureSize(figureSize);
	}

	public void InitializeVirus(PlayerController player, byte row, byte col, FigureType figureType)
	{
		if (player == null)
			throw new ArgumentNullException("player");

		Figure virus = virusPool.CreateVirus();

		if (virus == null)
			throw new Exception("Не удалось создать вирус");

		virus.Initialize(player, row, col, figureType);

		this.board.Grid[row, col] = virus;
	}

	public void InitializeBlock(byte row, byte col)
	{
		Figure block = virusPool.CreateVirus();

		if (block == null)
			throw new Exception("Не удалось создать вирус");

		block.Initialize(null, row, col, FigureType.Block);

		this.board.Grid[row, col] = block;
	}

	public void InitializeCellGrid(FigureType[,] figures)
	{
		if (figures == null)
			throw new ArgumentNullException();

		this.board.Grid = new Figure[figures.GetLength(0), figures.GetLength(1)];

		this.board.Graphics.SetCells(figures);
	}

	public PlayerController[] InitializePlayers(int count, AILevel aiLevel, byte moveDistance, byte captureDistance)
	{
		PlayerController[] players = new PlayerController[count];

		for (int i = 0; i < count; i++)
		{
			GameObject obj = new GameObject(string.Format("Player{0}", i));
			PlayerController player = obj.AddComponent<PlayerController>();

			switch (i)
			{
				case 0:
					player.Initialize(Color.blue, aiLevel);
					break;
				case 1:
					player.Initialize(Color.red, aiLevel);
					break;
				case 2:
					player.Initialize(Color.green, aiLevel);
					break;
				case 3:
					player.Initialize(Color.magenta, aiLevel);
					break;
			}

			player.MoveDistance = moveDistance;
			player.CaptureDistance = captureDistance;

			players[i] = player;
		}

		if (count < 4)
			for (int i = count; i < 4; i++)
				score[i].SetActive(false);

		players[0].IsVictim = true;

		board.SetPlayers(players);

		return players;
	}
}
