using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlternatePlayer : MonoBehaviour {
    private GameObject player;
    private bool InRange;
    private bool ShowError;
    float TextRiseAmount;
    private string ErrorMessage;
    private bool BallFellDown;
    public int SwapRecharge;
    public int TeleportRechange;
    private float TimeUntilSwap;
    private float TimeUntilTeleport;
    private Text SwapTimer;
    private Text TeleportTimer;
	// Use this for initialization
	void Start () {
        
        if (Inventory.FoundAlternatePlayer)
        {
            transform.Translate(0, -100f, 0);
            AlternatePlayerEnabled();
            BallFellDown = true;
            MoveDecoyToPlayer(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -100)
        {
            BallFellDown = true;
            MoveDecoyToPlayer(false);
        }
        if (GameController.BallPanel.activeSelf)
        {
            if (TimeUntilTeleport > 0)
            {
                TimeUntilTeleport -= Time.deltaTime;
                TeleportTimer.text = ((int)TimeUntilTeleport).ToString();
            }
            else
                TeleportTimer.enabled = false;
            if (TimeUntilSwap > 0)
            {
                TimeUntilSwap -= Time.deltaTime;
                SwapTimer.text = ((int)TimeUntilSwap).ToString();
            }
            else
                SwapTimer.enabled = false;
        }
    }

    public void SwapBalls(bool ResetTimer=true)
    {
        if (TimeUntilSwap <= 0)
        {
            if (player_control.NoSwapZone)
            {
                Messenger.SendMessage("You can't swap here! Try again later.");
                return;
            }
            var Collisions = Physics.OverlapSphere(player.transform.position, 1.1f);
            bool CanSwap = false;
            for (int i = 0; i < Collisions.Length; i++)
            {
                if (Collisions[i].gameObject.name.Contains("teleporter"))
                {   //the teleporting player can't move, launching the other one into the air
                    CanSwap = false;
                    break;
                }
                if (Collisions[i].gameObject.name.Contains("floor") || Collisions[i].gameObject.name.Contains("button"))
                {
                    CanSwap = true;
                }
            }
            if (!CanSwap)
            {
                Messenger.SendMessage("Can't swap if you're not on the ground!");
            }
            else
            {
                Vector3 PlayerLocation = player.transform.position;
                player.transform.position = transform.position;
                transform.position = PlayerLocation;
                if (ResetTimer)
                {
                    TimeUntilSwap = SwapRecharge;
                    SwapTimer.enabled = true;
                }
            }
        }
        else
            Messenger.SendMessage("Skill recharging.");
    }

    //checks for an open spot near the player to move the decoy,
    //moving in a circular motion starting w/ 1 radius, giving up 
    //at radius of 3
    public void MoveDecoyToPlayer(bool ResetTimer = true)
    {
        if (TimeUntilTeleport <= 0)
        {
            bool FoundSpot = false;
            Vector3 TargetLocation;
            float SearchRadius = 1f;
            float angle = Mathf.PI / -4;
            float XOff;
            float ZOff;
            bool FoundFloor = false;
            bool FoundBadCollision = false;
            while (!FoundSpot)
            {
                angle += Mathf.PI / 4;    //check every pi/4 angle
                FoundFloor = false;
                FoundBadCollision = false;
                if (SearchRadius > 3)  //At this point, tell the player there is no nearby spot to place the ball
                {
                    if (!BallFellDown)
                        Messenger.SendMessage("Cannot find a spot for the ball. Try again later.");
                    FoundSpot = true;
                }
                if (angle > Mathf.PI * 2)   //reset once you reach 2pi, expanding the radius
                {
                    angle = 0;
                    SearchRadius += 0.2f;
                }

                XOff = Mathf.Sin(angle) * SearchRadius;
                ZOff = Mathf.Cos(angle) * SearchRadius;
                TargetLocation = player.transform.position + new Vector3(XOff, 5, ZOff);
                var Collisions = Physics.OverlapSphere(TargetLocation + new Vector3(0, -5.3f), 1.3f);
                for (int i = 0; i < Collisions.Length; i++)
                {
                    if (Collisions[i].gameObject.name.Contains("floor"))    //must find floor to land on
                    {
                        FoundFloor = true;
                    }
                    else        //can't land on top of any other collision
                    {
                        FoundBadCollision = true;
                    }
                }
                if (FoundFloor && !FoundBadCollision)   //good to go, teleport
                {
                    transform.position = TargetLocation;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    FoundSpot = true;
                    FoundBadCollision = false;
                    BallFellDown = false;
                    if (ResetTimer)
                    {
                        TimeUntilTeleport = TeleportRechange;
                        TeleportTimer.enabled = true;
                    }
                }

            }
        }
        else
            Messenger.SendMessage("Skill Recharging.");
    }



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("player") && !Inventory.FoundAlternatePlayer)
        {
            player = col.gameObject;
            GameController.ShowTutorialTip("Hi there!", "I want to help you! Control me with the buttons above!", new Vector3(0.5f, 0.6f), "", true);
            AlternatePlayerEnabled();
            Inventory.FoundAlternatePlayer = true;
        }

    }

    public void AlternatePlayerEnabled()
    {
        GameController.BallPanel.SetActive(true);
        Destroy(GetComponent<BoxCollider>());
        player = GameObject.Find("player");
        SwapTimer = GameObject.Find("SwapTimer").GetComponent<Text>();
        TeleportTimer = GameObject.Find("TeleportTimer").GetComponent<Text>();
        SwapTimer.enabled = false;
        TeleportTimer.enabled = false;
        TimeUntilSwap = 0;
        TimeUntilTeleport = 0;
    }

}
