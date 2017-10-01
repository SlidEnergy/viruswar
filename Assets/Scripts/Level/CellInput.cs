using UnityEngine;
using System.Collections;

public class CellInput : MonoBehaviour {

	private Cell cell;
    private LevelManager board;

	void Awake()
	{
		this.cell = GetComponent<Cell>();
		this.board = GameObject.Find("board").GetComponent<LevelManager>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseClick()
	{
		this.board.cell_Clicked(cell);
	}

	public void OnMouseEnter()
	{
        this.board.cell_MouseEnter(cell);
	}

	public void OnMouseExit()
	{
		this.cell.Unfocus();
	}
}
