using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUpPanelScript : MonoBehaviour {

	public Text Score;
	public Text HighScore;
	public Text DifficultyLevel;
	public GameObject newHighScore;
	public Manager manager;

	public void setPanel(bool showShare){
		Score.text = manager.mainPanel.Score.text;
		HighScore.text = manager.mainPanel.HighScore.text;
		DifficultyLevel.text = manager.difficultyLevel.ToString ();
		newHighScore.SetActive(showShare);
		this.gameObject.SetActive(true);
	}
	public void ShareButtonClick(){
		string text = "Awesome! I made a new " + HighScore.text + " in Color Blind!";
//		AndroidShare.ShareHighScore(text);
		AndroidShare.ShareScreenShotWithHighScoreDirect (text);
	}

	public void HomeButtonClick(){
		this.gameObject.SetActive(false);
		manager.EndGame();
	}


}
