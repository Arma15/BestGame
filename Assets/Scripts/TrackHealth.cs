using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackHealth : MonoBehaviour
{
    public TextDisplay textDisplay;
    public Slider healthBarSlider;
    public float maxHealth = 100;//Max health should match max health in health bar slider settings 
    public float health;

    int regainHealthTimer;
    int regainHealthFrequency;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        regainHealthTimer = 0;
        regainHealthFrequency = 200;
        healthBarSlider.image.color = Color.green;
    }

    public void takeHit(float hitLevel)
    {
        health -= hitLevel;
        adjustSlider();
        //Debug.Log("Hit "+health);
        if (health < 0.01)
        {
            textDisplay.transitionLevel("Goodnight Son :( ");
            textDisplay.pause(1000);
            Application.Quit();
        }

    }

    public void adjustSlider()
    {
        healthBarSlider.value = health;
        healthBarSlider.image.color = Color.Lerp(Color.red, Color.green, healthBarSlider.value / 30);
    }

    public void regainHealth(float gain)
    {
        health += gain;
        adjustSlider();
    }

    // Update is called once per frame
    void Update()
    {
        //Regain health as time passes
        if(health < maxHealth)
        {
            regainHealthTimer += 1;
            regainHealthTimer = regainHealthTimer % regainHealthFrequency;

            if (regainHealthTimer == 0)
            {
                regainHealth(0.7f);
            }
        }

    }
}

