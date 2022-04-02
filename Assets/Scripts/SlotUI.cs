using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    private void Awake()
    {
        icon = GetComponentInChildren<Image>();
    }
    public Image icon;
    public ItemSO item;
    public void AddToSlot(ItemSO item)
    {
        icon.sprite = item.icon;
    }
}
