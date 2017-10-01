using UnityEngine;

public class VirusInput : MonoBehaviour {

	private Figure figure;
	private LevelManager board;

	void Awake()
	{
		this.figure = GetComponent<Figure>();
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
		if (this.board.battleState != BattleState.Play)
			return;

		if (this.board.currentPlayer.IsVictim && this.board.currentPlayer == this.figure.Player)
		{
			this.board.OnPlayerVirusClicked(figure);
		}
	}

	public void OnMouseEnter()
	{
		if (this.board.battleState != BattleState.Play)
			return;

		if (this.board.currentPlayer == this.figure.Player)
			figure.Controller.Focus();
	}

	public void OnMouseExit()
	{
		if (this.board.battleState != BattleState.Play)
			return;

		this.figure.Controller.Unfocus();
	}
}
