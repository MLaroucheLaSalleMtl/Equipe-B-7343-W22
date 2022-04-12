using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public ItemSO energy;
    public ItemSO trampoline;
    public ItemSO car;
    public ItemSO item;
    public ItemSO teleport;
    [SerializeField] public GameObject yes_btn;
    public void CompletePurchase()
    {
        item.AssignItem(item);
        Debug.Log("Complete purchase");
    }
    public void BuyEnergy()
    {
        yes_btn.GetComponent<Items>().item = energy;
    }
    public void BuyCar()
    {
        yes_btn.GetComponent<Items>().item = car;
    }
    public void BuyTrampoline()
    {
        yes_btn.GetComponent<Items>().item = trampoline;
    }
    public void BuyTeleport()
    {
        yes_btn.GetComponent<Items>().item = teleport;
    }
}
