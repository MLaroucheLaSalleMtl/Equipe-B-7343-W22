using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountTimer : MonoBehaviour
{
    
    public static bool FailDelivery = false;
    [SerializeField] public GameObject timerCanvas;
    [SerializeField] public Text timer;
    public static float Timestart = 300;
    float minutes;
    float seconds;
    int inDelivery;
    public void Toggle_Timer()
    {
        if (timer.IsActive())
        {
            timerCanvas.gameObject.SetActive(false);
            Timestart = 300;
        }
        else if (Interactions.delCount > 0)
        {
            timerCanvas.gameObject.SetActive(true);
            
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        Timestart = PlayerPrefs.GetFloat("Time",300);
        inDelivery = PlayerPrefs.GetInt("inDelivery");
        minutes = PlayerPrefs.GetFloat("Minutes");
        seconds = PlayerPrefs.GetFloat("Seconds");
        timer.text = Timestart.ToString();
        if(inDelivery == 1)
        {
            timerCanvas.gameObject.SetActive(true);
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
    public static void ResetTimer()
    {
        Timestart = 300;
    }
    // Update is called once per frame
    void Update()
    {
        inDelivery = PlayerPrefs.GetInt("inDelivery");
        minutes = Mathf.FloorToInt(Timestart / 60);
        seconds = Mathf.FloorToInt(Timestart % 60);
        if (Timestart <= 0) 
        {
            ResetTimer();
            Toggle_Timer();
            FailDelivery = true;
        }
        else
        {
            if (!FailDelivery && inDelivery == 1)
            {
                Timestart -= Time.deltaTime;
            }
        }

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        PlayerPrefs.SetFloat("Time", Timestart);
        PlayerPrefs.SetFloat("Minutes", minutes);
        PlayerPrefs.SetFloat("Seconds", seconds);
    }
}
