using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Puzzle1 : Puzzle {
    private FlameColor Torch1;
    private FlameColor Torch2;
    private FlameColor Torch3;
	// Use this for initialization
	void Start () {
        Door = transform.Find("Door").gameObject;
        Torch1 = transform.Find("Torch1").GetComponentInChildren<FlameColor>();
        Torch2 = transform.Find("Torch2").GetComponentInChildren<FlameColor>();
        Torch3 = transform.Find("Torch3").GetComponentInChildren<FlameColor>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public override void Solution()
    {
        if (Torch1.GetColorCount().Equals("Color2") && Torch2.GetColorCount().Equals("Color3") && Torch3.GetColorCount().Equals("Color1"))
        {
            Solve();
        }
    }

    public override void Reset()
    {
        TimeRemaining = TimeToSolve;
        Torch1.SetColor("Color1");
        Torch2.SetColor("Color1");
        Torch3.SetColor("Color1");
    }
}
