using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyAI : AI
{
	public EasyAI(LevelManager levelManager, PlayerController player) : base(levelManager, player) { }

	public override MoveInfo CalcMove()
	{
		List<MoveInfo> moves = new List<MoveInfo>();

		foreach (Figure virus in this.levelManager.GetPlayerViruses(this.player))
		{
			int bestScore = -1;
			Cell cellTheBest = null;

			List<Cell> neighborCells = this.levelManager.GetNeighborEmptyCells(virus.Row, virus.Col,
				this.player.MoveDistance);

			if (neighborCells.Count == 0)
				continue;

			foreach (Cell toCell in neighborCells)
			{
				int score = CalcScore(virus, toCell);

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

		return FindRandomCellForMove(moves);
	}

	private MoveInfo FindRandomCellForMove(List<MoveInfo> cellScores)
	{
		System.Random rnd = new System.Random();

		int index = rnd.Next(0, cellScores.Count);

		return cellScores[index];
	}
}
