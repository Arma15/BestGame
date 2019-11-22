using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public GameObject levelTransitionTextGameObj;
    public GameObject backgroundPanel;

    public bool paused;
    int timer;
    int pauseTime;

    //public Text levelTransitionText = levelTransitionTextGameObj.GetComponent<Text>();
    //public Text levelTransitionText;

    // Start is called before the first frame update
    void Start()
    {
        //Text levelTransitionText = GetComponent<Text>();
        levelTransitionTextGameObj.SetActive(false);
        backgroundPanel.SetActive(false);

        paused = false;
        timer = 0;

    }


    public void transitionLevel(string message)
    {       
        levelTransitionTextGameObj.GetComponent<Text>().text = message;
        levelTransitionTextGameObj.SetActive(true);
        backgroundPanel.SetActive(true);
    }

    public void gameOver(string message)
    {
        levelTransitionTextGameObj.GetComponent<Text>().text = message;
        levelTransitionTextGameObj.SetActive(true);
        backgroundPanel.SetActive(true);
    }


    public void pause(int time)
    {
        paused = true;
        pauseTime = time;
        //Let time pass
    }

    // Update is called once per frame
    void Update()
    {

        //levelTransitionText.SetActive(true);
        //levelTransitionText.text = "hello";

        //Paused is false by default. The moment it becomes true...
        if (paused)
        {
            timer++;
        }

        if(timer == pauseTime)
        {
            pauseTime = 0;
            paused = false;

            //Remove panel and text 
            levelTransitionTextGameObj.SetActive(false);
            backgroundPanel.SetActive(false);
        }
        
    }
}
