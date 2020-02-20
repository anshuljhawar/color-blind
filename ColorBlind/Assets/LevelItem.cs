using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour {

	public int level;

	// Use this for initialization
	void Start () {
		gameObject.GetComponentInChildren<Text> ().text = level.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
