using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPistol : MonoBehaviour
{
    public SpiderWave spiderWave;
    AudioSource laserBurst;
    public Camera cam;									// camera reference with the ray
	
    // Start is called before the first frame update
    void Start()
    {
		cam = Camera.main;								// get the camera object pointer (using main camera tag here)
		laserBurst = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //Draw ray
            //Debug.DrawRay(transform.position, transform.forward * 20, Color.red, 0.05f);
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10000, Color.red, 0.05f);
            
            //Play sound
            laserBurst.Play(0);
			
			RaycastHit hit;
			Vector3 ray = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));  // get the reference of the cam

            //if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            // use camera's position instead of the gun position (gun's coordinate doesn't change when moving the character -> not be able to shoot while moving)
            if (Physics.Raycast(ray, cam.transform.forward, out hit, Mathf.Infinity))
            //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Debug.Log("Hit " + hit.collider.gameObject.name, hit.collider.gameObject);
                //Debug.Log("Hit " + hit.collider.gameObject.name+" at distance " + hit.distance);

                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red, 0.05f);

                //Kill spider
                //Debug.Log("Hit " + hit.collider.gameObject.name);
                //Destroy(hit.collider.gameObject);
                Destroy(hit.collider.gameObject.transform.parent.gameObject);

                /*for (int i = 0; i < spiderWave.spiders.Length; i++) {
                    if (spiderWave.spiders[i] != null)
                    {
                        Debug.Log("Spider "+i+" lives");
                    }
                    else
                    {
                        Debug.Log("Spider "+i+" dead");
                    }
                }*/

            }
        }
    }
}
