using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {
    public float scrollZone = 15;
    public float scrollSpeed = 1;
    public bool allowMouseScroll = true;

    public float xMax = 8;
    public float xMin = 0;
    //public float yMax = 15;
    //public float yMin = 5;
    public float zMax = 8;
    public float zMin = 0;
   
    private Vector3 desiredPosition;
    private void Start()
    {
        desiredPosition = transform.position;
    }

    private void Update()
    {
        float x = 0, y = 0, z = 0;
        float speed = scrollZone * Time.deltaTime;

        if (Input.mousePosition.x < scrollZone & allowMouseScroll == true)
            x -= speed;
        else if (Input.mousePosition.x > Screen.width - scrollZone & allowMouseScroll == true)
            x += speed;

        if (Input.mousePosition.y < scrollZone & allowMouseScroll == true)
            z -= speed;
        else if (Input.mousePosition.y > Screen.height - scrollZone & allowMouseScroll == true)
            z += speed;

        //y += Input.GetAxis("Mouse ScrollWheel");

        Vector3 move = new Vector3(x, y, z) + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        //move.y = Mathf.Clamp(move.y, yMin, yMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);
    }
}
