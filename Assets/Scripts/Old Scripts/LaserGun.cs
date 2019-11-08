using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{

    public float turnSpeed = 50f;


    public float degToRad(float angle)
    {
        return Mathf.PI * angle / 180.0f;

    }



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


        //Mouse moves
        //if (Input.GetAxis("Mouse X") < 0)
        if (Input.GetKey("left"))
        {
            //Code for action on moving left
            //transform.Rotate(new Vector3(0, 0, 1), turnSpeed * Time.deltaTime);

        }

        //if (Input.GetAxis("Mouse X") > 0)
        if (Input.GetKey("right"))
        {
            //Code for action on moving right
            //transform.Rotate(new Vector3(0, 0, -1), turnSpeed * Time.deltaTime);


        }

        //if (Input.GetAxis("Mouse Y") > 0)
        if (Input.GetKey("up"))
        {

            //Code for action on moving up
            //transform.Rotate(new Vector3(0, -1, 0), turnSpeed * Time.deltaTime);

        }

        //if (Input.GetAxis("Mouse Y") < 0)
        if (Input.GetKey("down"))
        {
            //Code for action on moving down
            //transform.Rotate(new Vector3(0, 1, 0), turnSpeed * Time.deltaTime);

        }


    }
}
