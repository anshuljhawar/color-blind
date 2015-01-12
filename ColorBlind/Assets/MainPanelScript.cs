using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainPanelScript : MonoBehaviour {

	public Manager manager;
	public Text Score;
	public Text HighScore;

	private int highScore;

	public void UpdateScore(int score){
		this.Score.text = "Score: " + score;
		if (score > highScore)
			highScore = score;
		this.HighScore.text = "HighScore: " + highScore;
	}

	public void SwitchToGame(){
		manager.ResetGame ();
		manager.gameObject.SetActive (true);
		this.gameObject.SetActive (false);
	}
}
