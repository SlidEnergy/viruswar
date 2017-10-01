using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private PlayerController[] players;

	public Text score0;
	public Text score1;
	public Text score2;
	public Text score3;

	void Update () {

		if (this.players == null)
			return;
		
		if(players.Length > 0 && players[0] != null)
			score0.text = "Score: " + players[0].Score;

		if (players.Length > 1 && players[1] != null)
			score1.text = "Score: " + players[1].Score;

		if (players.Length > 2 && players[2] != null)
			score2.text = "Score: " + players[2].Score;

		if (players.Length > 3 && players[3] != null)
			score3.text = "Score: " + players[3].Score;
	}

	public void SetPlayers(PlayerController[] players)
	{
		this.players = players;
	}
}
