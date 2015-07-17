using UnityEngine;
using System.Collections;
using System.IO;

public class AndroidShare {

	#if UNITY_ANDROID
	private static AndroidJavaObject playerActivityContext = null;
	#endif

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
			pluginClass.CallStatic("sendText", playerActivityContext, highScore);
		}
		#endif
	}

	public static void ShareScreenShotWithHighScoreDirect(string highscore){

		#if UNITY_ANDROID

		// create the texture
		Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		
		// put buffer into texture
		screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		
		// apply
		screenTexture.Apply();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		
		byte[] dataToSave = screenTexture.EncodeToPNG();
		
//		string destination = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");

		string destination =  "/sdcard/highscore.png";

		File.WriteAllBytes(destination, dataToSave);
		
		if(!Application.isEditor)
		{
			// block to open the file and share it ------------START
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destination);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), highscore);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), highscore);
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			
			// option one:
			currentActivity.Call("startActivity", intentObject);
			
			// option two:
			//AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "YO BRO! WANNA SHARE?");
			//currentActivity.Call("startActivity", jChooser);
			
			// block to open the file and share it ------------END
			
		}

		#endif
	}

	public static void ShareScreenShotWithHighScore (string highScore){

		#if UNITY_ANDROID && !UNITY_EDITOR
		string filePath = TakeScreenShot ();

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
			pluginClass.CallStatic("sendImageWithCaption", playerActivityContext, filePath, highScore);
		}
		#endif
	}

	private static string TakeScreenShot(){

		/*string fileName = "highscore.png";
		Application.CaptureScreenshot (fileName);

		return Application.persistentDataPath + "/" + fileName;
		*/

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

//		string destination = Path.Combine(Application.persistentDataPath , "highscore.png");

		string destination = "/sdcard/highscore.png";

		File.WriteAllBytes(destination, bytes);

		return "file://" + destination;

	}
}
