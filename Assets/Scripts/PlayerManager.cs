using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private float health = 100;
    private int healthPoints = 3;
    [SerializeField] public Text txthp;


    public float Health { get => health; set => health = value; }
    public int HealthPoints { get => healthPoints; set => healthPoints = value; }


    private void Update()
    {
        if (Health < 0)
        {
            if (healthPoints > 0)
            {
                health = 100;
                healthPoints--;
            }
            if (healthPoints < 0)
                this.gameObject.GetComponent<Interactions>().FailDelivery();
        }
        RefreshHpDisplay();
    }


    void RefreshHpDisplay()
    {
        txthp.text = "HP: " + ((int)Math.Round(Health)).ToString("D2");
    }
}
