using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MainPanelScript : MonoBehaviour {

	public Manager manager;
	public Text Score;
	public Text HighScore;

	private int highScore;

	void Start(){
		highScore = PlayerPrefs.GetInt("HighScore");
		this.HighScore.text = "HighScore: " + highScore;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	public void UpdateScore(int score){
		this.Score.text = "Score: " + score;
		if (score > highScore)
			highScore = score;
		this.HighScore.text = "HighScore: " + highScore;
		
		PlayerPrefs.SetInt("HighScore", highScore);
	}

	public void SwitchToGame(){
		manager.ResetGame ();
		manager.gameObject.SetActive (true);
		this.gameObject.SetActive (false);
	}
}
