using UnityEngine;
using System.Collections;

public class camera_control : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
    private Vector3 OriginalOffset;
    private Quaternion OriginalRotation;
	private Vector3 MaxOffset;
	private Vector3 MinOffset;
	private bool IsLookingAround;
	private Vector3 LookAroundCenter;



	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
		MaxOffset = offset * 2;
		MinOffset = offset / 2;
		IsLookingAround = false;
        OriginalOffset = offset;
        OriginalRotation = transform.rotation;

	}
	
	// Update is called once per frame
	/// <summary>
    /// 
    /// </summary>
    void LateUpdate () {
		float ScrollAmount = Input.GetAxis ("Mouse ScrollWheel");
		if ((offset - offset * ScrollAmount).magnitude < MaxOffset.magnitude && (offset - offset * ScrollAmount).magnitude > MinOffset.magnitude && GameObject.Find("TutorialTip")==null)
			offset = offset - offset * ScrollAmount;
		//transform.position = player.transform.position + offset;
		//if (Input.GetKeyUp (KeyCode.C)) {
			//IsLookingAround = !IsLookingAround;
			//if (!IsLookingAround) { 
			//	transform.rotation = OriginalRotation;
             //   offset = OriginalOffset;
            //}
			//if (IsLookingAround)
			//	LookAroundCenter = Input.mousePosition;
		//}
  //      if (Input.GetMouseButtonDown(2))
  //      {
  //          transform.rotation = OriginalRotation;
  //          offset = OriginalOffset;
  //          LookAroundCenter = Input.mousePosition;
  //      }
  //          //if (IsLookingAround) {
  //          if (Input.GetMouseButton(2)) { 
  //          //Vector3 MousePosition = (Input.mousePosition - LookAroundCenter) - new Vector3(Screen.width, Screen.height, 0f);
  //          float VerticalRotation = transform.rotation.x + (LookAroundCenter.y - Input.mousePosition.y) + OriginalRotation.x;
		//	float HorizontalRotation = transform.rotation.y +  (Input.mousePosition.x - LookAroundCenter.x);
		//	if (VerticalRotation < -40)
		//		VerticalRotation = -40;
		//	else if (VerticalRotation > 85)
		//		VerticalRotation = 85;
		//	transform.rotation = Quaternion.Euler(VerticalRotation, HorizontalRotation, transform.rotation.z);
		//}
  //      if (Input.GetKeyUp(KeyCode.Escape) && IsLookingAround)
  //          IsLookingAround = false;
    }
}
