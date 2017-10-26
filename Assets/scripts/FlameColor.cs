using UnityEngine;
using System.Collections;

public class FlameColor : MonoBehaviour {
    public Color Color1;
    public Color Color2;
    public Color Color3;
    private Color CurrentColor;
    private string ColorCount;
    private bool InRange;
	// Use this for initialization
	void Start () {
        CurrentColor = Color1;
        ColorCount = "Color1";
	}
	
	// Update is called once per frame
	void Update () {
	    if (InRange && Input.GetKeyUp(KeyCode.X))
        {
            if (ColorCount.Equals("Color1")) {
                CurrentColor = Color2;
                ColorCount = "Color2";
            }
            else if (ColorCount.Equals("Color2")) {
                CurrentColor = Color3;
                ColorCount = "Color3";
            }
            else {
                CurrentColor = Color1;
                ColorCount = "Color1";
            }
            GetComponent<ParticleSystem>().startColor = CurrentColor;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        GameController.CollectPanel.SetActive(true);
        InRange = true;

    }

    void OnTriggerExit(Collider col)
    {
        GameController.CollectPanel.SetActive(false);
        InRange = false;
    }

    public Color GetCurrentColor()
    {
        return CurrentColor;
    }

    public string GetColorCount()
    {
        return ColorCount;
    }

    public void SetColor(string ColorCode) 
    { 
        if (ColorCode.Equals("Color1")) { 
            ColorCount = "Color1";
            CurrentColor = Color1;
        }
        else if (ColorCode.Equals("Color2")) {
            ColorCount = "Color2";
            CurrentColor = Color2;
        }
        else if (ColorCode.Equals("Color3"))
        {
            ColorCount = "Color3";
            CurrentColor = Color3;
        }
        GetComponent<ParticleSystem>().startColor = CurrentColor;
    }
}
