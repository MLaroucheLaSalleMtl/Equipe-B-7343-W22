using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Script : MonoBehaviour
{
        [SerializeField] private Confirm_Btn thisConfirm;

        [SerializeField] GameObject price;
        [SerializeField] Button Buy_btn;

        [SerializeField] string Carname= "car";
        [SerializeField] int CarPrice = 10000;
        //[SerializeField] int PlayerCash;

        [SerializeField] string Drinkname = "drink";
        [SerializeField] int Drink = 100;

        [SerializeField] string Echellename = "echelle";
        [SerializeField] int Echelle = 50000;


        [SerializeField] string buy = "Buy";

        // Start is called before the first frame update
        void Start()
        {
        
        Interactions.money = PlayerPrefs.GetInt("Money", 0);
        Interactions.RefreshDisplay();

        }

//------------------- Achat Car Item in Shop--------------------------------
    public void BuyCar()
    {
        //-------Affichage de la sauvegarde-----
        Debug.Log(PlayerPrefs.GetInt("Money")); 

        //----On comparenot monaie fictif pour faire des achats------
        if (Interactions.money < CarPrice)
        {
            
            Debug.Log(" Not Enough Money");

        //    Change_Text.text = "Not Enough Money";
        //    price.SetActive(false);
        }
        else
        {
            //Debug.Log("Enough 1 Money"+ PlayerCash);
            Interactions.money = Interactions.money - CarPrice;
            Debug.Log("Enough 2 Money"+ Interactions.money);
            PlayerPrefs.SetInt("Money", Interactions.money);
 
        }
        
        Interactions.RefreshDisplay();
    }

    //------------------ Achat Energie Item in Shop -------------------------
    public void BuyDrink()
    {
        Debug.Log(PlayerPrefs.GetInt("Money"));

        if (Interactions.money < Drink)
        {
            Debug.Log(" Not Enough Money");

        }
        else
        {

            Interactions.money = Interactions.money - Drink;
            Debug.Log("Enough Money" + Interactions.money);
            PlayerPrefs.SetInt("Money", Interactions.money);

        }
        Interactions.RefreshDisplay();
    }

    //----------------- Achat Ladder Item in Shop ----------------------
    public void BuyEchelle()
    {
        Debug.Log(PlayerPrefs.GetInt("Money"));

        if (Interactions.money < Echelle)
        {

            Debug.Log(" Not Enough Money");

        }
        else
        {
            Interactions.money = Interactions.money - Echelle;
            Debug.Log("Enough Money");
            
        }
        Interactions.RefreshDisplay();
    }

    // Update is called once per frame
    void Update()
    {   

        Interactions.RefreshDisplay();
    }
}
