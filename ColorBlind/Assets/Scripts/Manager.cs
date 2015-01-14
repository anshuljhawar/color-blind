using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public Text Score;
	public Text Timer;
	
	private int level = 2;
	[HideInInspector]
	public ToggleValue.ToggleValues difficultyLevel = ToggleValue.ToggleValues.Easy;
	private int difficulty = 0;
	
	public Color levelColor = Color.red;
	public Color levelSecondColor;
	
	private int score = 0;
	private float timer = 0;

	public GridLayoutGroup gridLayoutGroup;
	public GameObject gridItemPrefab;

	public int MAX_TIMER = 45;
	private int MAX_GRID_SIZE = 8;
	private float GRID_PANEL_SIZE;

	public MainPanelScript mainPanel;
	public TimeUpPanelScript timeUpPanel;

	void Start () {
//		float screenWidth = Screen.width;
//		float screenHeight = Screen.height;
//		float panelWidth = this.gridLayoutGroup.GetComponent<RectTransform> ().rect.width / 2;
//
//		float left = (screenWidth - this.gridLayoutGroup.GetComponent<RectTransform> ().rect.width) / 2;
//		float top = (screenHeight - this.gridLayoutGroup.GetComponent<RectTransform> ().rect.width) / 2;
//
//		this.gridLayoutGroup.GetComponent<RectTransform>().rect.Set(left, top, panelWidth, panelWidth);

		GRID_PANEL_SIZE = this.gridLayoutGroup.GetComponent<RectTransform> ().rect.width;
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			this.EndGame();
		}
			
		if(timer>0){
			timer -= Time.deltaTime;
			if(timer <=0){
				Timer.text = "Time UP!!";
				TimeUp();
			} 
			else
				Timer.text = "Time Left: "+Mathf.CeilToInt(timer);
		}

	}

	void RecreateGrid(int level){
		//Recreate level x level grid
		//Change color

		//Clear the layout 
		int childs = this.gridLayoutGroup.transform.childCount;
		
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(this.gridLayoutGroup.transform.GetChild(i).gameObject);
		}

		//Get color for levels
		this.levelColor = LevelColorScript.GetLevelColor ();
		this.levelSecondColor = LevelColorScript.GetLevelSecondColor (level,difficulty, levelColor);

		int gridSize = level > MAX_GRID_SIZE ? MAX_GRID_SIZE : level;

		float cellSpace = (GRID_PANEL_SIZE - 3 * this.gridLayoutGroup.padding.left);
		float cellSize = cellSpace / (gridSize);

		this.gridLayoutGroup.cellSize = new Vector2(cellSize,cellSize);

		this.gridLayoutGroup.spacing = new Vector2(((float)this.gridLayoutGroup.padding.left) / (gridSize - 1), ((float)this.gridLayoutGroup.padding.left) / (gridSize - 1));
	
		System.Random random = new System.Random ();
		int secondColorGridItemPosition = random.Next (gridSize * gridSize);

		for(int i =0; i < gridSize * gridSize ; i++){
			GameObject gridItem = (GameObject) GameObject.Instantiate(gridItemPrefab);
			RectTransform t = gridItem.GetComponent<RectTransform>();
			t.SetParent(this.gridLayoutGroup.transform);
			t.localScale = Vector3.one;

			if(i == secondColorGridItemPosition)
				gridItem.GetComponent<Image>().color = levelSecondColor;
			else
				gridItem.GetComponent<Image>().color = levelColor;
		}
	}

	public void LevelUp(){
		level++;
		score++;
		Score.text = "Score: "+score;
		RecreateGrid(level);
	}

	public void ResetGame(){
		timer = MAX_TIMER;
		score = 0;
		level = 2;
		Score.text = "Score: "+score;
		Timer.text = "Time Left: "+timer;
		this.RecreateGrid (level);
	}

	public void SetDifficultyLevel(ToggleValue.ToggleValues value){
		difficultyLevel = value;
		mainPanel.ShowHighScore(difficultyLevel.ToString());
		switch(difficultyLevel){
		case ToggleValue.ToggleValues.Hard:
			difficulty = 30;
			break;
		case ToggleValue.ToggleValues.Medium:
			difficulty = 15;
			break;
		default:
			difficulty = 0;
			break;
		}
	}
	public void TimeUp(){
		timeUpPanel.SetPanel(mainPanel.UpdateScore(score));
		this.gameObject.SetActive (false);

	}

	public void EndGame(){
		mainPanel.gameObject.SetActive (true);
		mainPanel.UpdateScore(score);
		this.gameObject.SetActive (false);
	}

}
