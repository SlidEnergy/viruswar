using UnityEngine;
using System.Collections;

public class LevelInfo
{
	public int Level { get; private set; }
	public int BoardSize { get; private set; }

	public int PlayerCount { get; private set; }

	public int MoveDistance { get; private set; }

	public int EnemyDifficult { get; private set; }

	public FigureType[,] Figures { get; private set; }

	public bool Block { get; set; }

	public LevelInfo(int level, int boardSize, int playerCount, int moveDistance, int enemyDifficult, FigureType[,] figures)
	{
		this.Level = level;
		this.BoardSize = boardSize;
		this.PlayerCount = playerCount;
		this.MoveDistance = moveDistance;
		this.EnemyDifficult = enemyDifficult;

		this.Figures = figures;
	}
}
