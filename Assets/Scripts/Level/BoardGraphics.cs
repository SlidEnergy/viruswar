using UnityEngine;
using System.Collections;

public class BoardGraphics : MonoBehaviour {

	/// <summary>
	/// Префаб ячейки
	/// </summary>
	public RectTransform cellPrefab;

	private RectTransform thisRectTransform;

	private LevelManager board;

	void Awake()
	{
		this.thisRectTransform = GetComponent<RectTransform>();
		this.board = GetComponent<LevelManager>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCells(FigureType[,] figures)
	{
		this.board.allCells = new Cell[figures.GetLength(0), figures.GetLength(1)];

		byte size = (byte)(thisRectTransform.rect.width / figures.GetLength(0));

		cellPrefab.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
		cellPrefab.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

		for (int i = 0; i < figures.GetLength(0); i++)
		{
			for (int j = 0; j < figures.GetLength(1); j++)
			{
				//if (figures[i, j] != FigureType.Block)
				{
					RectTransform cellTransform = Instantiate(cellPrefab, new Vector3(size / 2 + i * size, size / 2 + size * j, 0), Quaternion.identity) as RectTransform;
					cellTransform.SetParent(thisRectTransform, false);

					Cell cell = cellTransform.GetComponent<Cell>();
					cell.Row = (byte)i;
					cell.Col = (byte)j;

					this.board.allCells[i, j] = cell;
				}
			}
		}
	}
}
