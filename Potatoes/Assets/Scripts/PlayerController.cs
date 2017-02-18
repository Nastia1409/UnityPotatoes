using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Globals;
using UnityEditor;

public class PlayerController : MonoBehaviour
{

    public static GameObject CurrentlySelectedUnit;
    private Vector2 mouseDownPoint;
    static List<GameObject> _selectedPlayers = null;
    GlobalPlayersController _globalController;
    private void Awake()
    {
        mouseDownPoint = Vector2.zero;
        _selectedPlayers = new List<GameObject>();
        _globalController = GetComponent<GlobalPlayersController>();//GlobalPlayersController.Instance;
    }

    public static ArrayList CurrentlySelectedUnits = new ArrayList();


    public float moveSpeed;
    public bool isSelected;
    // Use this for initialization
    void Start()
    {
        //    if (_selectedPlayers == null)
    }


    // Update is called once per frame
    void Update()
    {
        GameObject currGameObj = this.gameObject;
        Vector2 currMousePos = Input.mousePosition;
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        Action<Vector2> locationAction = LocationHandler;
        if (hit.collider != null && hit.collider.transform.FindChild("Player"))
        {
            if (Input.GetMouseButtonDown(0))
                mouseDownPoint = hit.point;

            if (Input.GetMouseButtonUp(0) && didUserClickedLeftMouse(hit.point))
            {
                bool isRegisteredLocationEvent = _globalController.IsHandlerRegisteredFor(UpdateOption.Location, LocationHandler);
                if (hit.collider == transform.GetComponent<Collider2D>())
                {
                    if (isRegisteredLocationEvent)
                        _globalController.UnRegisterFromLocationUpdates(LocationHandler);
                    else
                        _globalController.RegisterForLocationUpdates(LocationHandler);
                    Debug.Log(hit.collider.name);
                }
                ////if current gameObject selected, but the user chose other object - deselect the current
                //else if (isRegisteredLocationEvent)
                //{
                //    _globalController.UnRegisterFromLocationUpdates(LocationHandler);
                //}

            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 v2 = Camera.main.ScreenToWorldPoint(currMousePos);
                if (didUserClickedLeftMouse(v2))
                    _globalController.ClearEventHandlers(UpdateOption.Location);
            }
        }
        //if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        //    _isShiftKeyDown = true;
        //if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        //    _isShiftKeyDown = false;

        //// if left button pressed...
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (isSelected)
        //    {
        //        isSelected = false;
        //        if (!_isShiftKeyDown)
        //        {
        //            _selectedPlayers.Clear();
        //        }
        //        _selectedPlayers.Add(this);
        //    }


        //}

        if (_globalController.IsHandlerRegisteredFor(UpdateOption.Location, LocationHandler))
        {
            float horizInput = Input.GetAxisRaw("Horizontal");
            float verInput = Input.GetAxisRaw("Vertical");

            if (horizInput > 0.5f || horizInput < -0.5f)
            {
                transform.Translate(new Vector3(horizInput * moveSpeed * Time.deltaTime, 0f, 0f));
            }

            if (verInput > 0.5f || verInput < -0.5f)
            {
                transform.Translate(new Vector3(0f, verInput * moveSpeed * Time.deltaTime, 0f));
            }
        }
    }

    private void OnMouseDown()
    {
    }

    //triggers by GlobalPlayersController when location is changed
    private void LocationHandler(Vector2 newLocation)
    {
        transform.position = Vector3.MoveTowards(transform.position, newLocation, 10000);
    }

    #region Helper Functions
    private bool didUserClickedLeftMouse(Vector2 hitPoint)
    {
        float clickZone = 1.3f;
        return (mouseDownPoint.x < hitPoint.x + clickZone && mouseDownPoint.x > hitPoint.x - clickZone)
                && (mouseDownPoint.y < hitPoint.y + clickZone && mouseDownPoint.y > hitPoint.y - clickZone);
    }

    private bool isShiftKeyDown()
    {
        return Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.RightShift);
    }
    #endregion
}
