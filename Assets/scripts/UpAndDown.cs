using UnityEngine;
using System.Collections;

public class UpAndDown : MonoBehaviour {
    public int MoveAmount;
    private float StartHeight;
    private bool InvokeMovingUp;
    private bool TransformUp;
    private bool InvokeMovingDown;
    private bool TransformDown;
    private float multiplier;
    private bool MoreThanHalfWay;
	// Use this for initialization
	void Start () {
        StartHeight = transform.position.y;
        InvokeMovingUp = true;
        multiplier = 1f;
	}

    // Update is called once per frame
    void Update()
    {
        if (InvokeMovingDown)
        {
            Invoke("StartMoveDown", 0.1f);
            InvokeMovingDown = false;

        }
        if (InvokeMovingUp)
        {
            Invoke("StartMoveUp", 0.1f);
            InvokeMovingUp = false;
        }
        if (TransformUp)
        {
            //if (MoreThanHalfWay)
            //    multiplier *= 0.95f;
           // else
             //   multiplier *= 1/0.95f;
            transform.Translate(0f, Time.deltaTime * MoveAmount * 0.1f, 0f);
           // if (Mathf.Abs(transform.position.y - (StartHeight + 0.5f * MoveAmount)) < 0.2f)
              //  MoreThanHalfWay = true;
            if (transform.position.y >= StartHeight + MoveAmount)
            {
                InvokeMovingDown = true;
                TransformUp = false;
                //MoreThanHalfWay = false;
                //multiplier = 1f;
            }
        }
        if (TransformDown)
        {
            //if (MoreThanHalfWay)
            //    multiplier *= 0.95f;
            //else
              //  multiplier *= 1 / 0.95f;
            transform.Translate(0f, Time.deltaTime * MoveAmount * -0.1f, 0f);
           // if (Mathf.Abs(transform.position.y - (StartHeight + 0.5f * MoveAmount)) < 0.2f)
             //   MoreThanHalfWay = true;
            if (transform.position.y <= StartHeight)
            {
                InvokeMovingUp = true;
                TransformDown = false;
              //  MoreThanHalfWay = false;
              //  multiplier = 1f;
            }
        }
    }

    void StartMoveUp()
    {
        TransformUp = true;
    }
    void StartMoveDown()
    {
        TransformDown = true;
    }
}
