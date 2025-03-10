﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiders : MonoBehaviour
{
    public System.Random rnd = new System.Random();
    int numSpiders;
    int spiderInitiationTimer;
    //int spiderRotationTimer;
    int currentSpider;
    int spiderInitiationFreq;
    //int spiderRotationFreq;

    public GameObject house;
    public GameObject player;

    public GameObject spiderPrefab;
    //AudioSource spiderAppearSound;

    GameObject[] spiders;
    float[] spiderMoveSpeed;
    int[] spiderBodyRotationFreq;
    int[] spiderRotationTimer;
    //GameObject spider1;


    int randFreq()
    {
        return rnd.Next(90, 180);
    }

    // Start is called before the first frame update
    void Start()
    {
        numSpiders = 40;
        spiderInitiationTimer = 0;
        //spiderRotationTimer = 0;
        currentSpider = 0;
        spiderInitiationFreq = randFreq();
        //spiderRotationFreq = 10;

        spiders = new GameObject[numSpiders];
        spiderMoveSpeed = new float[numSpiders];
        spiderBodyRotationFreq = new int[numSpiders];
        spiderRotationTimer = new int[numSpiders];

        //spiderAppearSound = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        spiderInitiationTimer += 1;
        spiderInitiationTimer = spiderInitiationTimer % spiderInitiationFreq;

        //spiderRotationTimer += 1;
        //spiderRotationTimer = spiderRotationTimer % spiderRotationFreq;

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

            //spiders[currentSpider].transform.gameObject.AddComponent(MeshCollider);
            //spiderAppearSound.Play();

            //Mesh mesh = (MeshFilter)spiders[currentSpider].GetComponent<MeshFilter>().mesh;
            /*Mesh mesh = spiderPrefab.GetComponent<MeshFilter>().mesh;
            if (mesh != null)
            {
                // Add a new MeshCollider to the object
                //MeshCollider meshCollider = spiders[currentSpider].AddComponent<MeshCollider>();
                MeshCollider meshCollider = spiders[currentSpider].AddComponent<MeshCollider>();

                // Finaly we set the Mesh in the MeshCollider
                meshCollider.sharedMesh = mesh;
            }*/
            //SphereCollider sc = spiders[currentSpider].AddComponent<SphereCollider>() as SphereCollider;

            currentSpider++;
            spiderInitiationFreq = randFreq();//Change the initiation frequency for next spider
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
                    if (x == playerX && z == playerZ)
                    {

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




                //Debug.Log("Player pos(" + playerX + ", " + playerY + ", " + playerZ + ")");
                //Debug.Log("Spider " + i + " pos("+x+", "+y+", "+z+"): House y "+houseY);
            }
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
