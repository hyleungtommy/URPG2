using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG;
public class BasicBox : MonoBehaviour
{
    public Image content;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addClickEvent(UnityAction call)
    {
        GetComponent<Button>().onClick.AddListener(call);
    }

    public void render(Displayable obj)
    {
        if (obj != null)
        {
            boxHaveItem(obj);
        }
        else
        {
            boxIsEmpty();
        }
    }

    protected virtual void boxHaveItem(Displayable obj)
    {
        content.gameObject.SetActive(true);
        content.sprite = obj.GetDisplayingImage();
    }

    protected virtual void boxIsEmpty()
    {
        content.gameObject.SetActive(false);
    }

    // public void setSelected(bool selected)
    // {
    //     if (selected)
    //     {
    //         gameObject.GetComponent<Image>().sprite = SpriteManager.basicBoxSelected;
    //     }
    //     else
    //     {
    //         gameObject.GetComponent<Image>().sprite = SpriteManager.basicBoxNormal;
    //     }
    // }
}
