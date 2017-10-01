using UnityEngine;
using System.Collections;
using System;

public class Figure : MonoBehaviour, ICell
{
	public byte Row { get; set; }
	public byte Col { get; set; }

	public bool Focused;
	public bool Selected;

	public FigureType Type { get; set; }

	public bool IsVirus
	{
		get
		{
			return this.Type == FigureType.Player1Virus || this.Type == FigureType.Player2Virus || this.Type == FigureType.Player3Virus || this.Type == FigureType.Player4Virus;
		}
	}

	public VirusController Controller;
	public FigureGraphics Graphics;
	public PlayerController Player { get; private set; }
	public LevelManager Board;

	void Awake()
	{
		this.Graphics = GetComponent<FigureGraphics>();
		this.Controller = GetComponent<VirusController>();
		this.Board = GameObject.Find("board").GetComponent<LevelManager>();
	}

	public void SetPlayer(PlayerController player)
	{
		if (player == null)
			throw new ArgumentNullException("player");

		this.Player = player;

		this.Graphics.UpdateSprite();
	}

	public void Initialize(PlayerController player, byte row, byte col, FigureType type)
	{
		//if (player == null)
		//    throw new ArgumentNullException("player");

		if (row < 0)
			throw new ArgumentOutOfRangeException("row");

		if (col < 0)
			throw new ArgumentOutOfRangeException("col");

		this.Player = player;
		this.Row = row;
		this.Col = col;
		this.Type = type;

		this.Graphics.UpdateSprite();
		this.Graphics.UpdatePosition();
	}
}
