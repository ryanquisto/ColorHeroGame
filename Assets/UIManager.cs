using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    private List<ClosableUIElement> ActiveUIElements;
    // Use this for initialization
	void Start () {
        ActiveUIElements = new List<ClosableUIElement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape) && ActiveUIElements.Count >=1)
        {
            ClosableUIElement top = ActiveUIElements[ActiveUIElements.Count - 1];
            ActiveUIElements.Remove(top);
            top.Close();
        }
	
	}

    public void AddUIElement(GameObject ui)
    {
        if (ui!=null && ActiveUIElements!=null)
            ActiveUIElements.Add(ui.GetComponent<ClosableUIElement>());
    }

    public void remove(GameObject ui)
    {
        try
        {
            ActiveUIElements.Remove(ui.GetComponent<ClosableUIElement>());
        }
        catch (System.Exception)
        {
            throw new System.ObjectDisposedException(ui.name);
        }
        
    }
}
