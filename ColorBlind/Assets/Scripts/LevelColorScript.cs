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

	public static Color GetLevelSecondColor(int level, int difficulty, Color levelColor ){

		Color levelSecondColor = new Color (levelColor.r, levelColor.g, levelColor.b);
		float max = Mathf.Min(levelColor.r,levelColor.g,levelColor.b);
		if(max == levelColor.r)
			levelSecondColor.r = ChangeColorComponent(level, difficulty, levelColor.r);
		else if( max == levelColor.g)
			levelSecondColor.g = ChangeColorComponent(level, difficulty, levelColor.g);
		else
			levelSecondColor.b = ChangeColorComponent(level, difficulty, levelColor.b);

		return levelSecondColor;
	}

	private static float ChangeColorComponent(int level, int difficulty, float colorComponent){
		float levelDiff = (UPPER_LIMIT - (level + difficulty) * 2);
		levelDiff = levelDiff > 4 ? levelDiff : 4;
		float temp = colorComponent - levelDiff/255;
		return temp;
	}

}
