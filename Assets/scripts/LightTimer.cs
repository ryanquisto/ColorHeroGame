using UnityEngine;
using System.Collections;

public class LightTimer : MonoBehaviour {
	public int LevelTime;
	private Color SkyStartColor;
	private Color LightStartColor;
	private Color DeathColor = new Color(0.5f, 0.5f, 0.5f, 1f);
	private Color CurrentLightColor;
	private Color CurrentSkyColor;
	private Light CurrentLight;
	private float TimeRemaining;
	private float TimeCount;
	private bool GameOver;

	// Use this for initialization
	void Start () {
		CurrentLight = GetComponent<Light> ();
		TimeRemaining = LevelTime;
		LightStartColor = CurrentLight.color;
		SkyStartColor = LevelColors.SkyColors [Application.loadedLevel];
	}
	
	// Update is called once per frame
	void Update () {
		//CurrentSkyColor = new Color(DeathColor.r * (LevelTime - TimeRemaining) + SkyStartColor.r * TimeRemaining, DeathColor.g * (LevelTime - TimeRemaining) + SkyStartColor.g * TimeRemaining, DeathColor.b * (LevelTime - TimeRemaining) + SkyStartColor.b * TimeRemaining, 1);
		//CurrentLightColor = new Color(DeathColor.r * (LevelTime - TimeRemaining) + LightStartColor.r * TimeRemaining, DeathColor.g * (LevelTime - TimeRemaining) + LightStartColor.g * TimeRemaining, DeathColor.b * (LevelTime - TimeRemaining) + LightStartColor.b * TimeRemaining, 1);
		CurrentLightColor = Color.Lerp (LightStartColor, DeathColor, TimeCount/LevelTime);
		CurrentSkyColor = Color.Lerp (SkyStartColor, DeathColor, TimeCount / LevelTime);
		CurrentLight.color = CurrentLightColor;
		RenderSettings.skybox.SetColor ("_Tint", CurrentSkyColor);
		TimeRemaining -= Time.deltaTime;
		TimeCount += Time.deltaTime;
		if (TimeRemaining <= 0 && !GameOver) {
			GameOver = true;
			CurrentLight.color = LightStartColor;
			RenderSettings.skybox.SetColor ("_Tint", SkyStartColor);
			GameController.DoGameOver("The light has faded...");
		}
	}
    public void Reset()
    {
        TimeRemaining = LevelTime;
    }

    public void AddTime(int time)
    {
        TimeRemaining += time;
        TimeCount -= time;
    }
}
