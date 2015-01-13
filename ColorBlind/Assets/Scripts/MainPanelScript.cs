using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class MainPanelScript : MonoBehaviour {

	public Manager manager;
	public Text Score;
	public Text HighScore;

	private Dictionary<string,int> highScore;

	void Start(){
		highScore = new Dictionary<string,int>();
		highScore["Easy"] = PlayerPrefs.GetInt("HighScoreEasy",0);
		highScore["Medium"] = PlayerPrefs.GetInt("HighScoreMedium",0);
		highScore["Hard"] = PlayerPrefs.GetInt("HighScoreHard",0);
		this.HighScore.text = "HighScore: " + highScore[manager.difficultyLevel.ToString()];
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit(); 
	}

	public bool UpdateScore(int score){
		this.Score.text = "Score: " + score;
		if (score > highScore[manager.difficultyLevel.ToString()]){
			highScore[manager.difficultyLevel.ToString()] = score;
			PlayerPrefs.SetInt("HighScore"+manager.difficultyLevel.ToString(), highScore[manager.difficultyLevel.ToString()]);
			this.HighScore.text = "HighScore: " + highScore[manager.difficultyLevel.ToString()];
			return true;
		}
		this.HighScore.text = "HighScore: " + highScore[manager.difficultyLevel.ToString()];
		return false;
	}

	public void ShowHighScore(string difficulty){
		this.HighScore.text = "HighScore: " + highScore[difficulty];
	}

	public void SwitchToGame(){
		manager.ResetGame ();
		manager.gameObject.SetActive (true);
		this.gameObject.SetActive (false);
	}

}
