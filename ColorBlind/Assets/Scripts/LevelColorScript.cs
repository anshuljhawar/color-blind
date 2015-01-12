using UnityEngine;
using System.Collections;
using System;

public class LevelColorScript {

	private static float UPPER_LIMIT = 100;

	public static Color GetLevelColor(){

		System.Random random = new System.Random();
		float red = 100 + random.Next(155);
		float green = 100 + random.Next(155);
		float blue = 100 + random.Next(155);

		Color color = new Color (red/255, green/255, blue/255);

		return color;
	}

	public static Color GetLevelSecondColor(int level, Color levelColor ){

		Color levelSecondColor = new Color (levelColor.r, levelColor.g, levelColor.b);

		System.Random random = new System.Random ();
		int colorToModify = random.Next (2);

		switch (colorToModify) {
		case 0:
			levelSecondColor.r = ChangeColorComponent(level, levelColor.r);
			break;
		case 1:
			levelSecondColor.g = ChangeColorComponent(level, levelColor.g);
			break;
		case 2:
			levelSecondColor.b = ChangeColorComponent(level, levelColor.b);
			break;
		}

		return levelSecondColor;
	}

	private static float ChangeColorComponent(int level, float colorComponent){
		float temp = colorComponent - (UPPER_LIMIT - level * 2)/255;
		return temp > 0 ? temp : 0;
	}

}
