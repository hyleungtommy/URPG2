using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;
public class BasicBox : MonoBehaviour
{
    public Image content;

    public void AddClickEvent(UnityAction call)
    {
        GetComponent<Button>().onClick.AddListener(call);
    }

    public void Render(IDisplayable obj)
    {
        if (obj != null)
        {
            BoxHaveItem(obj);
        }
        else
        {
            BoxIsEmpty();
        }
    }

    protected virtual void BoxHaveItem(IDisplayable obj)
    {
        content.gameObject.SetActive(true);
        content.sprite = obj.GetDisplayingImage();
    }

    protected virtual void BoxIsEmpty()
    {
        content.gameObject.SetActive(false);
    }
}
