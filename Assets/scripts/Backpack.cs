using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Backpack : ClosableUIElement {
	private GameObject ItemPanel;
	private GameObject[] ItemSlots = new GameObject[8];
	public Sprite OpenBackpack;
	public Sprite ClosedBackpack;
	private Color EmptySlotColor;
    private Sprite EmptySlotImage;
    private bool ClickingItem;
    private string HoverDescription;
    private bool Hovering;
	// Use this for initialization
	void Start () {
		ItemPanel = transform.Find ("Items").gameObject;
		//find the 8 item panels to place item images
		for (int i = 0; i < 8; i++) {
			string ToFind = "Item" + i.ToString();
			ItemSlots[i] = ItemPanel.transform.Find (ToFind).gameObject;
		}
		EmptySlotColor = ItemSlots [0].GetComponent<Image> ().color;
        EmptySlotImage = ItemSlots[0].GetComponent<Image>().sprite;
		ItemPanel.SetActive (false);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (ItemPanel.activeInHierarchy)
        {
            for (int i = 0; i < Inventory.CollectedItems.Count; i++)
            {
                //x part works, y never returns true\
                if (Input.mousePosition.x >= ItemSlots[i].transform.position.x && Input.mousePosition.x <= ItemSlots[i].transform.position.x + 100 && Input.mousePosition.y <= ItemSlots[i].transform.position.y && Input.mousePosition.y >= ItemSlots[i].transform.position.y - 100)
                {
                    HoverDescription = Inventory.CollectedItems[i].description;
                    Hovering = true;
                    if (Input.GetMouseButtonUp(0))
                    {
                        GameController.UseItem(Inventory.CollectedItems[i], Inventory.CollectedItems[i].DestroyOnUse);
                        PlayerPrefs.SetString("Item" + i.ToString(), null);
                        Invoke("LoadBackpackItems", 0.2f);
                        ClickingItem = true;
                        Hovering = false;
                    }
                    break;
                }
                else
                    Hovering = false;

            }
        }
        else
            Hovering = false;

	
	}

    void OnGUI()
    {
        if (Hovering) {
            float xpos = Input.mousePosition.x + 15;
            float ypos = Screen.height - Input.mousePosition.y - 15;
            GUIStyle style = new GUIStyle();
            style.wordWrap = true;
            style.alignment = TextAnchor.UpperCenter;
            style.normal.textColor = new Color(0, 0, 0, 1);
            GUI.Label(new Rect(xpos+1, ypos, 100, 200), HoverDescription, style);
            GUI.Label(new Rect(xpos-1, ypos, 100, 200), HoverDescription, style);
            GUI.Label(new Rect(xpos, ypos+1, 100, 200), HoverDescription, style);
            GUI.Label(new Rect(xpos, ypos-1, 100, 200), HoverDescription, style);
            style.normal.textColor = new Color(1, 1, 1, 1);
            GUI.Label(new Rect(xpos, ypos, 100, 200), HoverDescription, style);
        }
    }

	public void OnButtonClick(){
        Invoke("CheckStatus", 0.1f);
		

	}

	void LoadBackpackItems(){
		for (int i=0; i<Inventory.CollectedItems.Count; i++) {
			ItemSlots[i].GetComponent<Image>().sprite = Inventory.CollectedItems [i].image;
			ItemSlots[i].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
            if (Inventory.CollectedItems[i].GetCount() > 1)
            {
                ItemSlots[i].transform.Find("Text").gameObject.GetComponent<Text>().text = Inventory.CollectedItems[i].GetCount().ToString() + " ";
            }
            else
            {
                ItemSlots[i].transform.Find("Text").gameObject.GetComponent<Text>().text = "";
            }
		}
        for (int j = Inventory.CollectedItems.Count; j < 8; j++)
        {   //make sure all empty slots have correct color and image
            ItemSlots[j].GetComponent<Image>().color = EmptySlotColor;
            ItemSlots[j].GetComponent<Image>().sprite = EmptySlotImage;
            ItemSlots[j].transform.Find("Text").gameObject.GetComponent<Text>().text = "";
        }
        ClickingItem = false;
	}

    void CheckStatus()
    {
        if (ItemPanel.activeInHierarchy)
        {
            Close();
        }
        else
        {
            Open();
            ItemPanel.SetActive(true);
            gameObject.GetComponent<Image>().sprite = OpenBackpack;
            LoadBackpackItems();
        }
    }

    public override void Close()
    {
        ItemPanel.SetActive(false);
        gameObject.GetComponent<Image>().sprite = ClosedBackpack;
    }
}
