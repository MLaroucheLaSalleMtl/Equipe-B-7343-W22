using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using StarterAssets;
using System.IO;

public class Interactions : MonoBehaviour
{

    GameManager gameManager;

    [SerializeField] public static int score;
    public static bool endGame = false;
    public static bool minigame = false;


    [SerializeField] public GameObject delCompletText;
    [SerializeField] public GameObject deliveryLimiter;
    [SerializeField] public GameObject DTWaypoint;

    [SerializeField] public GameObject timerCanvas;
    [SerializeField] public GameObject inventoryCanvas;
    [SerializeField] public GameObject[] currentDP;
    [SerializeField] public GameObject[] dp;
    [SerializeField] public GameObject[] dpItem;
    [SerializeField] public GameObject popup_Accept;
    [SerializeField] public GameObject popup_Complete;
    [SerializeField] public int currentDel;
    [SerializeField] public Text adddel;
    [SerializeField] private static Text numDel;
    [SerializeField] private static Text txtmoney;
    [SerializeField] public GameObject miniGame;
    [SerializeField] public GameObject miniMap;
    [SerializeField] public GameObject ChooseNumDeliveryCanvas;
    [SerializeField] public GameObject FailText;

    [SerializeField] public Text txtmoneyobj;
    [SerializeField] public Text numDelobj;
    [SerializeField] public GameObject scoreTxt;

    [SerializeField] public GameObject canvasScore;
    [SerializeField] public GameObject mapCanvas;
    [SerializeField] public GameObject deliveriesCanvas;
    [SerializeField] public GameObject moneyCanvas;
    [SerializeField] public GameObject Popup_Shop;
    [SerializeField] public GameObject Shop_terminal;
    [SerializeField] public GameObject canvasEsc;






    bool shopInteract = false;

    bool esc = false;

    bool termInteract = false;
    bool dpoint = false;

    bool inventory = false;

    private int[] delArray = new int[3];

    [SerializeField] public static int money = 0;
    public static int delCount = 0;
    int indexCount = 0;
    int counter = 0;
    static int completed = 0;
    public int inDelivery = 0;
    int playerLife = 100;
    int numLife = 3;
    int itemDeliveryBonus = 300;

    bool check;

    bool isItemDelivery = false;

