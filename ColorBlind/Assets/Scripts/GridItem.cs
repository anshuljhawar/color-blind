using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridItem : MonoBehaviour {

	Manager manager;
	Image image;

	public int MAX_HINT_TIMER = 5;
	private float timer = 0;

	private bool showHint = false;
	private float hintDuration = 4;
	private float lerpTime = 0;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
		image = this.gameObject.GetComponent <Image>();
		timer = MAX_HINT_TIMER;
	}

	void Update(){
		if(!image.color.Equals(manager.levelColor)){
			timer -= Time.deltaTime;
			if(timer <=0){
				this.showHint = true;
			} 
		}

		if (showHint) {
			this.ShowHint();
		}
	}

	public void ButtonClick(){
		if(!image.color.Equals(manager.levelColor)){
			manager.LevelUp();
		}
	}

	private void ShowHint(){
//		Debug.LogError ("CAme herererer");
//		Color color = image.color;
//		color.r = color.r + 100/255;
//		image.color = manager.levelColor;

		image.color = Color.Lerp (image.color, manager.levelColor, lerpTime);

		if (lerpTime < 1) {
			lerpTime += Time.deltaTime/hintDuration;
		}
	}
}
