using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [SerializeField] public GameObject fullInventory;
    [SerializeField] public GameObject inDeliveryMessage;

    public Image icon;
    public ItemSO itemInSlot;
    public int itemId;

    public ItemSO energy;
    public ItemSO trampoline;
    public ItemSO car;
    public ItemSO teleport;
    

    public void AddToSlot(ItemSO item)
    {
        icon.sprite = item.icon;
        itemInSlot = item;
        itemId = item.id;
    }
    public void UseItem()
    {
        if (itemInSlot == energy)
        {
            if (PlayerPrefs.GetInt("inDelivery") == 0)
            {
                inDeliveryMessage.SetActive(true);
                Invoke("HideMessage", 5);
                
            }
            else
            {
                Inventory.instance.UseEnergyDrink();
                icon.sprite = null;
                itemInSlot = null;
                itemId = 0;
            }
        }
        else if (itemInSlot == car)
        {
            if (PlayerPrefs.GetInt("inDelivery") == 0)
            {
                inDeliveryMessage.SetActive(true);
                Invoke("HideMessage", 5);
            }
            else
            {
                Inventory.instance.UseCar();
                icon.sprite = null;
                itemInSlot = null;
                itemId = 0;
            }
            
        }
        else if (itemInSlot == teleport)
        {
            Inventory.instance.UseTeleport();
            icon.sprite = null;
            itemInSlot = null;
            itemId = 0;
            Debug.Log("item in slot is teleport");
        }
        else if (itemInSlot == trampoline)
        {
            if (PlayerPrefs.GetInt("inDelivery") == 0)
            {
                inDeliveryMessage.SetActive(true);
                Invoke("HideMessage", 5);
            }
            else
            {
                Debug.Log("SlotUI trampoline");
                Inventory.instance.UseTrampoline();
                icon.sprite = null;
                itemInSlot = null;
                itemId = 0;
            }
        }
    }
    public void HideMessage()
    {
        inDeliveryMessage.SetActive(false);
        fullInventory.SetActive(false);
    }
    
}
