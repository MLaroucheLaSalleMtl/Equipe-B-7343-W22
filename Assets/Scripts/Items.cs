using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public ItemSO item;
    public void BuyItem()
    {
        item.AssignItem(item);
    }
}
