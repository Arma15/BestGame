using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWave : MonoBehaviour
{

    int timer;
    //Object spiderPrefab = AssetDatabase.LoadAssetAtPath("Assets/Models/18754_Spider_in_defensive_stance_V1", typeof(GameObject));
    //Object spiderPrefab = Resources.Load("Assets/Models/18754_Spider_in_defensive_stance_V1", typeof(GameObject));
    //Object spiderPrefab = Resources.Load("Assets/Models/18754_Spider_in_defensive_stance_V1");
    public GameObject spiderPrefab;
    GameObject[] spiders = new GameObject[20];
    //GameObject spider1;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        //GameObject spider1 = Instantiate(spiderPrefab, new Vector3(-1025, -349, -2573), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;
        //spider1 = Instantiate(spiderPrefab, new Vector3(-1025, -359, -2573), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;
        spiders[0] = Instantiate(spiderPrefab, new Vector3(-1025, -359, -2573), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;
        spiders[1] = Instantiate(spiderPrefab, new Vector3(-990, -359, -2573), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;
        spiders[2] = Instantiate(spiderPrefab, new Vector3(-1100, -359, -2573), Quaternion.Euler(-76.095f, -0.195f, -31.17f)) as GameObject;

        //spider1.transform.position = Vector3(-1025, -349, -2573);
        //spider1.transform.localScale = new Vector3(10, 10, 10);
        spiders[0].transform.localScale = new Vector3(10, 10, 10);
        spiders[1].transform.localScale = new Vector3(10, 10, 10);
        spiders[2].transform.localScale = new Vector3(10, 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
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
        }
        //Debug.Log("timer: " + timer);
    }
}
