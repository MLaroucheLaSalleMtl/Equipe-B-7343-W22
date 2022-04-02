using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountTimer : MonoBehaviour
{
    [SerializeField] public Text Show_timer;
    private float Timestart = 30;
    

    // Start is called before the first frame update
    void Start()
    {
        Show_timer.text = Timestart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timestart <= 0) 
        {
            Timestart = 0;
        }
        else
        {
             Timestart -= Time.deltaTime;
        }
        
        Show_timer.text = Mathf.Round(Timestart).ToString();
    }
}
