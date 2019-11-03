using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    public float turnSpeed = 50f;
    //public Transform target;

    // Start is called before the first frame update
    void Start() 
    {

    }

    // Update is called once per frame
    void Update()
    {

        //transform.LookAt(target);

        //if (Input.GetAxis("Mouse X") < 0)
        if (Input.GetKey("left"))
        {
            //Code for action on moving left
            transform.Rotate(new Vector3(0, -1, 0), turnSpeed * Time.deltaTime);

        }

        //if (Input.GetAxis("Mouse X") > 0)
        if (Input.GetKey("right"))
        {
            //Code for action on moving right
            transform.Rotate(new Vector3(0, 1, 0), turnSpeed * Time.deltaTime);


        }

        //if (Input.GetAxis("Mouse Y") > 0)
        if (Input.GetKey("up"))
        {
            //float y = Camera.main.transform.localEulerAngles.y;
            //Debug.Log(y);
            //Code for action on moving up
            if (transform.eulerAngles.y > -307)
            {
                transform.Rotate(new Vector3(-1, 0, 0), turnSpeed * Time.deltaTime);
            }

        }

        //if (Input.GetAxis("Mouse Y") < 0)
        if (Input.GetKey("down"))
        {
            //Code for action on moving down
            transform.Rotate(new Vector3(1, 0, 0), turnSpeed * Time.deltaTime);

        }
    }
}
