using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "ScriptablesObjects/Create item", order = 1)]
public class ItemSO : ScriptableObject
{
    public Sprite icon;
    public int id;

    public void AssignItem(ItemSO item)
    {
        Debug.Log("Assign item");
        Inventory.instance.AddItem(item);
    }
}
