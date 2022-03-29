//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Confirmation : MonoBehaviour
//{
//    [SerializeField] private Confirm_Btn thisConfirm;

//    Start is called before the first frame update
//    void Start()
//    {

//    }

//    private void thisConfirmBtn(string text)
//    {
//        thisConfirm.gameObject.SetActive(true);
//        thisConfirm.yes_btn.onClick.AddListener(Yes_Btn);
//        thisConfirm.yes_btn.onClick.AddListener(No_Btn);
//        thisConfirm.CancelText.text = text;
//    }

//    private void Yes_Btn()
//    {
//        thisConfirm.gameObject.SetActive(false);
//        Debug.Log("Yes touch");
//    }

//    private void No_Btn()
//    {
//        thisConfirm.gameObject.SetActive(true);
//        Debug.Log("No touch");

//    }
//}
