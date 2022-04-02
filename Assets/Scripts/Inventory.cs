using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Inventory : MonoBehaviour
{
    ItemSO item;
    public static Inventory instance = null;
    [SerializeField] SlotUI[] slots;
    int currentSlot = 0;

    private bool hasEnergyDrink = false;

    private void Awake()
    {
        if(instance == null)
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trampoline");
        if (other.gameObject.CompareTag("Trampoline"))
        {
            Debug.Log("Trampoline1");
            UseTrampoline();
        }
    }
    private void UseEnergyDrink()
    {
        ThirdPersonController.MoveSpeed = 1.0f;
        ThirdPersonController.SprintSpeed = 1.5f;
    }
    private void UseCar()
    {
        //TODO
    }
    private void UseTrampoline()
    {
        /*ThirdPersonController.MoveSpeed = 0.2f;
        ThirdPersonController.MoveSpeed = 0.3f;
        Vector3 force = new Vector3(0, 3, 0);
        this.GetComponent<CharacterController>().Move(force);
        Debug.Log("Trampoline2");
        ThirdPersonController.MoveSpeed = 0.75f;
        ThirdPersonController.SprintSpeed = 1.2f;*/
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
