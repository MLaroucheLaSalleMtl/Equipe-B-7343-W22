using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] public GameObject[] Dp;
    [SerializeField] public int currentDel;

    [SerializeField] public GameObject popup;
    [SerializeField] public GameObject deliveryTerm;
    bool termInteract = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terminal"))
        {
            termInteract = true;
            popup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terminal")) 
        {
            termInteract = false;
            popup.SetActive(false);
        }
    }

    public void OnTermInteract()
    {
        if (termInteract)
        {
            popup.SetActive(false);
            deliveryTerm.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //Get a random delivery point
            currentDel = GetRandomNbr();
            Dp[currentDel - 1].SetActive(true);
            termInteract = false;


            //To Do :
            //Show the delivery on a minimap
        }
    }
    public void CloseMenus()
    {
        popup.SetActive(false);
        deliveryTerm.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void AcceptDelivery()
    {
        //To do
    }
    public void RefuseDelivery()
    {
        CloseMenus();
        popup.SetActive(true);
        Dp[currentDel - 1].SetActive(false);
        termInteract = true;
    }
    public int GetRandomNbr()
    {
        return Random.Range(1, Dp.Length);
    }
}
