using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour {
	public Sprite image;
	public string description;
	public bool InRange;
    public bool DestroyOnUse;
    public string ID;
    public bool Stacks;
    private int Count;
	// Use this for initialization
	void Start () {
        Count = 0;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void CheckForDuplicateID()   //if gameover, destroy the already collected objects by comparing ID
    {
		if (ID == null)
			return;
        for (int i = 0; i < Inventory.CollectedIDs.Count; i++)
        {
            if (Inventory.CollectedIDs[i].Equals(ID))
                Destroy(this.gameObject);
        }
    }
	

	public abstract void UseItem();

	void OnTriggerEnter(Collider col){
        if (col.gameObject.name.Equals("player"))
        {
            InRange = true;
            GameController.CollectPanel.SetActive(true);
        }
	}
	
	void OnTriggerExit(Collider col){
        if (col.gameObject.name.Equals("player"))
        {
            InRange = false;
            GameController.CollectPanel.SetActive(false);
        }
	}

    public void CheckForCollect(bool CollectOverride=false){
		if ((InRange && Input.GetKeyDown (KeyCode.X))||CollectOverride) {
			InRange = false;
			GameController.CollectItem (this);
            Disable();            
			GameController.CollectPanel.SetActive(false);
            GameController.ShowTutorialTip("New Item", "You just got an item! You can find it in your backpack at the bottom right of your screen.", new Vector3(0.5f, 0.5f, 0.5f), "ObtainItem");
		}
	}

    public virtual void Disable()
    {
        Destroy(GetComponent<SphereCollider>());
        Destroy(GetComponent<MeshRenderer>());
        Destroy(GetComponentInChildren<ParticleSystem>());
    }

    public int GetCount()
    {
        return Count;
    }

    public void SetCount(int count)
    {
        Count = count;
    }
}
