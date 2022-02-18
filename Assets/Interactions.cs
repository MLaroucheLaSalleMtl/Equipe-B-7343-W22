using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Interactions : MonoBehaviour
{
    [SerializeField] public GameObject delCompletText;
    [SerializeField] public GameObject deliveryLimiter;
    [SerializeField] public GameObject DTWaypoint;
    [SerializeField] public GameObject mapCanvas;
    [SerializeField] public GameObject deliveriesCanvas;
    [SerializeField] public GameObject moneyCanvas;
    [SerializeField] public GameObject[] Dp;
    [SerializeField] public GameObject popup_Accept;
    [SerializeField] public GameObject popup_Complete;
    [SerializeField] public int currentDel;
    [SerializeField] private Text numDel;
    [SerializeField] private Text txtmoney;
    [SerializeField] public GameObject deliveryTerm;


    bool termInteract = false;
    bool dpoint=false;

    private int[] delArray = new int[3];

    int money=0;

    int rnd = 0;
    int delCount = 0;
    int completed = 0;

    bool restart = false;

    bool check;



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terminal"))
        {
            termInteract = true;
            popup_Accept.SetActive(true);
        }
        if (other.gameObject.CompareTag("DPoint"))
        {
            dpoint = true;
            popup_Complete.SetActive(true);
            
            //currentdel=id du dp
            currentDel = other.gameObject.GetComponent<DPoints>().Id;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terminal")) 
        {
            termInteract = false;
            popup_Accept.SetActive(false);
        }
        if(other.gameObject.CompareTag("DPoint"))
        {
            dpoint = false;
            popup_Complete.SetActive(false);
           
        }
    }

    void RefreshDisplay()
    {
        numDel.text = "Delivered: " + completed.ToString()+"/"+delCount;
        txtmoney.text = money.ToString("D6") + "$";
    }



    public void OnTermInteract()
    {
        if (termInteract && delCount<3)
        {
            popup_Accept.SetActive(false);
            deliveryTerm.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //Get a random delivery point

            GenerateDP();

            Dp[currentDel - 1].SetActive(true);
            
            //id de DP=currentdel
            Dp[currentDel - 1].gameObject.GetComponent<DPoints>().Id = currentDel;
            
            //Show the DP on a minimap

            mapCanvas.SetActive(true);
            termInteract = false;


            //To Do :
            //Show the delivery on a minimap
        }
        else if(termInteract && delCount>=3)
        {
            DTWaypoint.SetActive(false);
            deliveryLimiter.SetActive(true);
            Invoke("HideLimiterText", 10);
        }
        else if(dpoint)
        {
            Dp[currentDel - 1].SetActive(false);
            popup_Complete.SetActive(false);
            completed++;

            if (completed == delCount)
            {
                delCompletText.SetActive(true);
                deliveriesCanvas.SetActive(false);

                //msg deliveries completed
                Invoke("HideDelCompletText", 10);

                //set deliveries terminal waypoint
                DTWaypoint.SetActive(true);

                //argent proportieonnelle
                if (delCount == 1) money += 200;
                else if (delCount == 2) money += 460;
                else if (delCount == 3) money += 750;

                //reset delcount
                completed = 0;
                delCount= 0;    
                Array.Clear(delArray, 0, delArray.Length);
            }
            RefreshDisplay();
        }
    }

    public void HideDelCompletText()
    {
        delCompletText.SetActive(false);
    }

    public void HideLimiterText()
    {
        deliveryLimiter.SetActive(false);
    }
    private void GenerateDP()
    {
        do
        {
            currentDel = GetRandomNbr();
            check = Array.Exists<int>(delArray, x => x == currentDel);
        } while (check);
        if (!check)
        {
            delArray[delCount] = currentDel;
            
        }
        
    }
       
    public void CloseMenus()
    {
        popup_Accept.SetActive(false);
        deliveryTerm.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mapCanvas.SetActive(false);
    }
    public void AcceptDelivery()
    {
        //To do
        delArray[delCount] = currentDel;
        delCount++;
        deliveriesCanvas.SetActive(true);
        RefreshDisplay();
        GameObject temp = Dp[currentDel - 1];
        temp.transform.GetChild(0).gameObject.layer = 6;
        temp.transform.GetChild(0).transform.GetChild(0).gameObject.layer = 6;
        CloseMenus();
    }
    public void RefuseDelivery()
    {
        CloseMenus();
        popup_Accept.SetActive(true);
        Dp[currentDel - 1].SetActive(false);
        termInteract = true;
        delArray[delCount] = 0;
    }
    public int GetRandomNbr()
    {
        return UnityEngine.Random.Range(1, Dp.Length);
    }
}
