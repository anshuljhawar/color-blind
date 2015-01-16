using UnityEngine;
using System.Collections;

public class GoogleAnalyticsScript : MonoBehaviour {

	public GoogleAnalyticsV3 googleAnalytics;

	// Use this for initialization
	void Start () {
		// Reports that the user is viewing the Main Menu
		if (googleAnalytics)
			googleAnalytics.LogScreen("Main Menu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
