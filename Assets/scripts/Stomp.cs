using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour {
    private const float MOVE_AMOUNT = 6.5f;
    public int speed;
    public float WaitTime;
    public float StartHeight;
    private bool MovingUp;
    private float StartPos;
    private float EndPos;
    private bool NewWait = true;
    public enum StartMotions
    {
        Up,
        Down,
    };
    public StartMotions StartingDirection;

	// Use this for initialization
	void Start () {
        StartPos = gameObject.transform.position.y;
        EndPos = StartPos - MOVE_AMOUNT;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, StartPos - MOVE_AMOUNT * (1-StartHeight), gameObject.transform.position.z);
        if (StartingDirection == StartMotions.Down)
            MovingUp = false;
        else
            MovingUp = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (NewWait)
        {
            if (MovingUp && gameObject.transform.position.y < StartPos)
            {
                gameObject.transform.Translate(new Vector3(0f, 0f, speed / 100f));
            }
            else if (!MovingUp && gameObject.transform.position.y > EndPos)
            {
                gameObject.transform.Translate(new Vector3(0f, 0f, speed / -100f));
            }
            else
            {
                MovingUp = !MovingUp;
                NewWait = false;
                Invoke("ResumeMovement", WaitTime);
            }
        }
        else
            return;

	}

    private void ResumeMovement()
    {
        NewWait = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "player")
            GameController.DoGameOver("You got squished!");
    }
}
