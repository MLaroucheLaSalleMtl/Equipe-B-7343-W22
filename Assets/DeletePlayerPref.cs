using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerPref : MonoBehaviour
{
    public static bool DeletedPref = false;
    void DeletePref()
    {
        PlayerPrefs.DeleteAll();
        DeletedPref = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
