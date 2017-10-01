using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class AI
{
	// Можно придумать больше уровней сложности
	// 1 - полный рандом с тупым алгоритмом
	// 2 - худший вариант с тупым алгоритмом
	// 3 - средний вариант с тупым алгоритмом
	// 4 - хороший вариант с тупым алгоритмом
	// 4.5 - рандом с хорошим алгоритмом
	// 5 - худший вариант с хорошим алгоритмом
	// 6 - средний вариант с хорошим алгоритмом
	// 7 - хороший вариант с хорошим алгоритмом

	protected LevelManager levelManager;

	protected PlayerController player;

	public AI(LevelManager levelManager, PlayerController player)
	{
		this.levelManager = levelManager;

		this.player = player;
	}

	public abstract MoveInfo CalcMove();

	protected int CalcScore(Figure virus, Cell toCell)
	{
		if (virus == null)
			throw new ArgumentNullException("virus");

		if (toCell == null)
			throw new ArgumentNullException("toCell");

		int score = 0;

		// 1. Calculate move score. clone = 1, move = 0
		byte distance = this.levelManager.GetDistance(virus, toCell);

		if (distance == this.player.CaptureDistance)
			score++;

		// 2. Calculate fighting score.
		List<Figure> neighborViruses = this.levelManager.GetNeighborViruses(toCell.Row, toCell.Col, this.player.CaptureDistance);

		foreach (Figure neighborVirus in neighborViruses)
			if (neighborVirus.Player != this.player)
				score++;

		return score;
	}
}
