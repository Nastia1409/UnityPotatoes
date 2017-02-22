using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement2D : MonoBehaviour {
    public float scrollZone = 15;
    public float scrollSpeed = 1;
    public float zoomSpeed = 3;
    public bool allowMouseScroll = true;
    public bool allowZoom = true;

    public float xMax = 4; // left and right
    public float xMin = -4;

    public float yMax = 4; //up and down
    public float yMin = -4;

    public float zMax = -1; // zoom in and zoom out
    public float zMin = -5;

    private Vector3 desiredPosition;

    private void Start()
    {
        desiredPosition = transform.position;
    }

    private void Update()
    {
        float x = 0, y = 0, z = 0;
        float speed = scrollZone * Time.deltaTime;


        if (Input.mousePosition.x < scrollZone & allowMouseScroll == true) //camera left and right movement if mouse in position
            x -= (speed * scrollSpeed);
        else if (Input.mousePosition.x > Screen.width - scrollZone & allowMouseScroll == true)
            x += (speed * scrollSpeed);

        if (Input.mousePosition.y < scrollZone & allowMouseScroll == true)
            y -= speed * scrollSpeed;
        else if (Input.mousePosition.y > Screen.height - scrollZone & allowMouseScroll == true) // camera up and down movement if mouse in position
            y += speed * scrollSpeed;

        if (allowZoom==true)
        {
            z += Input.GetAxis("Mouse ScrollWheel")*zoomSpeed;
        }

        Vector3 move = new Vector3(x, y, z) + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.y = Mathf.Clamp(move.y, yMin, yMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);
    }

}
