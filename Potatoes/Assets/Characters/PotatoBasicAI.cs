using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using Assets.Scripts.Interfaces;

public class PotatoBasicAI : MonoBehaviour, IPotatoAI
{
    public Transform followTarget;

    private bool moving = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Jump") && moving)
        //{
        //    goTo(followTarget);
        //    moving = !moving;
        //}
        //else if (Input.GetButtonDown("Jump") && !moving)
        //{
        //    stop();
        //    moving = !moving;
        //}
    }


    // go to function
    public void goTo(Transform target)
    {
        GetComponentInParent<AILerp>().target = target;
    }
    void stop()
    {
        GetComponentInParent<AILerp>().target = null;
    }

    public void OnRaycastHit()
    {
        print("player was hit");
    }

    //used by data structures like HashSet, List and etc . To find an element/check if it exists there.
    public bool Equals(PotatoBasicAI other)
    {
        return this.transform.Equals(other);
    }
}
