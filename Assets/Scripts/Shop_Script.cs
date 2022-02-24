using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Script : MonoBehaviour
{
        [SerializeField] int money;
        [SerializeField] GameObject price;
        [SerializeField] Button Buy_btn;
        [SerializeField] int item;
        public Text Change_Text;
        public string buy = "Buy";

        // Start is called before the first frame update
        void Start()
        {

        }

    public void Buy()
    {
        if (PlayerPrefs.GetInt("Money") < item)
        {
            Debug.Log("Not Enough Money");
            Change_Text.text = "Not Enough Money";
            price.SetActive(false);
        }
        else
        {
            Debug.Log("Enough Money");
            Change_Text.text = "Buy";
            Buy_btn.interactable = false;
            PlayerPrefs.SetString("Buy1", Change_Text.text);
            int coin = PlayerPrefs.GetInt("Money");
            money = money - item;
            PlayerPrefs.SetInt("money", money);
            price.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
