using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;

public class PlayerController : MonoBehaviour {

	public AILevel AILevel { get; private set; }

	public Color Color { get; private set; }

	public LevelManager levelManager;

	public bool IsVictim;

	public bool IsLose;

	public byte MoveDistance;
	public byte CaptureDistance;

	private AI ai;

	public int Score
	{
		get
		{
			return this.levelManager.GetPlayerViruses(this).Count;
		}
	}

	public int StepCount;

	void Awake()
	{
		levelManager = GameObject.Find("board").GetComponent<LevelManager>();
	}

	// Use this for initialization
	void Start () {
		
	}


	public void Initialize(Color color, AILevel aiLevel)
	{
		this.Color = color;
		this.AILevel = aiLevel;

		switch (aiLevel)
		{
			case AILevel.Easy:
				this.ai = new EasyAI(this.levelManager, this);
				break;

			case AILevel.Normal:
				this.ai = new NormalAI(this.levelManager, this);
				break;

			case AILevel.Hard:
				this.ai = new HardAI(this.levelManager, this);
				break;

			default:
				throw new ArgumentOutOfRangeException("Текущий уровень сложности не поддерживается. AILevel = " + aiLevel);
		}
	}

	public bool CanPlayMore()
	{
		foreach (Figure virus in this.levelManager.GetPlayerViruses(this))
		{
			if (levelManager.GetNeighborEmptyCells(virus.Row, virus.Col, this.MoveDistance).Count > 0)
				return true;
		}

		return false;
	}

	public void Lose()
	{
		this.IsLose = true;

		this.Color = new Color(this.Color.r < 0.1f ? 0.8f : this.Color.r,
			this.Color.g < 0.1f ? 0.8f : this.Color.g,
			this.Color.b < 0.1f ? 0.8f : this.Color.b,
			this.Color.a);
	}

	public IEnumerator MoveAI()
	{
		MoveInfo move = this.ai.CalcMove();

		if (move == null)
			throw new ArgumentNullException("move", "Не удалось рассчитать ход. CalcMove вернул null.");

		if(move.ToCell == null)
			throw new ArgumentNullException("move.ToCell", "Клетка для хода не указана.");

		// Задержка перед выбором вируса
		yield return new WaitForSeconds(0.2f);

		// Выбираем вирус.
		move.Virus.Controller.Select();

		// Задержка перед ходом игрока
        yield return new WaitForSeconds(0.5f);

		// Копируем или перемещаем ячейки
		if (levelManager.GetDistance(move.Virus, move.ToCell) == this.CaptureDistance)
			this.levelManager.CopyVirus(move.Virus, move.ToCell.Row, move.ToCell.Col);
		else
			this.levelManager.MoveVirus(move.Virus, move.ToCell.Row, move.ToCell.Col);

		// Находим все соседнии вражеские ячейки
		List<Figure> neighborViruses = levelManager.GetNeighborViruses(move.ToCell.Row, move.ToCell.Col, this.CaptureDistance);

		// Захватываем ячейки
		foreach (Figure virus in neighborViruses)
		{
			// Задержка перед захватом вируса.
			yield return new WaitForSeconds(0.2f);

			this.levelManager.CaptureVirus(this, virus);
		}
	}
}
