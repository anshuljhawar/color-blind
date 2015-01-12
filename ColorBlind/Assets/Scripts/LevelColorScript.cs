using UnityEngine;
using System.Collections;
using System;

public class LevelColorScript {

	int UPPER_LIMIT = 200;

	private Color GetLevelColor(){

		System.Random random = new System.Random();
		int red = random.Next(255);
		int green = random.Next(255);
		int blue = random.Next(255);

		Color color = new Color (red, green, blue);

		return color;
	}

	private Color GetLevelSecondColor(int level, Color levelColor ){

		Color levelSecondColor = new Color (levelColor.r, levelColor.g, levelColor.b);

		System.Random random = new System.Random ();
		int colorToModify = random.Next (3);

		switch (colorToModify) {
		case 0:
			levelSecondColor.r = levelColor.r - (UPPER_LIMIT - level);
			break;
		case 1:
			levelSecondColor.g = levelColor.g - (UPPER_LIMIT - level);
			break;
		case 2:
			levelSecondColor.b = levelColor.b - (UPPER_LIMIT - level);
			break;
		case 3:
			break;
		}

		return levelSecondColor;
	}

}
