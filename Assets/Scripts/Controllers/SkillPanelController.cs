using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using UnityEngine.UI;
public class SkillPanelController : MonoBehaviour
{
    public SkillPanelElement[] elements;
    public Button btnNextPage;
    public Button btnPrevPage;
    public Text textPage;
    int currPage;
    int maxPage;
    public EntityPlayer SelectedPlayer {set; get;}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Render()
    {
        List<Skill> skills = SelectedPlayer.skillList;

        for (int i = 0; i < elements.Length; i++)
        {
            int pos = currPage * elements.Length + i;
            if (pos < skills.Count)
            {
                elements[i].SkillId = skills[pos].id;
                elements[i].Skill = skills[pos];
                elements[i].Render(skills[pos], SelectedPlayer);
            }
            else
            {
                elements[i].SkillId = -1;
                elements[i].Skill = null;
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
