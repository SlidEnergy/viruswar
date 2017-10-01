using System;

[Serializable]
public class LevelStats
{
	public bool Compleated;
	public int Level;
	public int Score;
	public int MaxScore;
	public int EmptyCellCount;
	public int Steps;

	public LevelStats(bool compleated, int level, int score, int maxScore, int steps, int emptyCellCount)
	{
		this.Compleated = compleated;
		this.Level = level;
		this.Score = score;
		this.MaxScore = maxScore;
		this.Steps = steps;
		this.EmptyCellCount = emptyCellCount;
	}
}
