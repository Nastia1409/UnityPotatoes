using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public static GameObject CurrentlySelectedUnit;
    private Vector2 mouseDownPoint;
    static List<GameObject> _selectedPlayers = null;

    private void Awake()
    {
        mouseDownPoint = Vector2.zero;
        _selectedPlayers = new List<GameObject>();
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
        if (hit)
        {
            if (Input.GetMouseButtonDown(0))
                mouseDownPoint = hit.point;

            if (Input.GetMouseButtonUp(0) && didUserClickedLeftMouse(hit.point))
            {
                if (hit.collider == transform.GetComponent<Collider2D>())
                {
                    if (_selectedPlayers.Contains(currGameObj))
                        _selectedPlayers.Remove(currGameObj);
                    else
                    {
                        if (!isShiftKeyDown())
                            _selectedPlayers.Clear();
                        _selectedPlayers.Add(gameObject);
                    }
                    Debug.Log(hit.collider.name);
                }
                //if current gameObject selected, but the user chose other object - deselect the current
                else if (_selectedPlayers.Contains(currGameObj))
                {
                    _selectedPlayers.Remove(currGameObj);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector2 v2 = Camera.main.ScreenToWorldPoint(currMousePos);
                if (didUserClickedLeftMouse(v2))
                    _selectedPlayers.Clear();
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

        if (_selectedPlayers.Contains(this.gameObject))
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

    private void otherPlayerWasChosen()
    {

    }

    private void OnMouseDown()
    {
        isSelected = true;
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
