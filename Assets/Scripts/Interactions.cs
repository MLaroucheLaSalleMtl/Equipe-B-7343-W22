using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Interactions : MonoBehaviour
{
    GameManager gameManager;

    public static bool endGame = false;
    public static bool minigame = false;


    [SerializeField] public GameObject delCompletText;
    [SerializeField] public GameObject deliveryLimiter;
    [SerializeField] public GameObject DTWaypoint;
    [SerializeField] public GameObject mapCanvas;
    [SerializeField] public GameObject deliveriesCanvas;
    [SerializeField] public GameObject moneyCanvas;
    [SerializeField] public GameObject[] Dp;
    [SerializeField] public GameObject popup_Accept;
    [SerializeField] public GameObject popup_Complete;
    [SerializeField] public int currentDel;
    [SerializeField] public Text adddel;
    [SerializeField] private Text numDel;
    [SerializeField] private Text txtmoney;
    [SerializeField] public GameObject miniGame;
    [SerializeField] public GameObject miniMap;
    [SerializeField] public GameObject ChooseNumDeliveryCanvas;

    [SerializeField] public GameObject canvasScore;
    [SerializeField] public GameObject scoreTxt;

    [SerializeField] public static int score;

    [SerializeField] public GameObject Popup_Shop;
    [SerializeField] public GameObject Shop_terminal;

    bool shopInteract = false;

    bool esc = false;

    bool termInteract = false;
    bool dpoint=false;

    private int[] delArray = new int[3];

    int money=0;
    int delCount = 0;
    int indexCount = 0;
    int counter = 0;
    int completed = 0;

    bool check;

    private void Start()
    {
        gameManager = GameManager.instance;
        LoadValues();
        Msg();
        
        //ResetValue();
        //Msg();
        
        if (delCount != 0)
        {
            deliveriesCanvas.SetActive(true);
            DTWaypoint.SetActive(false);
        }
        RefreshDisplay();
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
        }
        if(other.gameObject.CompareTag("DPoint"))
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

    void RefreshDisplay()
    {
        numDel.text = "Delivered: " + completed.ToString()+"/"+delCount;
        txtmoney.text = money.ToString("D6") + "$";
    }



    public void OnTermInteract()
    {
        if (termInteract && PlayerPrefs.GetInt("inDelivery") == 0)
        {
            miniMap.SetActive(false);
            popup_Accept.SetActive(false);
            //deliveryTerm.SetActive(true);
            ChooseNumDeliveryCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            termInteract = false;
            
            

        }
        else if(shopInteract == true)
        {
            Shop_terminal.SetActive(true);
            Popup_Shop.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("inDelivery") == 1 && termInteract)
        {
            deliveryLimiter.SetActive(true);
            Invoke("HideLimiterText", 10);
        }

        else if (dpoint)
        {
            Debug.Log("currentdel: " + currentDel);
            Dp[currentDel].SetActive(false);
            popup_Complete.SetActive(false);
            completed++;
            dpoint = false;
            

            if (completed == delCount)
            {
                delCompletText.SetActive(true);
                deliveriesCanvas.SetActive(false);

                //msg deliveries completed
                Invoke("HideDelCompletText", 10);

                //set deliveries terminal waypoint
                DTWaypoint.SetActive(true);

                //argent proportieonnelle
                if (delCount == 1) money += 200 + ((200 * score) / 50);
                else if (delCount == 2) money += 460 + ((460 * score) / 50);
                else if (delCount == 3) money += 750 + ((750 * score) / 50);

                PlayerPrefs.SetInt("inDelivery",0);

                //reset values
                ResetValue();
                
                Array.Clear(delArray, 0, delArray.Length);
                
            }
            Dp[currentDel].gameObject.GetComponent<DPoints>().Id = 0;
        }
            //Msg();
            RefreshDisplay();
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
       
    public void CloseMenus()
    {
        popup_Accept.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        }
        //Invoke("ShowMap", 5);
    }

    public void AcceptDelivery()
    {
        //To do
        delArray[indexCount] = currentDel;
        deliveriesCanvas.SetActive(true);

        //Get a random delivery point

        GenerateDP();
        indexCount++;

        Dp[currentDel].SetActive(true);

        //id de DP=currentdel
        Dp[currentDel].gameObject.GetComponent<DPoints>().Id = currentDel;

        
        GameObject temp = Dp[currentDel];
        temp.transform.GetChild(0).gameObject.layer = 6;
        temp.transform.GetChild(0).transform.GetChild(0).gameObject.layer = 6;
        indexCount = 1;
        PlayerPrefs.SetInt("inDelivery", 1);
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
        ChooseNumDeliveryCanvas.SetActive(false );
    }

    public int GetRandomNbr()
    {
        return UnityEngine.Random.Range(1, Dp.Length);
    }

    void SaveValues()
    {
            PlayerPrefs.SetInt("Deliveries Completed", completed);
            PlayerPrefs.SetInt("Delivery Count", delCount);
            PlayerPrefs.SetInt("Money", money);
            for (int i = 0; i < Dp.Length; i++)
            {
                if (Dp[i].activeInHierarchy)
                {
                    PlayerPrefs.SetInt(i.ToString(), Dp[i].gameObject.GetComponent<DPoints>().Id);
                }
            }
    }

    void LoadValues()
    {
        completed = PlayerPrefs.GetInt("Deliveries Completed", 0);
        delCount = PlayerPrefs.GetInt("Delivery Count", 0);
        money = PlayerPrefs.GetInt("Money", 0);
        for(int i = 0; i < Dp.Length; i++)
        {
            if (PlayerPrefs.GetInt(i.ToString(), 0) != 0) 
                Dp[i].SetActive(true);
            
        }
    }

    void ResetValue()
    {
        delCount = 0;
        completed = 0;
        indexCount = 0;
        currentDel = 0;
        GameManager.score = 0;
        for (int i = 0; i < Dp.Length; i++)
        {
            Dp[i].gameObject.GetComponent<DPoints>().Id=0;            
            Dp[i].SetActive(false);
            SaveValues();
            Array.Clear(delArray, 0, delArray.Length);
        }
        SaveValues();
    }



    void Msg()
    {
        Debug.Log("completed" + completed);
        Debug.Log("delCount: " + delCount);
        Debug.Log("terminteract: " + termInteract);
        Debug.Log("delpoint: " + dpoint);
        for (int i = 0; i < Dp.Length; i++) Debug.Log("dp" + i + ": " + PlayerPrefs.GetInt(i.ToString(), 0));
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void OnEsc()
    {
        if(esc == false)
        {
            esc = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            esc = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void HideScore()
    {
        canvasScore.GetComponent<Canvas>().enabled = false;
        endGame = false;
    }
    private void Update()
    {
        /*if(minigame == false)
        {
            isClosed = true;
            CloseMiniGame();
        }*/
        if (minigame)
        {
            score = GameManager.score;
            this.GetComponent<PlayerInput>().enabled = false;
        }
        if (endGame)
        {
            Invoke("HideScore", 10);
            miniMap.SetActive(true);
            this.GetComponent<PlayerInput>().enabled = true;
        }
    }
 
}
