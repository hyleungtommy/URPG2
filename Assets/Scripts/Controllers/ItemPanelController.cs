using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG;
public class ItemPanelController : MonoBehaviour
{
    public ItemPanelElement[] elements;
    public Button btnNextPage;
    public Button btnPrevPage;
    public Text textPage;
    int currPage;
    int maxPage;
    List<ItemAndQty> virtualInventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVirtualInventory(List<ItemAndQty> virtualInventory)
    {
        this.virtualInventory = virtualInventory;
        currPage = 0;
        maxPage = (int)Mathf.Floor((float)virtualInventory.Count / (float)elements.Length);
    }

    public void Render()
    {

        for (int i = 0; i < elements.Length; i++)
        {
            int pos = currPage * elements.Length + i;
            if (pos < virtualInventory.Count)
            {
                elements[i].itemId = virtualInventory[pos].item.id;
                elements[i].Render(virtualInventory[pos].item, virtualInventory[pos].qty);
            }
            else
            {
                elements[i].itemId = -1;
                elements[i].RenderEmpty();
            }
        }

        if (currPage == 0 && currPage == maxPage)
        {
            btnPrevPage.gameObject.SetActive(false);
            btnNextPage.gameObject.SetActive(false);
        }
        else if (currPage == 0)
        {
            btnPrevPage.gameObject.SetActive(false);
            btnNextPage.gameObject.SetActive(true);
        }
        else if (currPage == maxPage)
        {
            btnPrevPage.gameObject.SetActive(true);
            btnNextPage.gameObject.SetActive(false);
        }
        else
        {
            btnPrevPage.gameObject.SetActive(true);
            btnNextPage.gameObject.SetActive(true);
        }
        textPage.text = (currPage + 1) + "/" + (maxPage + 1);
    }

    public void OnClickNextPage()
    {
        currPage++;
        Render();
    }

    public void OnClickPrevPage()
    {
        currPage--;
        Render();
    }
}
