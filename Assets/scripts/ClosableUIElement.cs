using UnityEngine;
using System.Collections;

public abstract class ClosableUIElement: MonoBehaviour {

    public abstract void Close();

    public void Open()
    {
        if (GetComponentInParent<UIManager>())
            GetComponentInParent<UIManager>().AddUIElement(gameObject);
    }

    void OnDisable()
    {
        if (GetComponentInParent<UIManager>())
            GetComponentInParent<UIManager>().remove(gameObject);
    }

    void OnEnable()
    {
        Open();
    }
}
