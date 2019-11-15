using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPistol : MonoBehaviour
{
    AudioSource laserBurst;
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {

            //Play sound
            laserBurst = GetComponent<AudioSource>();
            laserBurst.Play(0);


            //if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Debug.Log("Hit " + hit.collider.gameObject.name, hit.collider.gameObject);
                //Debug.Log("Hit " + hit.collider.gameObject.name+" at distance " + hit.distance);



                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.DrawLine(new Vector3(0,0,0), new Vector3(1,1,1), Color.white, 100f);
                //Debug.DrawLine(transform.position, transform.forward, Color.white, 100f);
                //Debug.DrawRay(transform.position, transform.forward, Color.green, 1000f);



                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10000, Color.red);


                //Kill spider
                Destroy(hit.collider.gameObject);
                //numSpiders--;
            }
        }


    }
}
