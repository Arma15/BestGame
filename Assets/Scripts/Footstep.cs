using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
	public CharacterController controller;
	
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false) 
		{
			GetComponent<AudioSource>().Play();
		}
    }
}
