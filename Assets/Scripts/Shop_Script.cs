using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Script : MonoBehaviour
{
    Inventory inventory;
    public ItemSO item;

    [SerializeField] public GameObject fullInventory;
    [SerializeField] public GameObject Popup_confirm;
        [SerializeField] GameObject price;
        [SerializeField] Button Buy_btn;

        [SerializeField] string Carname= "car";
        [SerializeField] int CarPrice = 10000;

        [SerializeField] int ItemId;
        //0=car, 1=energyDrink, 2=trampoline, 3=telepote

        [SerializeField] string Drinkname = "drink";
        [SerializeField] int DrinkPrice = 500;

        [SerializeField] string Trampolinename = "Trampoline";
        [SerializeField] int TrampolinePrice = 1000;

        [SerializeField] string Teleportname = "Teleporter";
        [SerializeField] int TeleportPrice = 750;


    [SerializeField] string buy = "Buy";

    private void Awake()
    {
    }
    private void Update()
    {
        Interactions.RefreshDisplay();
    }

    //------------------- Achat Car Item in Shop--------------------------------
    public void BuyCar()
    {
        //-------Affichage de la sauvegarde-----------------------------------------

        Debug.Log(PlayerPrefs.GetInt("Money"));

        ItemId = 0;

        if (Inventory.instance.IsFull())
        {
            fullInventory.SetActive(true);
            Invoke("HideMessage", 10);
            return;
        }
        else
        if (Interactions.money >= CarPrice) // Assez d'argent
        {
            Popup_confirm.SetActive(true);
            Debug.Log("Enough Money");
        }
        else //Pas Assez d'argent
        {
            //To do show error messg

            Debug.Log("Not Enough Money" + Interactions.money);
            PlayerPrefs.SetInt("Money", Interactions.money);

        }
    }

    //------------------ Achat Energie Item in Shop -------------------------
    public void BuyDrink()
    {
        Debug.Log(PlayerPrefs.GetInt("Money"));
        
        ItemId = 1;

        if (Inventory.instance.IsFull())
        {
            fullInventory.SetActive(true);
            Invoke("HideMessage", 10);
            return;
        }
        else
        if (Interactions.money >= DrinkPrice) // Assez d'argent
        {
            Popup_confirm.SetActive(true);
            Debug.Log("Enough Money");
        }
        else //Pas Assez d'argent
        {
            //To do show error messg
            
            Debug.Log("Not Enough Money" + Interactions.money);
            PlayerPrefs.SetInt("Money", Interactions.money);

        }
    }

    //----------------- Achat Ladder Item in Shop ----------------------
    public void BuyTrampoline()
    {
        Debug.Log(PlayerPrefs.GetInt("Money"));

        ItemId = 2;

        if (Inventory.instance.IsFull())
        {
            fullInventory.SetActive(true);
            Invoke("HideMessage", 10);
            return;
        }
        else
        if (Interactions.money >= TrampolinePrice) // Assez d'argent
        {
            Popup_confirm.SetActive(true);
            Debug.Log("Enough Money");
        }
        else //Pas Assez d'argent
        {
            //To do show error messg

            Debug.Log("Not Enough Money" + Interactions.money);
            PlayerPrefs.SetInt("Money", Interactions.money);

        }
    }
    public void BuyTeleport()
    {
        Debug.Log(PlayerPrefs.GetInt("Money"));

        ItemId = 3;

        if (Inventory.instance.IsFull())
        {
            fullInventory.SetActive(true);
            Invoke("HideMessage", 10);
            return;
        }
        else
        if (Interactions.money >= TeleportPrice) // Assez d'argent
        {
            Popup_confirm.SetActive(true);
            Debug.Log("Enough Money");
        }
        else //Pas Assez d'argent
        {
            //To do show error messg

            Debug.Log("Not Enough Money" + Interactions.money);
            PlayerPrefs.SetInt("Money", Interactions.money);

        }
    }

public void Accept_YES()
    {
        switch (ItemId)
        {
            case 0: Interactions.money = Interactions.money - CarPrice;
                break;
            case 1:
                Interactions.money = Interactions.money - DrinkPrice;
                break;
            case 2:
                Interactions.money = Interactions.money - TrampolinePrice;
                break;
            case 3:
                Interactions.money = Interactions.money - TeleportPrice;
                break;
        }
        Popup_confirm.SetActive(false);
        Debug.Log("Buy successful : money" + Interactions.money);
        PlayerPrefs.SetInt("Money", Interactions.money);
        Interactions.RefreshDisplay();
    }

    public void Refuse_NO()
    {
        Popup_confirm.SetActive(false);
    }
    public void HideMessage()
    {
        fullInventory.SetActive(false);
    }
}
