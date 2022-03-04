using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Shop : MonoBehaviour
{

    [SerializeField] public GameObject popup_Accept;

    //bool termInteract = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            //termInteract = true;
            popup_Accept.SetActive(true);
        }
        //if (other.gameObject.CompareTag("DPoint"))
        //{
        //    dpoint = true;
        //    popup_Complete.SetActive(true);

        //    //currentdel=id du dp
        //    currentDel = other.gameObject.GetComponent<DPoints>().Id;
        //}
    }


}
