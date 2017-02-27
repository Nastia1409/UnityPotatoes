using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class updatePath : MonoBehaviour {
    // Use this for initialization
    private GraphUpdateObject guo;
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        var guo = new GraphUpdateObject(GetComponent<Collider2D>().bounds);
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
	}
}
