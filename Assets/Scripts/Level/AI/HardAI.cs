using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HardAI : AI
{
	public HardAI(LevelManager levelManager, PlayerController player) : base(levelManager, player) { }

	public override MoveInfo CalcMove()
	{
		List<MoveInfo> moves = new List<MoveInfo>();

		foreach (Figure virus in this.levelManager.GetPlayerViruses(this.player))
		{
			int bestScore = -1;
			Cell cellTheBest = null;

			int basePositionScore = this.levelManager.GetNeighborEmptyCells(virus.Row, virus.Col,
				this.player.CaptureDistance).Count;

			List<Cell> neighborCells = this.levelManager.GetNeighborEmptyCells(virus.Row, virus.Col,
				this.player.MoveDistance);

			if (neighborCells.Count == 0)
				continue;

			foreach (Cell toCell in neighborCells)
			{
				int score = CalcScore(virus, toCell);

				score += basePositionScore;

				if (score > bestScore)
				{
					cellTheBest = toCell;
					bestScore = score;
				}
			}

			MoveInfo move = new MoveInfo();
			move.Score = bestScore;
			move.Virus = virus;
			move.ToCell = cellTheBest;

			moves.Add(move);
		}

		return FindBestCellForMove(moves);
	}

	private MoveInfo FindBestCellForMove(List<MoveInfo> cellScores)
	{
		int bestScore = 0;

		foreach (MoveInfo cellScore in cellScores)
		{
			if (bestScore < cellScore.Score)
				bestScore = cellScore.Score;
		}

		List<MoveInfo> topScores = cellScores.FindAll(info => info.Score == bestScore);

		System.Random rnd = new System.Random();

		int index = rnd.Next(0, topScores.Count);

		return topScores[index];
	}
}
