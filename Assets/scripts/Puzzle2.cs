using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Puzzle2 : Puzzle {
    private MazeLightProbe LightProbeGreen;
    private MazeLightProbe LightProbePurple;
    private MazeLightProbe LightProbeOrange;
	// Use this for initialization
	void Start () {
        LightProbeGreen = transform.Find("lightprobe_green").GetComponent<MazeLightProbe>();
        LightProbePurple = transform.Find("lightprobe_purple").GetComponent<MazeLightProbe>();
        LightProbeOrange = transform.Find("lightprobe_orange").GetComponent<MazeLightProbe>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public override void Reset()
    {
        TimeRemaining = TimeToSolve;
        transform.Find("playerpiece_green").GetComponent<ChessPiece>().ResetPos();
        transform.Find("playerpiece_purple").GetComponent<ChessPiece>().ResetPos();
        transform.Find("playerpiece_orange").GetComponent<ChessPiece>().ResetPos();
        LightProbeGreen.Reset();
        LightProbePurple.Reset();
        LightProbeOrange.Reset();
        transform.Find("MazeSpikeTrigger1").GetComponent<MazeSpikeTrigger>().Reset();
        transform.Find("MazeSpikeTrigger2").GetComponent<MazeSpikeTrigger>().Reset();
        transform.Find("MazeSpikeTrigger3").GetComponent<MazeSpikeTrigger>().Reset();
    }

    public override void Solution()
    {
        if (LightProbeGreen.Solved && LightProbeOrange.Solved && LightProbePurple.Solved)
            Solve();
    }
}
