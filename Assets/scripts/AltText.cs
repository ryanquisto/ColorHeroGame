using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AltText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string HoverText;
    private bool Hovering;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventdata)
    {
        Hovering = true;
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        Hovering = false;
    }



    void OnGUI()
    {
        if (Hovering)
        {
            float xpos = Input.mousePosition.x + 15;
            float ypos = Screen.height - Input.mousePosition.y - 15;
            GUIStyle style = new GUIStyle();
            style.wordWrap = true;
            style.alignment = TextAnchor.UpperCenter;
            style.normal.textColor = new Color(0, 0, 0, 1);
            GUI.Label(new Rect(xpos + 1, ypos, 100, 200), HoverText, style);
            GUI.Label(new Rect(xpos - 1, ypos, 100, 200), HoverText, style);
            GUI.Label(new Rect(xpos, ypos + 1, 100, 200), HoverText, style);
            GUI.Label(new Rect(xpos, ypos - 1, 100, 200), HoverText, style);
            style.normal.textColor = new Color(1, 1, 1, 1);
            GUI.Label(new Rect(xpos, ypos, 100, 200), HoverText, style);
        }
    }
}
