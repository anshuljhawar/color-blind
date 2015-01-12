using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public Text Score;
	public Text Timer;
	
	private int level = 4;
	
	public Color levelColor = Color.red;
	public Color levelSecondColor;
	
	private int score = 0;
	private int timer = 30;

	public GridLayoutGroup gridLayoutGroup;
	public GameObject gridItemPrefab;

	private float startTime, currentTime;

	private int MAX_GRID_SIZE = 8;
	private int GRID_PANEL_SIZE;

	void Start () {
		startTime = Time.time;
		Score.text = ""+score;
		Timer.text = ""+timer;

		GRID_PANEL_SIZE = (int)this.gridLayoutGroup.GetComponent<RectTransform> ().rect.width;

		this.RecreateGrid (level);
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

		//Clear the layout 
		int childs = this.gridLayoutGroup.transform.childCount;
		
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.Destroy(this.gridLayoutGroup.transform.GetChild(i).gameObject);
		}

		//Get color for levels
		this.levelColor = LevelColorScript.GetLevelColor ();
		this.levelSecondColor = LevelColorScript.GetLevelSecondColor (level, levelColor);

		int gridSize = level > MAX_GRID_SIZE ? MAX_GRID_SIZE : level;

		int cellSpace = (GRID_PANEL_SIZE - 3 * this.gridLayoutGroup.padding.left);
		int cellSize = cellSpace / (gridSize);

		this.gridLayoutGroup.cellSize = new Vector2(cellSize,cellSize);

		this.gridLayoutGroup.spacing = new Vector2(this.gridLayoutGroup.padding.left / (level - 1),this.gridLayoutGroup.padding.left / (level - 1));
	
		System.Random random = new System.Random ();
		int secondColorGridItemPosition = random.Next (gridSize * gridSize);

		for(int i =0; i < gridSize * gridSize ; i++){
			GameObject gridItem = (GameObject) GameObject.Instantiate(gridItemPrefab);

			gridItem.GetComponent<RectTransform>().SetParent(this.gridLayoutGroup.transform);

			if(i == secondColorGridItemPosition)
				gridItem.GetComponent<Image>().color = levelSecondColor;
			else
				gridItem.GetComponent<Image>().color = levelColor;
		}
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
