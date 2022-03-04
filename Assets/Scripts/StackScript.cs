using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StackScript : MonoBehaviour
{
    public GameObject box;
    public GameObject lastBox;
    public Text score;
    public int level;
    public bool gameOver;


    private void Awake()
    {
        
        //create the base cube
        box=GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.transform.position = Vector3.zero;
        box.transform.localScale=new Vector3(100,75,100);
        box.GetComponent<Renderer>().material = Resources.Load<Material>("cardboardBox");

        lastBox = box;

        nextBox();
        
    }

    void nextBox()
    {
        box.transform.localScale = new Vector3(lastBox.transform.localScale.x - Mathf.Abs(box.transform.position.x - lastBox.transform.position.x),
                                                           lastBox.transform.localScale.y,
                                                           lastBox.transform.localScale.z - Mathf.Abs(box.transform.position.z - box.transform.position.z));

        if (box.transform.localScale.x <= 0f ||
               box.transform.localScale.z <= 0f)
        {
            gameOver = true;
            Application.Quit();
        }

        lastBox = box;
        box = Instantiate(lastBox);
        level++;

        Camera.main.transform.position = box.transform.position + new Vector3(100, 180f, -100);
        Camera.main.transform.LookAt(box.transform.position + Vector3.down * 30f);

    }


    private void Start()
    {
        //nextBox();
    }


    private void Update()
    {

        var time = Mathf.Abs(Time.realtimeSinceStartup % 2f - 1f);

        var pos1 = lastBox.transform.position + Vector3.up * 75f;
        var pos2 = pos1;

        if (level % 2 == 0)
            pos2 += Vector3.left * 140;
        else
            pos2 += Vector3.right * 140;

        if (level % 2 == 0)
            box.transform.position = Vector3.Lerp(pos2, pos1, time);
        else
            box.transform.position = Vector3.Lerp(pos1, pos2, time);


        if (Input.GetMouseButtonDown(0))
            nextBox();
    }


    void RefreshDisplay()
    {
        score.text = level.ToString("D4");

    }

}
