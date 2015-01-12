using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridItem : MonoBehaviour {

	Manager manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
	}

	public void ButtonClick(){

		Image image = this.gameObject.GetComponent <Image>();

		if(!image.color.Equals(manager.levelColor)){
			manager.LevelUp();
		}
	}
}
