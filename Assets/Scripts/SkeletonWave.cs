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
        public float dx;
        public float dy;
        public float dz;

    }

    public TrackHealth trackHealth;
    public TextDisplay textDisplay;
    public SpiderWave spiderWave;

    public System.Random rnd = new System.Random();
    int maxNumSkeletons;
    int origNumSkeletons;
    public int numSkeletonsLeft;

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
    int sceneTransitionSettings;
    public Collider playerCollider;

    //int[] skeletonRotationFreq;
    //int[] skeletonRotationTimer;
    // Start is called before the first frame update

    int randFreq(int min, int max)
    {
        return rnd.Next(min, max);
    }

    void Start()
    {
        maxNumSkeletons = 80;
        //skeletons = new GameObject[maxNumSkeletons];
        skeletons = new Skeleton[maxNumSkeletons];
        //skeletonRotationFreq = new int[maxNumSkeletons];
        //skeletonRotationTimer = new int[maxNumSkeletons];

        //skeletonRotationTimer[0] = 0;
        //skeletonRotationFreq[0] = 150;


        origNumSkeletons = 40;
        numSkeletonsLeft = origNumSkeletons;
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
            skeletons[i].rotationFreq = randFreq(10, 30);
            skeletons[i].rightArmDir = 0;
            skeletons[i].translateSpeed = skeletons[0].rotationFreq / 1000f;
        }
        /*skeletons[0].rotationTimer = 0;
        skeletons[0].rotationFreq = 60;
        skeletons[0].rightArmDir = 0;
        skeletons[0].translateSpeed = skeletons[0].rotationFreq/3000f;*/

        /*skeletons[0].clone = Instantiate(skeletonPrefab, new Vector3(0, 150, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
        skeletons[0].clone.transform.position = new Vector3(skeletons[0].clone.transform.position.x, skeletons[0].clone.transform.position.y-50, skeletons[0].clone.transform.position.z);
        skeletons[0].clone.transform.localScale = new Vector3(2, 2, 2);*/

        //skeletons[0].transform.localScale = new Vector3(1, 1, 1);


        //Debug.Log(skeletons[0].transform.GetChild(0).name);

        //skeletons[0].transform.GetChild(0).localScale = new Vector3(2, 2, 2);

        //skeletons[0].transform.GetChild(0).eulerAngles.x = 90;

        //skeletons[0].transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y+8, transform.rotation.eulerAngles.z);

    }

    // Update is called once per frame
    void Update()
    {
        //int i = 0;
        //skeletonRotationTimer[i] += 1;
        //skeletonRotationTimer[i] = skeletonRotationTimer[i] % skeletonRotationFreq[i];
//Debug.Log("text display paused: " + textDisplay.paused + " level: " + spiderWave.level);
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
//Debug.Log("timer " + skeletonInitiationTimer);
            skeletonInitiation();
            //Debug.Log("num spiders left "+numSpidersLeft);
        }


        float gunX = player.transform.position.x;
        float gunY = player.transform.position.y;
        float gunZ = player.transform.position.z;

        for (int i = 0; i < origNumSkeletons; i++)
        {
            if (skeletons[i].clone != null)
            {
                skeletons[i].rotationTimer += 1;
                skeletons[i].rotationTimer = skeletons[i].rotationTimer % skeletons[i].rotationFreq;


                skeletons[i].x = skeletons[i].clone.transform.position.x;
                skeletons[i].y = skeletons[i].clone.transform.position.y;
                skeletons[i].z = skeletons[i].clone.transform.position.z;


                skeletons[i].dx = gunX - skeletons[i].x;
                skeletons[i].dy = gunY - skeletons[i].y;
                skeletons[i].dz = gunZ - skeletons[i].z;


                Vector3 dirVector = new Vector3(skeletons[i].dx, gunY - 50, skeletons[i].dz);
                //Vector3 forwardDir = skeletons[i].clone.transform.forward;

                moveBones(i, dirVector, skeletons[i].dx, skeletons[i].dz);
            }

        }











        //Rotate skeleton to align with translation vector
        //Vector3 dirVector = new Vector3(dx, dy, dz);
        //Vector3 skeletonForward = skeletons[i].clone.transform.forward;

        /*Vector3 dirVector = new Vector3(dx, gunY-50, dz);
        Vector3 forwardDir = skeletons[i].clone.transform.forward;*/

        //forwardDir = Quaternion.Euler(0, -45, 0) * forwardDir;//Rotate to align with skeleton's actual forward facing direction

        /*Vector2 dirVectorXZ = new Vector2(dx, dz);
        Vector2 forwardDirXZ = new Vector2(forwardDir.x, forwardDir.z);*/
        //Vector2 skeletonForward = new Vector2() skeletons[i].clone.transform.forward;


        //Debug.DrawRay(skeletons[i].clone.transform.position, dirVector * 200, Color.red, 0.5f);
        //Debug.DrawRay(skeletons[i].clone.transform.position, forwardDir  * 200, Color.green, 0.5f);


        //Quaternion q = Quaternion.FromToRotation(skeletons[i].clone.transform.right, dirVector);
        //skeletons[i].clone.transform.rotation = q * skeletons[i].clone.transform.rotation;

        //float angleBetweenDirVectorAndForward = Vector2.Angle(dirVectorXZ, forwardDirXZ);
        //float angleBetweenDirVectorAndForward = Vector3.Angle(dirVector, forwardDir);

        //if (angleBetweenDirVectorAndForward < 1 || angleBetweenDirVectorAndForward > 11)
        //{
        //skeletons[i].clone.transform.rotation = Quaternion.Euler(skeletons[i].clone.transform.rotation.eulerAngles.x, skeletons[i].clone.transform.rotation.eulerAngles.y - 10, skeletons[i].clone.transform.rotation.eulerAngles.z);
        //}



        /*for (int j = 0; j <= 7; j++)
        {
            skeletons[i].clone.transform.GetChild(j).rotation = Quaternion.LookRotation(dirVector);
        }
        skeletons[i].clone.transform.rotation = Quaternion.LookRotation(dirVector);*/

        //skeletons[i].clone.transform.rotation = Quaternion.Euler(skeletons[i].clone.transform.rotation.eulerAngles.x, skeletons[i].clone.transform.rotation.eulerAngles.y + , skeletons[i].clone.transform.rotation.eulerAngles.z);


        //Debug.Log("Angle b/w " + angleBetweenDirVectorAndForward);
        //Debug.Log("Skeleton rotation around y: " + skeletons[i].clone.transform.rotation.eulerAngles.y);
        //Debug.Log("")

        //Debug.Log("Forward: "+"("+skeletonForward.x * Mathf.Rad2Deg + ","+skeletonForward.y * Mathf.Rad2Deg + ","+skeletonForward.z * Mathf.Rad2Deg + ")");
        //Debug.Log("dirVector: " + "(" + dirVector.x + "," + dirVector.y + "," + dirVector.z + ")");



        //moveBones(i, dirVector, dx, dz);





        //Translate skeleton
        //skeletons[i].x += dx * skeletons[i].translateSpeed;
        //skeletons[i].z += dz * skeletons[i].translateSpeed;


        //Debug.Log("("+x+","+y+","+z+")");
        //skeletons[i].clone.transform.position = new Vector3(skeletons[i].x, skeletons[i].y, skeletons[i].z);




        /*if (skeletons[i].rotationTimer == 0)
        {
            //Right arm 2
            skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x+8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            //Left arm 7
            skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x-8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);


            //skeletons[i].transform.GetChild(1).transform.Rotate(0, 30 * Time.deltaTime, 0);
            //skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
            //Debug.Log(0);
        }

        if (skeletons[i].rotationTimer == skeletons[i].rotationFreq / 3)
        {

            skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x-8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            //Left arm 7
            skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x+8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            //skeletons[i].transform.GetChild(1).transform.Rotate(0, -30 * Time.deltaTime, 0);
            //skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
            //Debug.Log(1);
        }

        if (skeletons[i].rotationTimer == skeletons[i].rotationFreq * 2 / 3)
        {

            skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            //Left arm 7
            skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            //skeletons[i].transform.GetChild(1).transform.Rotate(0, -30 * Time.deltaTime, 0);
            //skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
            //Debug.Log(1);
        }*/
    }



    void skeletonInitiation()
    {
        //Spawn new skeleton
        if (skeletonInitiationTimer == 0 && currentSkeleton < origNumSkeletons)
        {

            float posX = Random.Range(-400, 400);
            float posY = 100;
            float posZ = Random.Range(-400, 400);
            skeletons[currentSkeleton].clone = Instantiate(skeletonPrefab, new Vector3(posX, posY, posZ), Quaternion.Euler(0, 0, 0)) as GameObject;
            //skeletons[currentSkeleton].clone.transform.position = new Vector3(skeletons[currentSkeleton].clone.transform.position.x, skeletons[currentSkeleton].clone.transform.position.y - 50, skeletons[currentSkeleton].clone.transform.position.z);
            skeletons[currentSkeleton].clone.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

            currentSkeleton++;
            skeletonInitiationFreq = randFreq(minInitiationFreq, maxInitiationFreq);//Change the initiation frequency for next skeleton
        }

    }



    void moveBones1(int i, Vector3 dirVector, float dx, float dz)
    {


        if (skeletons[i].clone != null)
        {
            if (skeletons[i].rotationTimer == 0)
            {

                for (int j = 0; j <= 7; j++)
                {
                    skeletons[i].clone.transform.GetChild(j).transform.GetChild(0).rotation = Quaternion.LookRotation(dirVector);

                }
                //skeletons[i].clone.transform.rotation = Quaternion.LookRotation(dirVector);




                if (skeletons[i].rightArmDir == 0)
                {
                    //Debug.Log("Phase1 forward");
                    //Right arm 
                    skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    //Left arm
                    skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    //Right humerus
                    skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
                    //Right lower leg in tandem with right humerus
                    skeletons[i].clone.transform.GetChild(4).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 9, transform.rotation.eulerAngles.z);
                    //Left humerus
                    skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    //Skull
                    skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);

                    //Sternum-hip
                    skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);


                }
                else if (skeletons[i].rightArmDir == 1)
                {
                    //Debug.Log("Phase1 backward");
                    //Right arm 
                    skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    //Left arm
                    skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    //Right humerus
                    skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    //Left humerus
                    skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
                    //Left lower leg in tandem with left humerus
                    skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 9, transform.rotation.eulerAngles.z);
                    //Skull
                    skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);

                    //Sternum-hip
                    skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);
                }


                //Translate skeleton
                translateSkeleton(i, dx, dz);

            }


            if (skeletons[i].rotationTimer == skeletons[i].rotationFreq / 2)
            {//Return to neutral position

                for (int j = 0; j <= 7; j++)
                {
                    //skeletons[i].clone.transform.GetChild(j).rotation = Quaternion.LookRotation(dirVector);
                    skeletons[i].clone.transform.GetChild(j).transform.GetChild(0).rotation = Quaternion.LookRotation(dirVector);
                    //Debug.Log(skeletons[i].clone.transform.GetChild(j).transform.GetChild(0).name);
                    //skeletons[i].clone.transform.GetChild(j).rotation = Quaternion.FromToRotation(Vector3.forward, dirVector);

                }
                //skeletons[i].clone.transform.rotation = Quaternion.LookRotation(dirVector);
                //skeletons[i].clone.transform.rotation = Quaternion.FromToRotation(Vector3.forward, dirVector);

                if (skeletons[i].rightArmDir == 0)
                {
                    //Debug.Log("Phase2 forward");
                    //Right arm 
                    skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    //Left arm
                    skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    //Right humerus
                    skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    //Left humerus
                    skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 8, transform.rotation.eulerAngles.z);
                    //Left lower leg in tandem with left humerus
                    //skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x-8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    //Right lower leg back to neutral
                    skeletons[i].clone.transform.GetChild(4).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 9, transform.rotation.eulerAngles.z);

                    //Skull
                    skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);

                    //Sternum-hip
                    skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);
                    skeletons[i].rightArmDir = 1;

                }
                else if (skeletons[i].rightArmDir == 1)
                {
                    //Debug.Log("Phase2 backward");
                    //Right arm 
                    skeletons[i].clone.transform.GetChild(2).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 8);
                    //Left arm
                    skeletons[i].clone.transform.GetChild(7).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 8);
                    //Right humerus
                    skeletons[i].clone.transform.GetChild(3).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x - 8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    //Right lower leg in tandem with right humerus
                    //skeletons[i].clone.transform.GetChild(4).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x-8, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                    //Left lower leg back to neutral
                    skeletons[i].clone.transform.GetChild(1).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 9, transform.rotation.eulerAngles.z);
                    //Left humerus
                    skeletons[i].clone.transform.GetChild(0).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 8, transform.rotation.eulerAngles.z);
                    //Skull
                    skeletons[i].clone.transform.GetChild(5).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + 1);

                    //Sternum-hip
                    skeletons[i].clone.transform.GetChild(6).rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - 1);
                    skeletons[i].rightArmDir = 0;
                }

                //Translate skeleton
                translateSkeleton(i, dx, dz);
            }

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
