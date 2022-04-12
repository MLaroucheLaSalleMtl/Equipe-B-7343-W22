using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Confirm_Btn : MonoBehaviour
{
    [SerializeField] private Confirm_Btn thisConfirm;
    private Button yes_btn;
    private Button no_Btn;
    private TextMeshProUGUI CancelText;

    private void Awake()
    {

        yes_btn = transform.Find("yes_btn").GetComponent<Button>();
        no_Btn= transform.Find("no_btn").GetComponent<Button>();
    }

    private void AddconfirmBtn(string text, Action Yesbtn, Action Nobtn)
    {
        yes_btn.onClick.AddListener(new UnityEngine.Events.UnityAction (Yesbtn));
        no_Btn.onClick.AddListener(new UnityEngine.Events.UnityAction(Nobtn));

    }
    public void Yes_Btn()
    {
        thisConfirm.gameObject.SetActive(false);
        Debug.Log("Yes touch");
    }

    public void No_Btn()
    {
        thisConfirm.gameObject.SetActive(false);
        Debug.Log("No touch");
    }
}
