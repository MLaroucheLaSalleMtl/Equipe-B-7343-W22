using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Inventory : MonoBehaviour
{
    [SerializeField] public GameObject fullInventory;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject trampoline;
    ItemSO item;
    public static Inventory instance = null;
    [SerializeField] SlotUI[] slots;
    int currentSlot = -1;

    private bool hasEnergyDrink = false;

    private void Awake()
    {
        PlayerPrefs.SetInt("Money", 500000);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    
    public void AddItem(ItemSO item)
    {
            Debug.Log("AddItem");
            slots[currentSlot].AddToSlot(item);
            currentSlot++; 
    }
    public bool IsFull()
    {
        int i;
        for (i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemInSlot == null)
            {
                currentSlot = i;
                break;
            }
        }
        if (i == 15)
        {
            return true;
        }
        return false;
        /*if (currentSlot == 15)
        {
            return true;
        }
        else
        {
            return false;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trampoline");
        if (other.gameObject.CompareTag("Trampoline"))
        {
            Debug.Log("Trampoline1");
            JumpOnTrampoline();
        }
    }
    public void UseEnergyDrink()
    {
            ThirdPersonController.MoveSpeed = 1.0f;
            ThirdPersonController.SprintSpeed = 1.5f;
    }
    public void UseCar()
    {
        //TODO : Put a car at the delivery terminal
    }
    public void UseTrampoline()
    {
            trampoline.transform.position = player.transform.position;
            Instantiate(trampoline);
    }
    public void UseTeleport()
    {
        player.gameObject.transform.position = new Vector3(-53, -15, 18);
    }
    private void JumpOnTrampoline()
    {
        Vector3 force = new Vector3(0, 3, 0);
        ThirdPersonController.verticalVelocity = 5;
    }
    private void FixedUpdate()
    {
        if (hasEnergyDrink)
        {
            UseEnergyDrink();
        }
    }
}
