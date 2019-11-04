using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWave : MonoBehaviour
{
    public System.Random rnd = new System.Random();
    int spiderTimer;
    int currentSpider;
    int spiderInitiationFreq;

    public GameObject spiderPrefab;
    GameObject[] spiders = new GameObject[40];
    //GameObject spider1;
    

    // Start is called before the first frame update
    void Start()
    {
        spiderTimer = 0;
        currentSpider = 0;
        currentSpider = 0;
        spiderInitiationFreq = 150;

    }

    // Update is called once per frame
    void Update()
    {

        spiderTimer += 1;
        spiderTimer = spiderTimer % spiderInitiationFreq;

        if(spiderTimer == 0)
        {
            float posX = Random.Range(-1542, -738);
            float posY = Random.Range(-369, -67);

            float[] zRoomBounds = new float[2] { -2688, -1977 };
            int indexZ = rnd.Next(0, 2);//Random # btw 0 and 1
            Debug.Log(indexZ);
            float posZ = zRoomBounds[indexZ];

            spiders[currentSpider] = Instantiate(spiderPrefab, new Vector3(posX, posY, posZ), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;
            spiders[currentSpider].transform.localScale = new Vector3(5, 5, 5);

            currentSpider++;
        }

        /*timer += 1;
        timer = timer % 999999;

        if(timer == 1000)
        {
            Destroy(spiders[0]);
        }

        if (timer == 2000)
        {
            Destroy(spiders[1]);
        }

        if (timer == 3000)
        {
            Destroy(spiders[2]);
        }*/
        //Debug.Log("timer: " + timer);
    }
}
