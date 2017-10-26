using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public static Vector3 PlayerStartPos;
	public GameObject GameOver;
	private GameObject TutorialPanel;
	public static bool ShowingTutorialTips = true;
	public static bool AlwaysWantTutorialTips;
	public static bool PlayMusic = true;
	public static bool PlaySoundEffects = true;
	private AudioSource music; 
	public static int LightCubeCount;
	public int LightCubesInLevel;
	public GameObject LightCube;
	private List<AudioSource> SoundEffects = new List<AudioSource>();
	public static float EffectVolume;
	public static bool MusicEnabled;
	public static bool EffectsEnabled;
	public static bool HasWrench = false;
    public static Puzzle ActivePuzzle;
    //UI elements
    public static GameObject CollectPanel;
    public static GameObject BallPanel;
    public static GameObject BookTextPanel;
    public static GameObject Timer;
    

	// Use this for initialization
	void Start () {
        
        PlayerStartPos = GameObject.Find("player").transform.position;
		GameOver.SetActive (false);
		GameController.LightCubeCount = 0;
		TutorialPanel = GameObject.Find ("TutorialTip");
		TutorialPanel.SetActive (false);
		music = GameObject.Find ("GameMusic").GetComponent<AudioSource>();
		ReadPreferences();
		SpawnLightCubes ();
		GetAllAudioSources ();
		CollectPanel = GameObject.Find ("CollectPanel");
		CollectPanel.SetActive (false);
        BallPanel = GameObject.Find("BallPanel");
        BallPanel.SetActive(false);
        BookTextPanel = GameObject.Find("BookHintText");
        BookTextPanel.SetActive(false);
        Timer = GameObject.Find("Clock");
        Timer.SetActive(false);
        ActivePuzzle = null;
	}


	
	// Update is called once per frame
	void Update () {

        //Update puzzle timer and check if it's been solved
        if (ActivePuzzle != null)
        {
            ActivePuzzle.TimeRemaining -= Time.deltaTime;
            GameController.Timer.transform.Find("Text").GetComponent<Text>().text = ((int)ActivePuzzle.TimeRemaining).ToString();
            ActivePuzzle.Solution();
        }
        
	}

	public static void RestartGame()
	{
		Application.LoadLevel (0);
		GameObject.Find ("_GameController").GetComponent<GameController>().ResumeGame();
	}

	public static void RestartLevel(){
		Application.LoadLevel (Application.loadedLevel);
		GameObject.Find ("_GameController").GetComponent<GameController>().ResumeGame();
        //Inventory.RecollectItems();
        //for (int i = 0; i < Inventory.CollectedItems.Count; i++)
        //{
           // GameObject[] PossibleMatches = GameObject.FindGameObjectsWithTag("Item");
           // for (int j = 0; j < PossibleMatches.Length; j++)
           // {
                //PossibleMatches[j].GetComponent<Item>().CheckForCollect(true);
                //Destroy(PossibleMatches[j].gameObject);     //wtf
              //  if (PlayerPrefs.GetString("Item" + i.ToString()).Equals(PossibleMatches[j].GetComponent<Item>().ID))
              //  {
                    //PossibleMatches[j].GetComponent<Item>().CheckForCollect(true);    //infinite loop?
                    //Destroy(PossibleMatches[j].gameObject);
              //  }

           // }
        //}
    }

	public static void ExitGame()
	{
		Application.Quit ();
	}

	public static void DoGameOver(string reason = null)
	{
		GameController controller = (GameController) GameObject.Find ("_GameController").GetComponent<GameController>();
		controller.GameOver.SetActive (true);
		controller.PauseGame ();
		if (reason != null){
			Text ReasonText = (Text) GameObject.Find ("GameOverReason").GetComponent<Text>();
			ReasonText.text = reason;
		}
		player_control.CanMove = false;
		GameController.LightCubeCount = 0;
	}

	public static void AdvanceLevel(){
		GameController controller = (GameController) GameObject.Find ("_GameController").GetComponent<GameController>();
		try{
			GameController.LightCubeCount = 0;
			Application.LoadLevel (Application.loadedLevel + 1);
		}
		catch{
			controller.DoWin();
		}
	}

	public void PauseGame(){
		Time.timeScale = 0f;
	}

	public void ResumeGame(){
		Time.timeScale = 1f;
	}


	//not yet implemented
	void DoWin(){
	}

    /// <summary>
    /// Returns 1 if passed true or -1 if passed false.
    /// </summary>
    /// <param name="myBool">Parameter value to pass.</param>
    /// <returns>Returns an integer based on the passed value.</returns>
    public static void ShowTutorialTip(string Header, string Text, Vector3 Location,  string ID, bool PausesGame = false) 
	{
		if ((!GameObject.Find ("_GameController").GetComponent<GameController>().TextInFile(ID, "TutorialHistory.txt.dll") && GameController.ShowingTutorialTips)|| GameController.AlwaysWantTutorialTips || ID.Equals("")) {
			if (PausesGame) 
				GameObject.Find ("_GameController").GetComponent<GameController>().PauseGame ();
			GameObject Panel = (GameObject)GameObject.Find ("_GameController").GetComponent<GameController> ().TutorialPanel;
			Panel.SetActive (true);
			int WidthMultiplier = Screen.width;
			int HeightMultiplier = Screen.height;
			Panel.transform.position = new Vector3 (Location.x * WidthMultiplier, Location.y * HeightMultiplier, Location.z);
			Text HeaderText = (Text)GameObject.Find ("TutorialHeaderText").GetComponent<Text> ();
			HeaderText.text = Header;
			Text TutorialText = (Text)GameObject.Find ("TutorialText").GetComponent<Text> ();
			TutorialText.text = Text;
			if (GameObject.Find ("_GameController").GetComponent<GameController>().IsInvoking("ExitTutorialTip"))
			    GameObject.Find ("_GameController").GetComponent<GameController>().CancelInvoke();
			if (!PausesGame)
				GameObject.Find ("_GameController").GetComponent<GameController>().Invoke("ExitTutorialTip", 6f);
		}
	}

	//does the same thing as clicking the exit button
	void ExitTutorialTip(){
        try {
            GameObject.Find("TutorialScrollbar").GetComponent<Scrollbar>().value = 1;
            GameObject.Find("TutorialTip").SetActive(false);
        }
        catch { }
	}


	void ReadPreferences(){
		string[] settings = File.ReadAllLines (Application.dataPath + "/Plugins/Settings.txt.dll");
		for (int i=0; i<settings.Length; i++) {
			if(settings[i].Contains("ShowTutorialTips")){
				if(settings[i].Contains ("True"))
					GameController.ShowingTutorialTips = true;
				else
					GameController.ShowingTutorialTips = false;
			}
			else if(settings[i].Contains("AlwaysWantTutorialTips")){
				if(settings[i].Contains ("True"))
					GameController.AlwaysWantTutorialTips = true;
				else
					GameController.AlwaysWantTutorialTips = false;
			}
			else if(settings[i].Contains("MusicVolume")){
				SetMusicVolume(float.Parse(settings[i].Substring(settings[i].IndexOf("=")+1)));

			}
			
			else if(settings[i].Contains("EffectVolume")){
				SetEffectVolume(float.Parse(settings[i].Substring(settings[i].IndexOf("=")+1)));
			}

			else if(settings[i].Contains("WantMusic")){
				bool choice = false;
				if (Convert.ToString(settings[i].Substring(settings[i].IndexOf("=")+1)).Contains("True")){
					choice = true;
					music.mute = false;
				}
				else if (Convert.ToString(settings[i].Substring(settings[i].IndexOf("=")+1)).Contains ("False")){
					choice = false;
					music.mute = true;
				}
				GameController.MusicEnabled = choice;
			}
			else if(settings[i].Contains("WantEffects")){
				bool choice = false;
				if (Convert.ToString(settings[i].Substring(settings[i].IndexOf("=")+1)).Contains("True")){
					choice = true;
					GetAllAudioSources();
					for (int j=0; j<SoundEffects.Count; j++) {
						SoundEffects [j].mute = false;
						choice = true;
					}
				}
				else if (Convert.ToString(settings[i].Substring(settings[i].IndexOf("=")+1)).Contains ("False")){
					choice = false;
					GetAllAudioSources();
					for (int j=0; j<SoundEffects.Count; j++) {
						SoundEffects [j].mute = true;
						choice = false;
					}
				}
				GameController.EffectsEnabled = choice;
			}
		}
	}


	public bool TextInFile(string SearchText, string FileName){
		string TotalText = File.ReadAllText (Application.dataPath + "/Plugins/" + FileName);
		return TotalText.Contains (SearchText);
	}
	

	public void GetAllAudioSources(){
		SoundEffects.Clear ();
		GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
        for (int i = 0; i < objects.Length; i++) {
            if ((objects[i].GetComponent<AudioSource>() != null && objects[i].gameObject.name != "GameMusic"))
                SoundEffects.Add(objects[i].GetComponent<AudioSource>());
            else if (objects[i].GetComponentInChildren<AudioSource>() != null && objects[i].gameObject.name != "GameMusic")
                SoundEffects.Add(objects[i].GetComponentInChildren<AudioSource>());
        }
		if (SoundEffects.Count == 0)
			Debug.Log ("No audio sources found.");
	}

	public void SetEffectVolume(float NewVolume){
		GetAllAudioSources ();
		for (int i=0; i<SoundEffects.Count; i++) {
			SoundEffects [i].gameObject.GetComponent<AudioSource>().volume = NewVolume;
		}
		GameController.EffectVolume = NewVolume;

		string[] settings = File.ReadAllLines (Application.dataPath + "/Plugins/Settings.txt.dll");
		for (int i=0; i<settings.Length; i++) {
			if (settings [i].Contains ("EffectVolume")) {
				settings [i] = settings [i].Substring (0, (settings [i].IndexOf ("=") + 1)) + Convert.ToString (NewVolume);
			}
		}
		File.WriteAllLines (Application.dataPath + "/Plugins/Settings.txt.dll", settings);
	}

	public void SetMusicVolume(float NewVolume, bool RestartMusic = false){
	music.volume = NewVolume;
	if (RestartMusic)
		music.Play ();
		string[] settings = File.ReadAllLines (Application.dataPath + "/Plugins/Settings.txt.dll");
		if (!RestartMusic) {			//Update the saved music volume unless the music is being toggled on/off, in which case old value remains
			for (int i=0; i<settings.Length; i++) {
				if (settings [i].Contains ("MusicVolume")) {
					settings [i] = settings [i].Substring (0, (settings [i].IndexOf ("=") + 1)) + Convert.ToString (NewVolume);
				}
			}
			File.WriteAllLines (Application.dataPath + "/Plugins/Settings.txt.dll", settings);
		}
	} 

	public void ToggleAudio(string type){
		bool WantAudio = false;
		if (type.Equals ("Music")) {
			music.mute = !music.mute;
			WantAudio = !music.mute;
			GameController.MusicEnabled = !music.mute;;
		} else if (type.Equals ("Effects")) {
			GetAllAudioSources ();
			for (int i=0; i<SoundEffects.Count; i++) {
				SoundEffects [i].mute = ! SoundEffects [i].mute;
				WantAudio = !SoundEffects [i].mute;
				GameController.EffectsEnabled = !SoundEffects [i].mute;
			}
		} else 
			WantAudio = true;

		string[] settings = File.ReadAllLines (Application.dataPath + "/Plugins/Settings.txt.dll");	
		for (int i=0; i<settings.Length; i++) {
			if (settings [i].Contains ("Want" + type)) {
				settings [i] = settings [i].Substring (0, (settings [i].IndexOf ("=") + 1)) + Convert.ToString (WantAudio);
			}
		}
		File.WriteAllLines (Application.dataPath + "/Plugins/Settings.txt.dll", settings);
	}
		
	public void SaveTutorialPref(OptionsTutorialTips.TutorialPref pref){
		string ShowTutorial = "";
		string AlwaysWant = "";
		switch (pref) {
		case OptionsTutorialTips.TutorialPref.None:
			ShowTutorial = "False";
			AlwaysWant = "False";
			break;
		case OptionsTutorialTips.TutorialPref.NewOnly:
			ShowTutorial = "True";
			AlwaysWant = "False";
			break;
		case OptionsTutorialTips.TutorialPref.All:
			ShowTutorial = "True";
			AlwaysWant = "True";
			break;
		}
		string[] settings = File.ReadAllLines (Application.dataPath + "/Plugins/Settings.txt.dll");	
		for (int i=0; i<settings.Length; i++) {
			if (settings[i].Contains ("ShowTutorialTips"))
				settings[i] = settings [i].Substring (0, (settings [i].IndexOf ("=") + 1)) + ShowTutorial;
			else if (settings[i].Contains ("AlwaysWantTutorialTips"))
				settings[i] = settings [i].Substring (0, (settings [i].IndexOf ("=") + 1)) + AlwaysWant;
		}
		File.WriteAllLines (Application.dataPath + "/Plugins/Settings.txt.dll", settings);
	}


	void SpawnLightCubes(){
		GameObject[] FoundObjects = GameObject.FindGameObjectsWithTag ("LightCubeSpawnPoint");
		List<GameObject> SpawnPoints = new List<GameObject>();
		for (int i=0; i<FoundObjects.Length; i++) {
			SpawnPoints.Add (FoundObjects[i]);
		}
		if (SpawnPoints.Count < LightCubesInLevel) {
			Debug.Log ("Not enough spawn points set to place all cubes.");
			LightCubesInLevel = SpawnPoints.Count;
		}
		for (int i=0; i<LightCubesInLevel; i++) {
			int RandomIndex = UnityEngine.Random.Range(0, SpawnPoints.Count - 1);
			GameObject SpawnPoint = SpawnPoints[RandomIndex];
			GameObject cube = Instantiate (LightCube, SpawnPoint.transform.position + new Vector3(0, 2, 0), Quaternion.Euler(0,0,0)) as GameObject;
			cube.GetComponent<CubeMovement>().SetDifficulty(SpawnPoint.GetComponent<SpawnPoint>().Difficulty);
			SpawnPoints.Remove(SpawnPoint);
		}

	}

	public static void CollectItem(Item item){
        DontDestroyOnLoad(item);
		if (item.ID!=null)
        	Inventory.CollectedIDs.Add(item.ID);
        bool FoundItemStack = false;
        if (item.Stacks)
        {
            for (int i = 0; i < Inventory.CollectedItems.Count; i++)
            {
                if (item.description.Equals(Inventory.CollectedItems[i].description))
                {
                    Inventory.CollectedItems[i].SetCount(Inventory.CollectedItems[i].GetCount() + 1);
                    FoundItemStack = true;
                    break;
                }
            }
            if (!FoundItemStack)
            {
                Inventory.CollectedItems.Add(item);
                item.SetCount(1);
            }
        }
        else
        {
            Inventory.CollectedItems.Add(item);
            item.SetCount(1);
        }
		
        
	}

	public static void UseItem(Item item, bool remove=true){
		item.UseItem ();
        bool FoundItemStack = false;
        if (item.Stacks && remove)
        {
            for (int i = 0; i < Inventory.CollectedItems.Count; i++)
            {
                if (item.description.Equals(Inventory.CollectedItems[i].description))
                {
                    Inventory.CollectedItems[i].SetCount(Inventory.CollectedItems[i].GetCount() - 1);
                    FoundItemStack = true;
                    break;
                }
            }
            if (!FoundItemStack)
                item.SetCount(0);
        }
        if (remove && item.GetCount() == 0)
            Inventory.CollectedItems.Remove (item);

	}
}
