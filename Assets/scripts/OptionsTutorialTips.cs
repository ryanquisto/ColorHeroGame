using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsTutorialTips : MonoBehaviour {
	public enum TutorialPref{
		None,
		NewOnly,
		All
	}
	public TutorialPref Choice;
	private Text UIText;
	// Use this for initialization
	void Start () {
		if (GameController.AlwaysWantTutorialTips)
			AdjustTutorialSetting (TutorialPref.All);
		else if (GameController.ShowingTutorialTips)
			AdjustTutorialSetting (TutorialPref.NewOnly);
		else
			AdjustTutorialSetting (TutorialPref.None);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AdjustTutorialSetting(TutorialPref choice){
		UIText = transform.FindChild ("Panel").gameObject.transform.FindChild("ChoiceText").GetComponent<Text>();
		switch (choice){
		case TutorialPref.None:
			UIText.text = "Never";
			GameController.ShowingTutorialTips = false;
			GameController.AlwaysWantTutorialTips = false;
			break;
		case TutorialPref.NewOnly:
			UIText.text = "New Only";
			GameController.ShowingTutorialTips = true;
			GameController.AlwaysWantTutorialTips = false;
			break;
		case TutorialPref.All:
			UIText.text = "Always";
			GameController.ShowingTutorialTips = true;
			GameController.AlwaysWantTutorialTips = true;
			break;
		}
		Choice = choice;
		GameObject.Find ("_GameController").GetComponent<GameController> ().SaveTutorialPref (Choice);
	}

	public void DoRightArrow(){
		Choice += 1;
		if (Choice > TutorialPref.All)
			Choice = (TutorialPref)0;
		AdjustTutorialSetting (Choice);
	}
	
	public void DoLeftArrow(){
		Choice -= 1;
		if (Choice < TutorialPref.None)
			Choice = (TutorialPref)2;
		AdjustTutorialSetting (Choice);
		
	}

}