    private void Awake()
    {
        currentDP = dp;
        txtmoney = txtmoneyobj;
        numDel = numDelobj;
        gameManager = GameManager.instance;
        LoadValues();


        //ResetValue();
        Msg();

        if (delCount != 0)
        {
            deliveriesCanvas.SetActive(true);
            DTWaypoint.SetActive(false);
        }
        RefreshDisplay();
    }


    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit.gameObject.CompareTag("AICars"))
    //    {
    //        playerLife -= hit.gameObject.GetComponentInParent<AICarMove>().CarSpeed;

            //if (playerLife <= 0)
            //{
            //    hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn                
            //}
            //if (playerLife <= 0 && inDelivery == 1 && numLife > 0)
            //{
            //    hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn
            //    numLife--;
            //}
            //if (playerLife <= 0 && inDelivery == 1 && numLife <= 0)
            //{
            //    deliveriesCanvas.SetActive(false);
            //    this.gameObject.transform.position.Set(-70.201f, -15.548f, 10.453f);//reset position
            //    ResetValue();
            //    hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn
            //    FailText.SetActive(true);
            //    Invoke("HideFailText", 5);
            //    DTWaypoint.SetActive(true);
            //}
            //if (playerLife <= 0) 
            //{ 
            //    hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn                
            //}
            //if (playerLife <= 0 && inDelivery == 1 && numLife > 0)
            //{
            //    hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn
            //    numLife--;
            //}
            //if (playerLife <= 0 && inDelivery == 1 && numLife <= 0)
            //{
            //    deliveriesCanvas.SetActive(false);
            //    this.gameObject.transform.position.Set(-70.201f, -15.548f, 10.453f);//reset position
            //    ResetValue();
            //    hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn
            //    FailText.SetActive(true);
            //    Invoke("HideFailText", 5);
            //    DTWaypoint.SetActive(true);
            //}
    //        if (playerLife <= 0) 
    //        { 
    //            hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn                
    //        }
    //        if (playerLife <= 0 && inDelivery == 1 && numLife > 0)
    //        {
    //            hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn
    //            numLife--;
    //        }
    //        if (playerLife <= 0 && inDelivery == 1 && numLife <= 0)
    //        {
    //            deliveriesCanvas.SetActive(false);
    //            this.gameObject.transform.position.Set(-70.201f, -15.548f, 10.453f);//reset position
    //            ResetValue();
    //            hit.gameObject.GetComponentInParent<AICarMove>().RespawnPlayer(this.gameObject);//respawn
    //            FailText.SetActive(true);
    //            Invoke("HideFailText", 5);
    //            DTWaypoint.SetActive(true);
    //        }


    //    }
    //}
                
            
    //    } 
    //}
                
            
    //    } 
    //}
    public void FailDelivery()
    {
        ResetValue();
        FailText.SetActive(true);
        Invoke("HideFailText", 10);
        DTWaypoint.SetActive(true);
        CountTimer.ResetTimer();
        timerCanvas.SetActive(false);
        RefreshDisplay();
    }


    private void Project(int carSpeed)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            shopInteract = true;
            Popup_Shop.SetActive(true);

        }
        else if (other.gameObject.CompareTag("Terminal"))
        {
            termInteract = true;
            popup_Accept.SetActive(true);
        }
        else if (other.gameObject.CompareTag("DPoint"))
        {
            dpoint = true;
            popup_Complete.SetActive(true);

            //currentdel=id du dp
            currentDel = other.gameObject.GetComponentInParent<DPoints>().Id;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terminal"))
        {
            termInteract = false;
            popup_Accept.SetActive(false);
            if (ChooseNumDeliveryCanvas.activeInHierarchy)
            {
                ChooseNumDeliveryCanvas.SetActive(false);
                counter = 0;
                adddel.text = counter.ToString();
            }
        }
        if (other.gameObject.CompareTag("DPoint"))
        {
            dpoint = false;
            popup_Complete.SetActive(false);
        }
        if (other.gameObject.CompareTag("Shop"))
        {
            shopInteract = false;
            Popup_Shop.SetActive(false);
        }
        RefreshDisplay();
    }

    public static void RefreshDisplay()
    {
        numDel.text = "Delivered: " + completed.ToString() + "/" + delCount;
        txtmoney.text = PlayerPrefs.GetInt("Money").ToString("D6") + "$";
        
    }

    

    public void OnTermInteract()
    {
        if (termInteract && inDelivery == 0)
        {
            miniMap.SetActive(false);
            popup_Accept.SetActive(false);
            //deliveryTerm.SetActive(true);
            ChooseNumDeliveryCanvas.SetActive(true);
            ShowCursor();

            termInteract = false;
        }
        else if (shopInteract == true)
        {
            ShowCursor();
            Shop_terminal.SetActive(true);
            Popup_Shop.SetActive(false);
            miniMap.SetActive(false);
        }
        else if (inDelivery == 1 && termInteract)
        {
            deliveryLimiter.SetActive(true);
            Invoke("HideLimiterText", 10);
        }

        else if (dpoint)
        {
            Msg();
            dpItem[currentDel].SetActive(false);
            popup_Complete.SetActive(false);
            completed++;
            dpoint = false;
            for (int i = 0; i < dpItem.Length; i++)
            {
                if (!dpItem[i].activeInHierarchy)
                {
                    dpItem[i].gameObject.GetComponent<DPoints>().Id = 0;
                    PlayerPrefs.SetInt(i.ToString(), 0);
                }
            }

            if (completed == delCount)
            {
                delCompletText.SetActive(true);
                deliveriesCanvas.SetActive(false);
                

                //msg deliveries completed
                Invoke("HideDelCompletText", 10);

                //set deliveries terminal waypoint
                DTWaypoint.SetActive(true);


                //argent proportieonnelle
                if (isItemDelivery)
                {
                    money += itemDeliveryBonus;
                }                
                if (delCount == 1)
                {
                    money += 200 + ((200 * score) / 50);
                }
                else if (delCount == 2)
                {
                    money += 460 + ((460 * score) / 50);
                }
                else if (delCount == 3)
                {
                    money += 750 + ((750 * score) / 50);
                }


                //reset values

                ThirdPersonController.MoveSpeed = 0.75f;
                ThirdPersonController.SprintSpeed = 1.2f;
                CountTimer.ResetTimer();
                timerCanvas.SetActive(false);


                for (int i = 0; i < dpItem.Length; i++)
                {
                    dpItem[i].gameObject.GetComponent<DPoints>().Id = 0;
                    PlayerPrefs.SetInt(i.ToString(), 0);
                }
                Array.Clear(delArray, 0, delArray.Length);
                ResetValue();
                PlayerPrefs.Save();
            }
            dpItem[currentDel].gameObject.GetComponent<DPoints>().Id = 0;
        }
        //Msg();
        //SaveValues();
        RefreshDisplay();
    }
    public void ToggleDpArray()
    {
        if (currentDP == dp)
        {
            currentDP = dpItem;
            isItemDelivery = true;
        }
        else
        {
            currentDP = dp;
            isItemDelivery = false;
        }
    }
    public void AddDeliveryPoint()
    {
        if (counter < 3 - delCount) counter++;
        adddel.text = counter.ToString();

    }
    public void DeleteDelPoint()
    {
        if (counter > 0) counter--;
        adddel.text = counter.ToString();
    }


    public void HideDelCompletText()
    {
        delCompletText.SetActive(false);
    }

    public void HideFailText()
    {
        FailText.SetActive(false);
    }

    public void HideLimiterText()
    {
        deliveryLimiter.SetActive(false);
    }

    public void ShowMap()
    {
        miniMap.SetActive(true);
    }
    private void GenerateDP()
    {
        do
        {
            currentDel = GetRandomNbr();
            check = Array.Exists<int>(delArray, x => x == currentDel);
        } while (check);
        if (!check)
        {
            delArray[indexCount] = currentDel;

        }

    }

    public void CloseShop()
    {
        Shop_terminal.SetActive(false);
        HideCursor();
        miniMap.SetActive(true);

    }

    public void CloseMenus()
    {
        popup_Accept.SetActive(false);
        HideCursor();
        mapCanvas.SetActive(false);


    }

    public void Confirm()
    {
        miniMap.SetActive(false);
        ChooseNumDeliveryCanvas.SetActive(false);
        if (counter != 0)
        {
            delCount += counter;
            for (int i = 0; i < delCount; i++)
            {
                AcceptDelivery();
                SaveValues();
                RefreshDisplay();
                //DTWaypoint.SetActive(false);
            }
            ShowMiniGame();
            return;
        }
        //Invoke("ShowMap", 5);
        HideCursor();
        this.GetComponent<PlayerInput>().enabled = true;
    }

    public void AcceptDelivery()
    {
        //To do
        delArray[indexCount] = currentDel;
        deliveriesCanvas.SetActive(true);

        //Get a random delivery point

        GenerateDP();
        indexCount++;

        dpItem[currentDel].SetActive(true);

        //id de DP=currentdel
        dpItem[currentDel].gameObject.GetComponent<DPoints>().Id = currentDel;


        GameObject temp = dpItem[currentDel];
        temp.transform.GetChild(0).gameObject.layer = 6;
        temp.transform.GetChild(0).transform.GetChild(0).gameObject.layer = 6;
        indexCount = 1;
        inDelivery = 1;
        //CloseMenus();
    }

    /*public void RefuseDelivery()
    {
        CloseMenus();
        popup_Accept.SetActive(true);
        Dp[currentDel - 1].SetActive(false);
        termInteract = true;
        delArray[delCount] = 0;
    }*/

    void ShowMiniGame()
    {
        this.GetComponent<PlayerInput>().enabled = false;
        Interactions.minigame = true;
        minigame = true;
        miniGame.SetActive(true);
        miniMap.SetActive(false);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }
    public void CloseScoreTxt()
    {
        canvasScore.SetActive(false);
    }
    public void ShowScore()
    {
        scoreTxt.GetComponent<Text>().text = "You got " + score + " parcels !";
        canvasScore.SetActive(true);
    }

    public void CloseNumDelivery()
    {
        this.GetComponent<PlayerInput>().enabled = true;
        ChooseNumDeliveryCanvas.SetActive(false);
    }

    public int GetRandomNbr()
    {
        return UnityEngine.Random.Range(1, currentDP.Length);
    }

    void SaveValues()
    {
        PlayerPrefs.SetInt("Deliveries Completed", completed);
        PlayerPrefs.SetInt("Delivery Count", delCount);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("inDelivery", inDelivery);
        PlayerPrefs.SetInt("PlayerLife", playerLife);
        PlayerPrefs.SetInt("numLife", numLife);
        for (int i = 0; i < dpItem.Length; i++)
        {
            if (dpItem[i].activeInHierarchy)
            {
                PlayerPrefs.SetInt(i.ToString(), dpItem[i].gameObject.GetComponent<DPoints>().Id);
            }
        }
        PlayerPrefs.Save();
    }

    void LoadValues()
    {
        completed = PlayerPrefs.GetInt("Deliveries Completed", 0);
        delCount = PlayerPrefs.GetInt("Delivery Count", 0);
        inDelivery = PlayerPrefs.GetInt("inDelivery", 0);
        money = PlayerPrefs.GetInt("Money", 0);
        playerLife = PlayerPrefs.GetInt("PlayerLife", 100);
        numLife = PlayerPrefs.GetInt("numLife", 3);
        for (int i = 0; i < dpItem.Length; i++)
        {
            if (PlayerPrefs.GetInt(i.ToString(), 0) != 0)
            {
                dpItem[i].SetActive(true);
                dpItem[i].gameObject.GetComponent<DPoints>().Id = i;

            }
        }
    }

    void ResetValue()
    {
        this.gameObject.GetComponent<PlayerManager>().Health = 100;
        this.gameObject.GetComponent<PlayerManager>().HealthPoints = 100;
        delCount = 0;
        inDelivery = 0;
        completed = 0;
        indexCount = 0;
        currentDel = 0;
        playerLife = 100;
        numLife = 3;
        GameManager.score = 0;
        for (int i = 0; i < dpItem.Length; i++)
        {
            dpItem[i].SetActive(false);
            if (!dpItem[i].activeInHierarchy)
            {
                dpItem[i].gameObject.GetComponent<DPoints>().Id = 0;
                PlayerPrefs.SetInt(i.ToString(), 0);
            }
        }
        Array.Clear(delArray, 0, delArray.Length);
        SaveValues();
    }



    void Msg()
    {
        Debug.Log("delCount" + inDelivery);
        Debug.Log("completed" + completed);
        Debug.Log("delCount: " + delCount);
        Debug.Log("terminteract: " + termInteract);
        Debug.Log("delpoint: " + dpoint);
        //for (int i = 0; i < Dp.Length; i++) Debug.Log("dp" + i + ": " + PlayerPrefs.GetInt(i.ToString(), 0));
    }

    public void OnApplicationQuit()
    {
        SaveValues();
        PlayerPrefs.Save();
    }

    public void OnEsc()
    {
        if (esc == false)
        {
            deliveriesCanvas.SetActive(false);
            canvasEsc.SetActive(true);
            esc = true;
            ShowCursor();
        }
        else
        {
            deliveriesCanvas.SetActive(true);
            canvasEsc.SetActive(false);
            esc = false;
            HideCursor();
        }
    }

    private void HideCursor()
    {
        this.GetComponent<PlayerInput>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ShowCursor()
    {
        this.GetComponent<PlayerInput>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void toggleInventory()
    {
        Debug.Log("Inventory");
        inventory = !inventory;
        if (inventory)
        {
            ShowCursor();
            inventoryCanvas.SetActive(true);
        }
        else
        {
            HideCursor();
            inventoryCanvas.SetActive(false);
        }
    }
    private void HideScore()
    {
        canvasScore.GetComponent<Canvas>().enabled = false;
    }
    private void Update()
    {
        if (CountTimer.FailDelivery)
        {

            CountTimer.ResetTimer();
            FailDelivery();
            CountTimer.FailDelivery = false;
        }
        if (minigame)
        {
            score = GameManager.score;
            this.GetComponent<PlayerInput>().enabled = false;
        }
        if (endGame)
        {
            endGame = false;
            Invoke("HideScore", 10);
            miniMap.SetActive(true);
            this.GetComponent<PlayerInput>().enabled = true;
            GameObject[] cubes;
            cubes = GameObject.FindGameObjectsWithTag("MovingCube");
            foreach (GameObject element in cubes)
            {
                Destroy(element);
            }
            //SceneManager.UnloadSceneAsync(1);
        }
        if (Input.GetButtonDown("Inventory"))
        {
            toggleInventory();
        }
        if (Input.GetButtonDown("Cancel"))
        {

            OnEsc();
        }
    }
}
