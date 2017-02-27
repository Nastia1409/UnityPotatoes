using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class potatoBasicAI : MonoBehaviour {
    

    public Transform followTarget;

    private bool moving = true;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump") && moving)
        {
            goTo(followTarget);
            moving = !moving;
        }
        else if(Input.GetButtonDown("Jump") && !moving)
        {
            stop();
            moving = !moving;
        }
	}


    // go to function
    void goTo ( Transform target )
    {
        GetComponentInParent<AILerp>().target = target;
    }
    void stop ()
    {
        GetComponentInParent<AILerp>().target = null;
    }
}
