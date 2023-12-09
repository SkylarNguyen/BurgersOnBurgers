using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class Bar : MonoBehaviour
{
    public GameObject bar;
    public float time = 10;

    public OrderMenu orderMenu;


    // Start is called before the first frame update
    void Start()
    {
        if(time <= 0)
        {
            time = 10;
        }

        orderMenu = GetComponent<OrderMenu>();
        AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Time: " + time); trying to see if the timer was working right

        if(time > 0)
        {
            time -= Time.deltaTime;

            
        }

        if(time <=0 )
        {
            
            orderMenu.ResetIngredients();
            time = 10;
            AnimateBar();
            //Debug.Log("is this working?");

        }
    }

    public void AnimateBar()
    {
        //the next two lines of code make it so that the bar can reset properly.
        //the 0 is important- if it's 1, the bar will appear maxed out
        LeanTween.cancel(bar);
        bar.transform.localScale = new Vector3(0, bar.transform.localScale.y, bar.transform.localScale.z);

        LeanTween.scaleX(bar,1,time);//.setOnComplete(OnTimerComplete);
    }


    //was going to try to reset the bar when the timer completes but I just did it a different way
    /*private void OnTimerComplete()
    {
        if(orderMenu != null)
        {
            orderMenu.SpawnNewIngredients();
        }
        else
        {
            Debug.LogError("OrderMenu issues");
        }
    }*/

    void BarMessage()
    {
        //timeUpMessage.SetActive(true);
    }
}
