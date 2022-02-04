using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryTerminal : MonoBehaviour
{
    [SerializeField] public GameObject popup;
    [SerializeField] public GameObject DeliveryMenu;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popup.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            popup.SetActive(false);
        }
    }
    public void OnInteract()
    {
        DeliveryMenu.SetActive(true);
        popup.SetActive(false);
    }
}