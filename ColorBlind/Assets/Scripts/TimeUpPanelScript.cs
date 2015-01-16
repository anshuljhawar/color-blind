using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUpPanelScript : MonoBehaviour {

	public Text Score;
	public Text HighScore;
	public Text DifficultyLevel;
	public GameObject newHighScore;
	public Manager manager;

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) 
			this.HomeButtonClick();
	}

	public void SetPanel(bool showShare){
		Score.text = manager.mainPanel.Score.text;
		HighScore.text = manager.mainPanel.HighScore.text;
		DifficultyLevel.text = manager.difficultyLevel.ToString ();
		newHighScore.SetActive(showShare);
		this.gameObject.SetActive(true);
	}
	public void ShareButtonClick(){
		string text = "Awesome! I made a new " + HighScore.text + " in Color Blind!";
		text = text + " Check this out https://www.dropbox.com/s/oa1a2es8yys1c6m/colorblind.apk?dl=0";
//		AndroidShare.ShareHighScore(text);
		AndroidShare.ShareScreenShotWithHighScoreDirect (text);
	}

	public void HomeButtonClick(){
		this.gameObject.SetActive(false);
		manager.EndGame();
	}

	public void SwitchToGame(){
		manager.ResetGame ();
		manager.gameObject.SetActive (true);
		this.gameObject.SetActive (false);
	}

}
