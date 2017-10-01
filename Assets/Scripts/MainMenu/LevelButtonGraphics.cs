using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LevelButtonGraphics : MonoBehaviour
{
	public int Level { get; set; }

	public Sprite Score1Sprite;

	public Sprite Score2Sprite;

	public Sprite Score3Sprite;

	public Image StarsImage;
	public Image LockImage;

	void Awake()
	{
		Image[] children = GetComponentsInChildren<Image>(true);

		foreach (Image child in children)
		{
			if (child.name == "Score")
				this.StarsImage = child;

			if (child.name == "Lock")
				this.LockImage = child;
		}
	}

	// Use this for initialization
	void Start ()
	{
		LevelInfo info = GameManager.Instance.GetLevelInfo(Level);

		LevelStats stats = GameManager.Instance.GetLevelStats(Level);

		if (stats != null && stats.Compleated)
		{
			this.StarsImage.gameObject.SetActive(true);
			this.LockImage.gameObject.SetActive(false);

			if (stats == null)
				throw new NullReferenceException("Статистика завершенного уровня недоступна.");

			if (stats.EmptyCellCount > 0)
				StarsImage.sprite = Score3Sprite;
			else if (stats.Score - stats.MaxScore < 3)
				StarsImage.sprite = Score2Sprite;
			else
				StarsImage.sprite = Score3Sprite;
		}
		else
		{
			this.StarsImage.gameObject.SetActive(false);

			this.LockImage.gameObject.SetActive(info.Block);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
