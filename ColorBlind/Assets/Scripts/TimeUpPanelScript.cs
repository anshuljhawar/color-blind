﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUpPanelScript : MonoBehaviour {

	public Text Score;
	public Text HighScore;
	public GameObject newHighScore;
	public Manager manager;

	public void setPanel(bool showShare){
		Score.text = manager.mainPanel.Score.text;
		HighScore.text = manager.mainPanel.HighScore.text;
		newHighScore.SetActive(showShare);
		this.gameObject.SetActive(true);
	}
	public void ShareButtonClick(){
		AndroidShare.ShareHighScore("Awesome! I made a new "+HighScore.text+" in Color Blind!");
	}

	public void HomeButtonClick(){
		this.gameObject.SetActive(false);
		manager.EndGame();
	}


}
