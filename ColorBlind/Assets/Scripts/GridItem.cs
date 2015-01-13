using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridItem : MonoBehaviour {

	Manager manager;
	Image image;

	public int MAX_HINT_TIMER = 5;
	private float timer = 0;

	private bool showHint = false;
	private float lerpTime = 0;

	private Color fromColor;
	private Color toColor;
	private Color hintColor;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
		image = this.gameObject.GetComponent <Image>();
		timer = MAX_HINT_TIMER;
		hintColor = Color.white;
	}

	void Update(){
		if(!image.color.Equals(manager.levelColor)){
			timer -= Time.deltaTime;
			if(timer <=0){
				if(this.showHint == false){
					this.showHint = true;
					fromColor = image.color;
					toColor = hintColor;
				}
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

		image.color = Color.Lerp (fromColor, toColor, lerpTime);

		if (lerpTime < 1) {
			lerpTime += Time.deltaTime;
		}else{
			lerpTime = 0;
			if(fromColor == hintColor){
				fromColor = manager.levelSecondColor;
				toColor = hintColor;
			}else{
				fromColor = hintColor;
				toColor = manager.levelSecondColor;
			}

		}

	}
}
