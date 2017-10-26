using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedoPotion : Item
{
    // Use this for initialization
    void Start()
    {
        CheckForDuplicateID();
		image = Sprite.Create (Resources.Load ("potion_undo") as Texture2D, new Rect (0, 0, 128, 128), Vector2.zero);
		Stacks = true;
		DestroyOnUse = true;
		description = "Restarts the active puzzle";
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCollect();
        if (GetComponent<MeshRenderer>())
        {
            transform.Rotate(new Vector3(0, 0.5f, 1), 3);
        }
    }



    public override void UseItem()
    {
        if (GameController.ActivePuzzle != null)
        {
            GameController.ActivePuzzle.Reset();
            Messenger.SendMessage("Reset Puzzle!");
        }
        else
        {
            Messenger.SendMessage("No active puzzle");
            GameController.CollectItem(this);
        }
    }



}
