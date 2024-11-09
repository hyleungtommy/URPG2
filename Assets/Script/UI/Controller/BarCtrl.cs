using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarCtrl : MonoBehaviour {

    public Slider bar;
    public Image barLeftEnd, barRightEnd;
    public Text textBarVlaue;
    public bool isATBBar = false;
    private float maxValue;
    private float barValue;

    public void render(float maxValue, float currValue)
    {
        //Debug.Log ("redner" + currValue + " " + maxValue);
        this.maxValue = maxValue;
        this.barValue = currValue;
        if (textBarVlaue.IsActive())
            textBarVlaue.text = (int)(currValue) + "/" + (int)maxValue;
        StartCoroutine(animateBarChange());
    }

    public void noAnimationRender(float maxValue, float currValue)
    {
        this.maxValue = maxValue;
        this.barValue = currValue;
        if (textBarVlaue != null && textBarVlaue.IsActive() && !isATBBar)
            textBarVlaue.text = (int)Mathf.Floor(currValue) + "/" + (int)maxValue;
        bar.value = currValue / maxValue;
        //		Debug.Log ("bar v" + bar.value);
        barLeftEnd.gameObject.SetActive(bar.value > 0);
        barRightEnd.gameObject.SetActive(bar.value >= bar.maxValue);
    }

    IEnumerator animateBarChange()
    {
        float passedTime = 0.5f;
        float targetValue = barValue / maxValue;
        float valueChange = (bar.value > targetValue ? -(bar.value - targetValue) : (targetValue - bar.value)) / 10;
        do
        {
            bar.value += valueChange;
            //if(textBarVlaue.IsActive())
            //	textBarVlaue.text = (int)(bar.value/maxValue) + "/" + (int)maxValue;
            barLeftEnd.gameObject.SetActive(bar.value > 0);
            barRightEnd.gameObject.SetActive(bar.value >= bar.maxValue);
            passedTime -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        } while (passedTime >= 0f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
