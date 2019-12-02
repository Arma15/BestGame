using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWave : MonoBehaviour
{

    public TrackHealth trackHealth;
    public TextDisplay textDisplay;
    public System.Random rnd = new System.Random();
    int origNumSpiders;
    public int numSpidersLeft;
    public int maxNumSpiders = 80;
    int spiderInitiationTimer;
    int currentSpider;
    public int minInitiationFreq;
    public int maxInitiationFreq;
    int spiderInitiationFreq;

    public GameObject house;
    public GameObject player;
    public GameObject grandma;

    public GameObject spiderPrefab;

    public GameObject[] spiders;
    float[] spiderMoveSpeed;
    int[] spiderBodyRotationFreq;
    int[] spiderRotationTimer;
    int[] whoToAttack;
    float killRadius;
    //public int hits;
    float hitPenalty;
    public int level;
    public float moveSpeedMin;
    public float moveSppedMax;

    public AudioClip grunt;
    public AudioClip scream;
    public AudioSource myAudio;


    int randFreq(int min, int max)
    {
        return rnd.Next(min, max);
    }

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        origNumSpiders = 40;
        numSpidersLeft = origNumSpiders;
        spiderInitiationTimer = 0;
        currentSpider = 0;
        minInitiationFreq = 60;
        maxInitiationFreq = 110;
        spiderInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//For first spider

        spiders = new GameObject[maxNumSpiders];
        spiderMoveSpeed = new float[maxNumSpiders];
        spiderBodyRotationFreq = new int[maxNumSpiders];
        spiderRotationTimer = new int[maxNumSpiders];
        whoToAttack = new int[maxNumSpiders];
        killRadius = 30f;
        //hits = 0;
        hitPenalty = 6f;
        moveSpeedMin = 0.008f;
        moveSppedMax = 0.012f;

        myAudio = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

        if (!textDisplay.paused)
        {
            spiderInitiationTimer += 1;
            spiderInitiationTimer = spiderInitiationTimer % spiderInitiationFreq;
    //Debug.Log("init timer " + spiderInitiationTimer);
            spiderAction();
            //Debug.Log("num spiders left "+numSpidersLeft);
        }


        if (numSpidersLeft == 0 && level < 3)
        {
            level++;
            //Print next level 
            //Debug.Log("Level " + level);
            if(level < 3)
            {
                currentSpider = 0;
            }
            
            //spiderInitiationTimer = 0;

            if (level == 2)
            {
                textDisplay.transitionLevel("Not so fast kido, Prepare for Meaner Spiders!!");
                textDisplay.pause(500);
                origNumSpiders = 50;
                numSpidersLeft = origNumSpiders;

                minInitiationFreq = 30;
                maxInitiationFreq = 80;
                spiderInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//For first spider


                killRadius = 35f;
                hitPenalty = 7f;
                moveSpeedMin = 0.012f;
                moveSppedMax = 0.016f;
            }

            if (level == 3)
            {
                //textDisplay.gameOver("Game Over!!");
                textDisplay.transitionLevel("Your Nightmare is Just About to Become Real!!");
                textDisplay.pause(500);
            }
        }
    }


    void spiderAction()
    {
        //Spawn new spider
        if (spiderInitiationTimer == 0 && currentSpider < origNumSpiders)
        {
            int wall = rnd.Next(0, 2);//Random # btw 0 and 1 for x walls or z walls
            float posX;
            float posZ;

            if (wall == 0)
            {//If z walls
                posX = Random.Range(-400, 400);
                float[] zRoomBounds = new float[2] { -350, 400 };
                int indexZ = rnd.Next(0, 2);//Random # btw 0 and 1
                posZ = zRoomBounds[indexZ];
            }
            else
            {//If x walls
                posZ = Random.Range(-400, 400);
                float[] xRoomBounds = new float[2] { -350, 400 };
                int indexX = rnd.Next(0, 2);//Random # btw 0 and 1
                posX = xRoomBounds[indexX];
            }
            float posY = Random.Range(0, 50);
            float moveSpeed = Random.Range(moveSpeedMin, moveSppedMax);

            spiders[currentSpider] = Instantiate(spiderPrefab, new Vector3(posX, posY, posZ), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;
            spiders[currentSpider].transform.localScale = new Vector3(10, 10, 10);
            spiderMoveSpeed[currentSpider] = moveSpeed;
            spiderBodyRotationFreq[currentSpider] = rnd.Next(9, 15);//Random # btw 9 and 14
            spiderRotationTimer[currentSpider] = 0;
            whoToAttack[currentSpider] = rnd.Next(0, 2);//0 for player 1 for grandma

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
                float playerX = player.transform.position.x;
                float playerY = player.transform.position.y;
                float playerZ = player.transform.position.z;

                float grandmaX = grandma.transform.position.x;
                float grandmaY = grandma.transform.position.y;
                float grandmaZ = grandma.transform.position.z;

                float dx0 = playerX - x;
                float dz0 = playerZ - z;
                Vector3 dirVector0 = new Vector3(dx0, y, dz0);

                float dx1 =grandmaX - x;
                float dz1 = grandmaZ - z;
                Vector3 dirVector1 = new Vector3(dx1, y, dz1);

                //Orient spider with the direction vector of its forward movement vector (dx, y, dz)
                if(whoToAttack[i] == 0)
                {//Attack player
                    //Debug.Log("Spider " + i + " attacking player");
                    spiders[i].transform.rotation = Quaternion.LookRotation(dirVector0);
                }
                else
                {//Attack grandma
                    //Debug.Log("Spider " + i + " attacking grandma");
                    spiders[i].transform.rotation = Quaternion.LookRotation(dirVector1);
                }
                

                //Move spider to floor level
                if (y > houseY)
                {
                    y -= 0.4f;
                    spiders[i].transform.position = new Vector3(x, y, z);
                }


                //Move spider towards player or grandma
                if (y <= houseY)
                {

                    if(whoToAttack[i] == 0)
                    {
                        x += dx0 * spiderMoveSpeed[i];
                        z += dz0 * spiderMoveSpeed[i];
                    }
                    else
                    {
                        x += dx1 * spiderMoveSpeed[i];
                        z += dz1 * spiderMoveSpeed[i];
                    }


                    spiders[i].transform.position = new Vector3(x, y, z);


                    /*Reduce player health if spider gets to player or grandma*/
                    float xSeparation0 = System.Math.Abs(x - playerX);
                    float zSeparation0 = System.Math.Abs(z - playerZ);

                    if (xSeparation0 < killRadius && zSeparation0 < killRadius)
                    {
                        Destroy(spiders[i]);
                        numSpidersLeft--;
                        //grunt.Play(0);//Play grunt sound
                        myAudio.PlayOneShot(grunt, 0.7f);
                        trackHealth.takeHit(hitPenalty);

                    }

                    float xSeparation1 = System.Math.Abs(x - grandmaX);
                    float zSeparation1 = System.Math.Abs(z - grandmaZ);

                    if (xSeparation1 < killRadius && zSeparation1 < killRadius)
                    {
                        Destroy(spiders[i]);
                        numSpidersLeft--;
                        //scream.Play(0);//Play grunt sound
                        myAudio.PlayOneShot(scream, 0.7f);
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
