using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Puzzle : MonoBehaviour {
    public GameObject Door;
    public float TimeToSolve;
    private bool IsTimed;
    public bool IsSolved;
    public bool IsActive;
    public float TimeRemaining;

    public abstract void Solution();
    public abstract void Reset();   //item does this
    public void StartPuzzle()
    {
        if (TimeToSolve > 0)
        {
            GameController.Timer.SetActive(true);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            if (TimeToSolve > 0)
            {
                IsTimed = true;
                TimeRemaining = TimeToSolve;
            }
            StartPuzzle();
            IsActive = true;
            Destroy(GetComponent<BoxCollider>());
            if (IsTimed)
                Invoke("OutOfTime", TimeToSolve);
            Messenger.SendMessage("PUZZLE STARTED");
            GameController.ActivePuzzle = this;
        }
    }
    public void Solve()
    {
        Messenger.SendMessage("Good Job!");
        IsSolved = true;
        IsActive = false;
        CancelInvoke("OutOfTime");
        GameController.Timer.SetActive(false);
        GameController.ActivePuzzle = null;
        //open door
        Destroy(Door);
    }
    public void OutOfTime()
    {
        IsSolved = false;
        IsActive = false;
        GameController.DoGameOver("You ran out of time!");
    }
}
