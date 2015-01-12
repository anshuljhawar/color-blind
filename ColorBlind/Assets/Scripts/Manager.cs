using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public Text Score;
	public Text Timer;
	[HideInInspector]
	public int level = 2;
	[HideInInspector]
	public Color color = Color.red;
	[HideInInspector]
	public int score = 0;
	[HideInInspector]
	public int timer = 30;

	private float startTime, currentTime;

	void Start () {
		startTime = Time.time;
		Score.text = ""+score;
		Timer.text = ""+timer;
	}

	void Update () {
		currentTime = Time.time - startTime;
		timer = Mathf.CeilToInt(timer - currentTime);
		if(timer <=0){
			//End Game
		} 
		else
			Timer.text = ""+timer;

	}

	void RecreateGrid(int level){
		//Recreate level x level grid
		//Change color
	}

	public void LevelUp(){
		level++;
		score++;
		timer = 30;
		Score.text = ""+score;
		Timer.text = ""+timer;
		RecreateGrid(level);
	}

}
