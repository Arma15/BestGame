using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonWave : MonoBehaviour
{

    public struct Skeleton
    {//Represents a skeleton and all its attributes
        public int id;
        public int rotationFreq;
        public int rotationTimer;
        public GameObject clone;
        public int rightArmDir;
        public float translateSpeed;
        public float x;
        public float y;
        public float z;
        public float dx0;
        public float dy0;
        public float dz0;
        public float dx1;
        public float dy1;
        public float dz1;
        public int whoToAttack;

    }

    public TrackHealth trackHealth;
    public TextDisplay textDisplay;
    public SpiderWave spiderWave;

    public System.Random rnd = new System.Random();
    int maxNumSkeletons;
    int origNumSkeletons;
    public int numSkeletonsLeft;
    float killRadius;
    float hitPenalty;

    public int skeletonInitiationTimer;
    public int currentSkeleton;
    public int minInitiationFreq;
    public int maxInitiationFreq;
    public int skeletonInitiationFreq;

    //public GameObject[] skeletons;
    public Skeleton[] skeletons;
    public GameObject skeletonPrefab;
    public GameObject player;
    public GameObject gun;
    public GameObject grandma;
    int sceneTransitionSettings;
    public Collider playerCollider;

    int randFreq(int min, int max)
    {
        return rnd.Next(min, max);
    }

    void Start()
    {
        maxNumSkeletons = 80;
        skeletons = new Skeleton[maxNumSkeletons];

        origNumSkeletons = 50;
        numSkeletonsLeft = origNumSkeletons;
        killRadius = 60f;
        hitPenalty = 8f;
        skeletonInitiationTimer = 0;
        currentSkeleton = 0;
        minInitiationFreq = 60;
        maxInitiationFreq = 110;
        skeletonInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//For first skeleton
        sceneTransitionSettings = 0;

        playerCollider = GetComponent<Collider>();

        for (int i = 0; i < origNumSkeletons; i++)
        {
            skeletons[i].rotationTimer = 0;
            //skeletons[i].rotationFreq = randFreq(20, 60);
            skeletons[i].rotationFreq = randFreq(5, 15);
            skeletons[i].rightArmDir = 0;
            skeletons[i].translateSpeed = skeletons[0].rotationFreq / 1000f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (numSkeletonsLeft == 0)
        {
            textDisplay.transitionLevel("Good Job Kid, You Survived Your Nightmare :)");
            textDisplay.pause(1000);
            Application.Quit();
        }

        if (!textDisplay.paused && spiderWave.level == 3)
        {
            if(sceneTransitionSettings == 0)
            {//Sets the scene once
                //playerCollider.enabled = false;//Disable collider to allow player move outside
                //player.GetComponent(CharacterCollider).isTrigger = false;
                //player.transform.position = new Vector3(627, 160, 399.84f);
                //player.transform.rotation = Quaternion.Euler(0, -253.4f, 0);
                //sceneTransitionSettings++;
            }


            skeletonInitiationTimer += 1;
            skeletonInitiationTimer = skeletonInitiationTimer % skeletonInitiationFreq;
            skeletonInitiation();
            //Debug.Log("num spiders left "+numSpidersLeft);
        }


        float gunX = player.transform.position.x;
        float gunY = player.transform.position.y;
        float gunZ = player.transform.position.z;

        float grandmaX = grandma.transform.position.x;
        float grandmaY = grandma.transform.position.y;
        float grandmaZ = grandma.transform.position.z;

        for (int i = 0; i < origNumSkeletons; i++)
        {
            if (skeletons[i].clone != null)
            {
                skeletons[i].rotationTimer += 1;
                skeletons[i].rotationTimer = skeletons[i].rotationTimer % skeletons[i].rotationFreq;


                skeletons[i].x = skeletons[i].clone.transform.position.x;
                skeletons[i].y = skeletons[i].clone.transform.position.y;
                skeletons[i].z = skeletons[i].clone.transform.position.z;


                skeletons[i].dx0 = gunX - skeletons[i].x;
                skeletons[i].dy0 = gunY - skeletons[i].y;
                skeletons[i].dz0 = gunZ - skeletons[i].z;

                skeletons[i].dx1 = grandmaX - skeletons[i].x;
                skeletons[i].dy1 = grandmaY - skeletons[i].y;
                skeletons[i].dz1 = grandmaZ - skeletons[i].z;



                //Orient spider with the direction vector of its forward movement vector (dx, y, dz)
                Vector3 dirVector;

                if (skeletons[i].whoToAttack == 0)
                {//Attack player
                    dirVector = new Vector3(skeletons[i].dx0, grandmaY - 5, skeletons[i].dz0);
                    //Vector3 forwardDir = skeletons[i].clone.transform.forward;

                    moveBones(i, dirVector, skeletons[i].dx0, skeletons[i].dz0);
                }
                else
                {//Attack grandma
                    dirVector = new Vector3(skeletons[i].dx1, grandmaY - 5, skeletons[i].dz1);
                    //Vector3 forwardDir = skeletons[i].clone.transform.forward;

                    moveBones(i, dirVector, skeletons[i].dx1, skeletons[i].dz1);
                }


            
                /*Reduce player health if spider gets to player or grandma*/
                float xSeparation0 = System.Math.Abs(skeletons[i].x - gunX);
                float zSeparation0 = System.Math.Abs(skeletons[i].z - gunZ);

                if (xSeparation0 < killRadius && zSeparation0 < killRadius)
                {
                    Destroy(skeletons[i].clone);
                    numSkeletonsLeft--;
                    spiderWave.myAudio.PlayOneShot(spiderWave.grunt, 0.7f);
                    trackHealth.takeHit(hitPenalty);
                }

                float xSeparation1 = System.Math.Abs(skeletons[i].x - grandmaX);
                float zSeparation1 = System.Math.Abs(skeletons[i].z - grandmaZ);

                if (xSeparation1 < killRadius && zSeparation1 < killRadius)
                {
                    Destroy(skeletons[i].clone);
                    numSkeletonsLeft--;
                    spiderWave.myAudio.PlayOneShot(spiderWave.scream, 0.7f);
                    trackHealth.takeHit(hitPenalty);

                }


            }

        }

    }



    void skeletonInitiation()
    {
        //Spawn new skeleton
        if (skeletonInitiationTimer == 0 && currentSkeleton < origNumSkeletons)
        {

            float posX = Random.Range(-400, 400);
            float posY = 150;
            float posZ = Random.Range(-400, 400);
            skeletons[currentSkeleton].clone = Instantiate(skeletonPrefab, new Vector3(posX, posY, posZ), Quaternion.Euler(0, 0, 0)) as GameObject;
            //skeletons[currentSkeleton].clone.transform.position = new Vector3(skeletons[currentSkeleton].clone.transform.position.x, skeletons[currentSkeleton].clone.transform.position.y - 50, skeletons[currentSkeleton].clone.transform.position.z);
            skeletons[currentSkeleton].clone.transform.localScale = new Vector3(3.1f, 3.1f, 3.1f);
            skeletons[currentSkeleton].whoToAttack = rnd.Next(0, 2);//0 for player, 1 for grandma

            currentSkeleton++;
            skeletonInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//Change the initiation frequency for next skeleton
        }

    }



    void moveBones(int i, Vector3 dirVector, float dx, float dz)
    {


        if (skeletons[i].clone != null)
        {
            if (skeletons[i].rotationTimer == 0)
            {

                for (int j = 0; j <= 7; j++)
                {
                    if (skeletons[i].clone.transform.GetChild(j) != null)
                    {
                        skeletons[i].clone.transform.GetChild(j).transform.GetChild(0).rotation = Quaternion.LookRotation(dirVector);
                    }
                }
                //skeletons[i].clone.transform.rotation = Quaternion.LookRotation(dirVector);




                if (skeletons[i].rightArmDir == 0)
                {
                    //Debug.Log("Phase1 forward");
                    //Right arm 
                    if (skeletons[i].clone.transform.GetChild(2) != null)
                    {
                        skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    }
                    //Left arm
                    if (skeletons[i].clone.transform.GetChild(7) != null)
                    {
                        skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    }
                    //Right humerus
                    if (skeletons[i].clone.transform.GetChild(3) != null)
                    {
                        skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
                    }
                    //Right lower leg in tandem with right humerus
                    if (skeletons[i].clone.transform.GetChild(4) != null)
                    {
                        skeletons[i].clone.transform.GetChild(4).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 9, transform.rotation.eulerAngles.z);
                    }
                    //Left humerus
                    if (skeletons[i].clone.transform.GetChild(0) != null)
                    {
                        skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    }
                    //Skull
                    if (skeletons[i].clone.transform.GetChild(5) != null)
                    {
                        skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);
                    }

                    //Sternum-hip
                    if (skeletons[i].clone.transform.GetChild(6) != null)
                    {
                        skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);
                    }

                }
                else if (skeletons[i].rightArmDir == 1)
                {
                    //Debug.Log("Phase1 backward");
                    //Right arm 
                    if (skeletons[i].clone.transform.GetChild(2) != null)
                    {
                        skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    }
                    //Left arm
                    if (skeletons[i].clone.transform.GetChild(7) != null)
                    {
                        skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    }
                    //Right humerus
                    if (skeletons[i].clone.transform.GetChild(3) != null)
                    {
                        skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    }
                    //Left humerus
                    if (skeletons[i].clone.transform.GetChild(0) != null)
                    {
                        skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
                    }
                    //Left lower leg in tandem with left humerus
                    if (skeletons[i].clone.transform.GetChild(1) != null)
                    {
                        skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 9, transform.rotation.eulerAngles.z);
                    }
                    //Skull
                    if (skeletons[i].clone.transform.GetChild(5) != null)
                    {
                        skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);
                    }
                    //Sternum-hip
                    if (skeletons[i].clone.transform.GetChild(6) != null)
                    {
                        skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);

                    }
                }

                //Translate skeleton
                translateSkeleton(i, dx, dz);

            }


            if (skeletons[i].rotationTimer == skeletons[i].rotationFreq / 2)
            {//Return to neutral position

                for (int j = 0; j <= 7; j++)
                {
                    //skeletons[i].clone.transform.GetChild(j).rotation = Quaternion.LookRotation(dirVector);
                    if (skeletons[i].clone.transform.GetChild(j) != null)
                    {
                        Debug.Log(j + " alive");
                        skeletons[i].clone.transform.GetChild(j).transform.GetChild(0).rotation = Quaternion.LookRotation(dirVector);
                    }
                    else
                    {
                        Debug.Log(j + " dead");
                    }
                    //Debug.Log(skeletons[i].clone.transform.GetChild(j).transform.GetChild(0).name);
                    //skeletons[i].clone.transform.GetChild(j).rotation = Quaternion.FromToRotation(Vector3.forward, dirVector);

                }
                //skeletons[i].clone.transform.rotation = Quaternion.LookRotation(dirVector);
                //skeletons[i].clone.transform.rotation = Quaternion.FromToRotation(Vector3.forward, dirVector);

                if (skeletons[i].rightArmDir == 0)
                {
                    //Debug.Log("Phase2 forward");
                    //Right arm 
                    if (skeletons[i].clone.transform.GetChild(2) != null)
                    {
                        skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    }
                    //Left arm
                    if (skeletons[i].clone.transform.GetChild(7) != null)
                    {
                        skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    }
                    //Right humerus
                    if (skeletons[i].clone.transform.GetChild(3) != null)
                    {
                        skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    }
                    //Left humerus
                    if (skeletons[i].clone.transform.GetChild(0) != null)
                    {
                        skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
                    }
                    //Left lower leg in tandem with left humerus
                    //skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x-8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    //Right lower leg back to neutral
                    if (skeletons[i].clone.transform.GetChild(4) != null)
                    {
                        skeletons[i].clone.transform.GetChild(4).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 9, transform.rotation.eulerAngles.z);
                    }

                    //Skull
                    if (skeletons[i].clone.transform.GetChild(5) != null)
                    {
                        skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);
                    }

                    //Sternum-hip
                    if (skeletons[i].clone.transform.GetChild(6) != null)
                    {
                        skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);
                    }
                    skeletons[i].rightArmDir = 1;

                }
                else if (skeletons[i].rightArmDir == 1)
                {
                    //Debug.Log("Phase2 backward");
                    //Right arm 
                    if (skeletons[i].clone.transform.GetChild(2) != null)
                    {
                        skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    }
                    //Left arm
                    if (skeletons[i].clone.transform.GetChild(7) != null)
                    {
                        skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    }
                    //Right humerus
                    if (skeletons[i].clone.transform.GetChild(3) != null)
                    {
                        skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    }
                    //Right lower leg in tandem with right humerus
                    //skeletons[i].clone.transform.GetChild(4).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x-8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    //Left lower leg back to neutral
                    if (skeletons[i].clone.transform.GetChild(1) != null)
                    {
                        skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 9, transform.rotation.eulerAngles.z);
                    }
                    //Left humerus
                    if (skeletons[i].clone.transform.GetChild(0) != null)
                    {
                        skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    }
                    //Skull
                    if (skeletons[i].clone.transform.GetChild(5) != null)
                    {
                        skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);
                    }
                    //Sternum-hip
                    if (skeletons[i].clone.transform.GetChild(6) != null)
                    {
                        skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);
                    }
                    skeletons[i].rightArmDir = 0;
                }

                //Translate skeleton
                translateSkeleton(i, dx, dz);
            }

        }


    }


    void translateSkeleton(int i, float dx, float dz)
    {

        skeletons[i].x += dx * skeletons[i].translateSpeed;
        skeletons[i].z += dz * skeletons[i].translateSpeed;

        skeletons[i].clone.transform.position = new Vector3(skeletons[i].x, skeletons[i].y, skeletons[i].z);

        //Debug.Log(skeletons[i].clone.transform.position.x);
    }


}
