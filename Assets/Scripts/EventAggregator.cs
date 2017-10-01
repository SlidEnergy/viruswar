using UnityEngine;
using System.Collections;

public class EventAggregator : MonoBehaviour
{
	public delegate void LevelWonEventHandler(LevelStats stats);
	public static event LevelWonEventHandler LevelWon;

	public static void OnLevelWon(LevelStats stats)
	{
		if(LevelWon != null)
			LevelWon(stats);
	}
}
