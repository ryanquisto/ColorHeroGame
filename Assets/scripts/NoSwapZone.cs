using UnityEngine;
using System.Collections;

public class NoSwapZone : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            player_control.NoSwapZone = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            player_control.NoSwapZone = false;
        }
    }
}
