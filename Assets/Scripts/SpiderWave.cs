using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWave : MonoBehaviour
{

    public TrackHealth trackHealth;
    public System.Random rnd = new System.Random();
    int numSpiders;
    int spiderInitiationTimer;
    int currentSpider;
    public int minInitiationFreq;
    public int maxInitiationFreq;
    int spiderInitiationFreq;

    public GameObject house;
    public GameObject player;

    public GameObject spiderPrefab;

    public GameObject[] spiders;
    float[] spiderMoveSpeed;
    int[] spiderBodyRotationFreq;
    int[] spiderRotationTimer;
    float killRadius;
    //public int hits;
    float hitPenalty;
    public int level;
    public float moveSpeedMin;
    public float moveSppedMax;


    int randFreq(int min, int max)
    {
        return rnd.Next(min, max);
    }

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        numSpiders = 40;
        spiderInitiationTimer = 0;
        currentSpider = 0;
        minInitiationFreq = 90;
        maxInitiationFreq = 180;
        spiderInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//For first spider

        spiders = new GameObject[numSpiders];
        spiderMoveSpeed = new float[numSpiders];
        spiderBodyRotationFreq = new int[numSpiders];
        spiderRotationTimer = new int[numSpiders];
        killRadius = 30f;
        //hits = 0;
        hitPenalty = 4f;
        moveSpeedMin = 0.001f;
        moveSppedMax = 0.005f;

    }

    // Update is called once per frame
    void Update()
    {

        spiderInitiationTimer += 1;
        spiderInitiationTimer = spiderInitiationTimer % spiderInitiationFreq;

        spiderAction();

        if(spiders.Length == 0)
        {
            level++;
            //Print next level Not so fast kido, prepare for meaner spiders!!
            //currentSpider = 0;
            //spiderInitiationTimer = 0;

            if (level == 2)
            {
                numSpiders = 50;
               
                minInitiationFreq = 100;
                maxInitiationFreq = 190;
                spiderInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//For first spider


                killRadius = 30f;
                hitPenalty = 5f;
                moveSpeedMin = 0.003f;
                moveSppedMax = 0.007f;
            }
        }
    }


    void spiderAction()
    {
        if (spiderInitiationTimer == 0 && currentSpider < numSpiders)
        {
            float posX = Random.Range(-400, 400);
            float posY = Random.Range(0, 50);
            float moveSpeeed = Random.Range(0.001f, 0.005f);

            float[] zRoomBounds = new float[2] { -350, 400 };
            int indexZ = rnd.Next(0, 2);//Random # btw 0 and 1
            //Debug.Log(indexZ);
            float posZ = zRoomBounds[indexZ];

            spiders[currentSpider] = Instantiate(spiderPrefab, new Vector3(posX, posY, posZ), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;
            spiders[currentSpider].transform.localScale = new Vector3(10, 10, 10);
            spiderMoveSpeed[currentSpider] = moveSpeeed;
            spiderBodyRotationFreq[currentSpider] = rnd.Next(9, 15);//Random # btw 9 and 14
            spiderRotationTimer[currentSpider] = 0;

            currentSpider++;
            spiderInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//Change the initiation frequency for next spider
        }


        //Move spiders
        for (int i = 0; i < spiders.Length; i++)
        {
            if (spiders[i] != null)
            {
                float x = spiders[i].transform.position.x;
                float y = spiders[i].transform.position.y;
                float z = spiders[i].transform.position.z;
                float houseY = house.transform.position.y;

                //Move spider to floor level
                if (y > houseY)
                {
                    y -= 0.4f;
                    spiders[i].transform.position = new Vector3(x, y, z);
                }


                float playerX = player.transform.position.x;
                float playerY = player.transform.position.y;
                float playerZ = player.transform.position.z;

                //Move spider towards player
                if (y <= houseY)
                {
                    float dx = playerX - x;
                    float dz = playerZ - z;
                    x += dx * spiderMoveSpeed[i];
                    z += dz * spiderMoveSpeed[i];

                    spiders[i].transform.position = new Vector3(x, y, z);

                    //Orient spider with the direction vector of its forward movement vector (dx, y, dz)
                    /*float orientAngle = Mathf.Atan2(dz, dx) * Mathf.Rad2Deg;
                    float d = 0.000001f;
                    if(spiders[i].transform.rotation.eulerAngles.y > (orientAngle - d) || spiders[i].transform.rotation.eulerAngles.y < (orientAngle + d))
                    {
                        spiders[i].transform.Rotate(0, 0, orientAngle);
                    }*/


                    /*Reduce player health if spider gets to player*/
                    float xSeparation = System.Math.Abs(x - playerX);
                    float zSeparation = System.Math.Abs(z - playerZ);

                    if (xSeparation < killRadius && zSeparation < killRadius)
                    {
                        Destroy(spiders[i]);
                        trackHealth.takeHit(hitPenalty);

                    }

                }


                //Rotate spider side to side
                spiderRotationTimer[i] += 1;
                spiderRotationTimer[i] = spiderRotationTimer[i] % spiderBodyRotationFreq[i];

                //if (spiderRotationTimer == 0)
                if (spiderRotationTimer[i] == 0)
                {
                    spiders[i].transform.Rotate(0, 180 * Time.deltaTime, 0);
                }
                //if(spiderRotationTimer == spiderRotationFreq / 2)
                if (spiderRotationTimer[i] == spiderBodyRotationFreq[i] / 2)
                {
                    spiders[i].transform.Rotate(0, -180 * Time.deltaTime, 0);
                }

            }
        }
    }

}
