using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{

    /*public float moveSpeed = 8;

    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;*/


    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Rotate
        /*yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);//Restric rotation to this range

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        //Translate
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 move = transform.right * vert + transform.forward * hor;//Base the up and right direction on the position of a fixed object such as the bed

        transform.Translate(move * moveSpeed);*/



        //if (Input.GetAxis("Mouse X") < 0)
        /*if (Input.GetKey("left"))
        {
            //transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        //if (Input.GetAxis("Mouse X") > 0)
        if (Input.GetKey("right"))
        {
            //transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            transform.Translate(Vector3.left * -moveSpeed * Time.deltaTime);
        }

        //if (Input.GetAxis("Mouse Y") > 0)
        if (Input.GetKey("up"))
        {
            //transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        
        if (Input.GetKey("down"))
        {
            //transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
        }*/
    }
}
