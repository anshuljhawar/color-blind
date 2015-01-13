using UnityEngine;
using System.Collections;

public class AndroidShare {

	private static AndroidJavaObject playerActivityContext = null;
	public static void ShareHighScore(string highScore){

	}

	public static void ShareHighScore (string highScore){
		#if UNITY_ANDROID && !UNITY_EDITOR
		Debug.Log("public static void ShareHighScore (ELANNotification notification) --> " + "AndroidShare" );
		// Obtain unity context
		if(playerActivityContext == null) {
			AndroidJavaClass actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			playerActivityContext = actClass.GetStatic<AndroidJavaObject>("currentActivity");
		}
		
		// Set notification within java plugin
		AndroidJavaClass pluginClass = new AndroidJavaClass("com.pranjh.androidshareproject.ShareManager");
		
		if (pluginClass != null) {
			Debug.Log("plugin class  --> " + " Not Null" );
			pluginClass.CallStatic("sendText", playerActivityContext, "Sample Text");
		}
		#endif
	}
}
