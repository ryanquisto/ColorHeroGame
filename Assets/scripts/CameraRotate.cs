using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
    private float radius;
    private float XStartOff;
    private float YStartOff;        
    private float ZStartOff;
    private float XOff;
    private float YOff;
    private float ZOff;
    private GameObject player;
    private GameObject cam;
    private float angle;
    private float StartAngle;
    private Vector3 LookAroundCenter;
    private Vector3 MouseOffsetFromCenter;
    private float StartTilt;
    private float XBeforeLookAt;
	// Use this for initialization
	void Start () {
        cam = gameObject;
        player = GameObject.Find("player");
        XStartOff = cam.transform.position.x - player.transform.position.x;
        YStartOff = cam.transform.position.y - player.transform.position.y;
        ZStartOff = cam.transform.position.z - player.transform.position.z;
        XOff = XStartOff;
        YOff = YStartOff;
        ZOff = ZStartOff;
        radius = Mathf.Sqrt(Mathf.Pow(XStartOff, 2f) + Mathf.Pow(ZStartOff, 2f));
        angle = Mathf.Asin(ZStartOff/radius);
        StartAngle = angle;
        StartTilt = transform.rotation.x;
   
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Input.GetMouseButtonDown(2))
        {
            LookAroundCenter = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            //Rotation around player
            MouseOffsetFromCenter = Input.mousePosition - LookAroundCenter;
            if (Mathf.Abs(MouseOffsetFromCenter.x) > 50)       //within certain range, keep camera steady
                angle -= MouseOffsetFromCenter.x / 2000;
            XOff = Mathf.Cos(angle) * radius;
            ZOff = Mathf.Sin(angle) * radius;
            YOff = YStartOff - (MouseOffsetFromCenter.y + Screen.height / 4) / 20;

        }
        else if (XOff != XStartOff || ZOff != ZStartOff || YOff != YStartOff) //released MMB
        {
            angle = StartAngle;
            XOff += (XStartOff - XOff) / 20;
            ZOff += (ZStartOff - ZOff) / 20;
            YOff += (YStartOff - YOff) / 20;
        }
        cam.transform.position = new Vector3(player.transform.position.x + XOff, player.transform.position.y + YOff, player.transform.position.z + ZOff);
        cam.transform.LookAt(player.transform);
        
    }
}
