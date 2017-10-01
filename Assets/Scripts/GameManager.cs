using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public int CurrentLevel { get; private set;}

	private List<LevelInfo> levels;

	private Dictionary<int, LevelStats> levelStats;

	private GameSaver gameSaver;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
		{
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

			return;
		}

		InitGame();

		EventAggregator.LevelWon += EventAggregator_LevelWon;
    }

	void Start () {

	}

  	private void InitGame()
	{
		CurrentLevel = 1;

		LevelsGenerator levelGenerator = new LevelsGenerator();

		levels = levelGenerator.Generate();

		this.gameSaver = new GameSaver(Application.persistentDataPath + "/game.dat");

		LevelStats[] stats = this.gameSaver.Load();

		this.levelStats = new Dictionary<int, LevelStats>();

		if (stats != null && stats.Length > 0)
		{
			for (int i = 0; i < stats.Length; i++)
				if(!this.levelStats.ContainsKey(stats[i].Level) && stats[i].Level != 0)
					this.levelStats.Add(stats[i].Level, stats[i]);
		}

		RecalcBlockLevels();
	}

	private void RecalcBlockLevels()
	{
        int unblockLevelCount = 3;

		foreach (LevelInfo level in this.levels)
		{
			if (this.levelStats.ContainsKey(level.Level) && this.levelStats[level.Level].Compleated)
				level.Block = false;
			else if (unblockLevelCount > 0)
			{
				level.Block = false;
				unblockLevelCount--;
			}
			else
				level.Block = true;
		}
	}


	// Update is called once per frame
	void Update () {
	
	}

	public List<LevelInfo> GetLevels()
	{
		return this.levels;
	}

	private void EventAggregator_LevelWon(LevelStats stats)
	{
		this.levelStats[this.CurrentLevel] = stats;

		this.gameSaver.Save(new List<LevelStats>(this.levelStats.Values).ToArray());

		RecalcBlockLevels();
	}

	public LevelInfo GetCurrentLevelInfo()
	{
		return levels[CurrentLevel - 1];
	}

	public LevelInfo GetLevelInfo(int level)
	{
		if (level < 1 || level > levels.Count)
			throw new System.ArgumentOutOfRangeException("level", "Указанный уровень не существует");

		return this.levels[level - 1];
	}

	public LevelStats GetLevelStats(int level)
	{
		if (level < 1 || level > levels.Count)
			throw new System.ArgumentOutOfRangeException("level", "Указанный уровень не существует");

		if (!this.levelStats.ContainsKey(level))
			return null;

		return this.levelStats[level];
	}

	public void RestartLevel()
	{
		LoadCurrentLevel();
	}

	public void LoadCurrentLevel()
	{
		Application.LoadLevel("Level");
	}

	public void LoadLevel(int level)
	{
		if (levels.Count >= level)
		{
			CurrentLevel = level;

			RestartLevel();
		}
	}

	public void NextLevel()
	{
		CurrentLevel++;

		RestartLevel();
	}

	public void LoadMenu()
	{
		Application.LoadLevel("MainMenu");
	}
}
