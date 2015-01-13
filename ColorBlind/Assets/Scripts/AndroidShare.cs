using UnityEngine;
using System.Collections;
using System.IO;

public class AndroidShare {

	private static AndroidJavaObject playerActivityContext = null;

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

	public static void ShareScreenShotWithHighScore (string highScore){

		#if UNITY_ANDROID && !UNITY_EDITOR
		TakeScreenShot ();

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
			pluginClass.CallStatic("sendImageWithCaption", playerActivityContext, "/sdcard/color-blind/highscore.png", "Sample Text");
		}
		#endif
	}

	private static void TakeScreenShot(){

		// Create a texture the size of the screen, RGB24 format
		var width = Screen.width;
		var height = Screen.height;
		var tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		// Read screen contents into the texture
		tex.ReadPixels (new Rect(0, 0, width, height), 0, 0);
		tex.Apply ();
		// Encode texture into PNG
		var bytes = tex.EncodeToPNG();
		Object.Destroy (tex);

		File.WriteAllBytes("/sdcard/color-blind/highscore.png", bytes);

	}
}
