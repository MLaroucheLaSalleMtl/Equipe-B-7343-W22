using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Inventory : MonoBehaviour
{

    [SerializeField] public GameObject fullInventory;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject trampoline;
    [SerializeField] public ItemSO energySO;
    [SerializeField] public ItemSO teleportSO;
    [SerializeField] public ItemSO trampolineSO;
    ItemSO item;
    public static Inventory instance = null;
    [SerializeField] SlotUI[] slots;
    int currentSlot = -1;

    private bool hasEnergyDrink = false;

    private void Awake()
    {
        LoadInventory();
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    public void SaveInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            PlayerPrefs.SetInt("Items " + i, slots[i].itemId);
        }
        PlayerPrefs.Save();
        Debug.Log("Inventory saved");
    }

    public void LoadInventory()
    {
        currentSlot = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].itemId = PlayerPrefs.GetInt("Items " + i, -1);
        }
            foreach (SlotUI slot in slots)
            {
                currentSlot++;
                if (slot.itemId == energySO.id)
                {
                currentSlot--;
                    AddItem(energySO);
                }
                else if (slot.itemId == trampolineSO.id)
                {
                currentSlot--;
                    AddItem(trampolineSO);
                }
                else if(slot.itemId == teleportSO.id){
                currentSlot--;
                    AddItem(teleportSO);
                }
                
            }
        
        currentSlot = 0;
        Debug.Log("Inventory loaded");
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
        if (i == slots.Length)
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
    private void OnApplicationQuit()
    {
        SaveInventory();
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
        player.transform.position = new Vector3(-53, -15, 18);
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
    public void HideMessage()
    {
        fullInventory.SetActive(false);
    }
}
