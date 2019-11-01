using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{


    public Vector3 pos;
    public Vector3 ang;
    float distanceFromCamera;
    float posX;
    float posY;
    float posZ;
    float posXCam;
    float posYCam;
    float posZCam;
    float angX;
    float angY;
    float angZ;
    //float camXCompleteRotation;

    public float degToRad(float angle)
    {
        return Mathf.PI * angle / 180.0f;
 
    }
    //int dir = 1;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("sin 90 " + Mathf.Sin(Mathf.PI) + "cos 90 "+ Mathf.Cos(Mathf.PI));

        //Laser gun coords
        pos = transform.position;
        posX = pos.x;
        posY = pos.y;
        posZ = pos.z;
        //posX = -1274;
        //posY = -342;
        //posZ = -2102;

        //Camera coords
        posXCam = Camera.main.gameObject.transform.position.x;
        posYCam = Camera.main.gameObject.transform.position.y;
        posZCam = Camera.main.gameObject.transform.position.z;
        //posXCam = -1278;
        //posYCam = -322;
        //posZCam = -2128;

        //Laser gun angles
        //ang = transform.localEulerAngles;
        angX = ang.x;
        angY = ang.y;
        angZ = ang.z;
        //angX = -93.106f;
        //angY = -73.50598f;
        //angZ = 4.4739f;

        //distanceFromCamera = posZ - posZCam;

        //distanceFromCamera = Mathf.Sqrt(Mathf.Pow((posX - posXCam),2.0f) + Mathf.Pow((posY - posZCam), 2.0f));
        distanceFromCamera = Mathf.Sqrt(Mathf.Pow((posX - posXCam), 2.0f) + Mathf.Pow((posZ - posZCam), 2.0f));

        Debug.Log("Laser gun distance from camera " + distanceFromCamera);
        //Debug.Log("Laser Gun translation coords ("+x+","+y+","+z+")");
        //Debug.Log("Laser Gun rotation coords (" + angleX + "," + angleY + "," + angleZ + ")");
        //Debug.Log("on start camera coords(" + posXCam + ", " + posYCam + ", " + posZCam + ")");
        //camXCompleteRotation = Camera.main.transform.eulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        ang = transform.localEulerAngles;
        float angX = ang.x;
        float angY = ang.y;
        float angZ = ang.z;
        float rotateBy = 0.8f;
        float nRotateBy = rotateBy * -1;


        //Mouse moves
        //if (Input.GetAxis("Mouse X") < 0)
        if (Input.GetKey("left"))
        {
            //Code for action on mouse moving left
            //print("Mouse moved left");
            transform.Rotate(0, 0, nRotateBy);//Rotate gun counterclockwise
            Camera.main.transform.Rotate(0, nRotateBy, 0);//Rotate camera counterclockwise

            //Debug.Log("y anlge " + Camera.main.transform.eulerAngles.y);
            //Debug.Log("sin theta: " + Mathf.Sin(degToRad(nRotateBy)) + "cos theta: " + Mathf.Cos(degToRad(nRotateBy)));
  

            //float dx = distanceFromCamera * Mathf.Sin(degToRad(nRotateBy));
            //float dz = distanceFromCamera * Mathf.Cos(degToRad(nRotateBy));


            //Update camera coordinates
            //posXCam -= dx;
            //posZCam -= dz;
            posXCam = posX - distanceFromCamera * Mathf.Sin(degToRad(Camera.main.transform.eulerAngles.y));
            posZCam = posZ - distanceFromCamera * Mathf.Cos(degToRad(Camera.main.transform.eulerAngles.y));

            //Debug.Log("dx: "+dx+" dz: " + dz);
            //Debug.Log("theta: "+ Camera.main.transform.eulerAngles.y + " x: " + Mathf.Sin(degToRad(Camera.main.transform.eulerAngles.y)) + " z: " + Mathf.Cos(degToRad(Camera.main.transform.eulerAngles.y)));
            //Debug.Log("on update camera coords(" + posXCam + ", " + posYCam + ", " + posZCam + ")");

            GameObject.Find("Main Camera").transform.position = new Vector3(posXCam, posYCam, posZCam);//Move camera slightly
            
        }

        //if (Input.GetAxis("Mouse X") > 0)
        if (Input.GetKey("right"))
        {
            //Code for action on mouse moving right
            //print("Mouse moved right");
            transform.Rotate(0, 0, rotateBy);//Rotate gun clockwise
            Camera.main.transform.Rotate(0, rotateBy, 0);//Rotate camera clockwise

            posXCam = posX - distanceFromCamera * Mathf.Sin(degToRad(Camera.main.transform.eulerAngles.y));
            posZCam = posZ - distanceFromCamera * Mathf.Cos(degToRad(Camera.main.transform.eulerAngles.y));

            GameObject.Find("Main Camera").transform.position = new Vector3(posXCam, posYCam, posZCam);//Move camera slightly

        }

        //if (Input.GetAxis("Mouse Y") > 0)
        /*if (Input.GetKey("up"))
        {
            //Code for action on mouse moving up
            //print("Mouse moved up");
            //if(angX > 75)
            //{

            //Debug.Log("gun angle("+angX+", "+angY+", "+angZ+")");
                camXCompleteRotation =  (camXCompleteRotation + nRotateBy) % 360;
                if(camXCompleteRotation < 0) { camXCompleteRotation += 360; }
                transform.Rotate(0, nRotateBy, 0);//Rotate gun up
                Camera.main.transform.Rotate(nRotateBy, 0, 0);//Rotate camera up

            //Debug.Log("cam x angle: "+ Camera.main.transform.eulerAngles.x);
            Debug.Log("cam x angle: " + camXCompleteRotation);
                //Update camera coordinates
                posYCam = posY - distanceFromCamera * Mathf.Sin(degToRad(camXCompleteRotation));
                posZCam = posZ - distanceFromCamera * Mathf.Cos(degToRad(camXCompleteRotation));

                GameObject.Find("Main Camera").transform.position = new Vector3(posXCam, posYCam, posZCam);//Move camera slightly

            //}

        }

        //if (Input.GetAxis("Mouse Y") < 0)
        if (Input.GetKey("down"))
        {
            //Code for action on mouse moving down
            //print("Mouse moved down");
            if (angX > 153)
            {

                //Debug.Log("gun angle(" + angX + ", " + angY + ", " + angZ + ")");
                transform.Rotate(0, rotateBy, 0);//Rotate gun down
                Camera.main.transform.Rotate(rotateBy, 0, 0);//Rotate camera down
            }

        }*/


        /*
        if (dir == 1)
        {//Clockwise
            transform.Rotate(0, 0, 0.8f);//Rotate gun clockwise
            Camera.main.transform.Rotate(0, 0.8f, 0);//Rotate camera clockwise
        }
        else
        {
            transform.Rotate(0, 0, -0.8f);//Rotate camera counterclockwise
            Camera.main.transform.Rotate(0, -0.8f, 0);
        }



        //Update the angles


        if(angZ >= 180)
        {
            dir = -1;//Counter clockwise
        }
        if (angZ <= 120)
        {
            dir = 1;//Counter clockwise
        }*/

        //Debug.Log("Laser Gun rotation coords (" + angX + "," + angY + "," + angZ + ")");
    }
}
