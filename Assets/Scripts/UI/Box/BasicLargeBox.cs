using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;

public class BasicLargeBox : MonoBehaviour
{
    // Start is called before the first frame update
    public BasicBox innerBox;

    public void addClickEvent(UnityAction call)
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
        innerBox.Render(obj);
    }

    protected virtual void BoxIsEmpty()
    {
        innerBox.Render(null);
    }


}
