using UnityEngine;
using System.Collections;

public class VirusController : MonoBehaviour {

	private Figure figure;

	public bool Focused;
	public bool Selected;

	void Awake() {
		this.figure = GetComponent<Figure>();
	}

	public void Move(byte row, byte col)
	{
		this.figure.Row = row;
		this.figure.Col = col;

		this.figure.Graphics.UpdatePosition();
	}

	public void Remove()
	{
		this.figure.Board.VirusPool.Put(figure);
	}

	public void Unfocus()
	{
		this.Focused = false;
	}

	public void Focus()
	{
		this.Focused = true;
	}

	public void Unselect()
	{
		this.Selected = false;
	}

	public void Select()
	{
		this.Selected = true;
	}
}
