using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact_Shop : MonoBehaviour
{

    

    bool termInteract = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        //if (other.gameObject.CompareTag("DPoint"))
        //{
        //    dpoint = true;
        //    popup_Complete.SetActive(true);

        //    //currentdel=id du dp
        //    currentDel = other.gameObject.GetComponent<DPoints>().Id;
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        
        //RefreshDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
           
            
        }
    }
}
