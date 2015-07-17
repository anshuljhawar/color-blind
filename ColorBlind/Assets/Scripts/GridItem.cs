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

	Animator animator;

	public float invisibleTime = 0;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
		image = this.gameObject.GetComponent <Image>();
		timer = MAX_HINT_TIMER;
		hintColor = Color.white;

		animator = GetComponent<Animator>();
		this.gameObject.GetComponent<Image> ().enabled = false;
	}

	void Update(){

		if (invisibleTime > 0) {
			invisibleTime -= Time.deltaTime;
			return;
		}else{
			animator.SetBool("visible", true);
			this.gameObject.GetComponent<Image> ().enabled = true;
		}

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
//			this.ShowHint();
			animator.SetBool("hintactive", true);
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
