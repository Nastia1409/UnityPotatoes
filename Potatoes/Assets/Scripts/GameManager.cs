using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void ChangeTarget(Vector3 target, bool isFollowTarget);
    private event ChangeTarget OnTargetChange;
    private HashSet<GameObject> chosenAIList;

    // Use this for initialization
    void Start()
    {
        chosenAIList = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //left mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit)
            {
                GameObject hitedGameObj = hit.collider.gameObject;
                //search for IPotatoAI component in hited gameObject and all its children
                var pbAI = hitedGameObj.GetComponentInChildren<IPotatoAI>();
                //if component exists register its function goTo to OnTargetChange
                if (pbAI != null)
                {
                    //if that was first click on AI add it to data
                    if (!chosenAIList.Contains(hitedGameObj))
                        AddAIPlayer(hitedGameObj, pbAI);
                    //if player clicked on AI second time - remove it from data.
                    else
                        RemoveAIPlayer(hitedGameObj, pbAI);
                }
            }
            //if user clicked on empty space remove all selected AIs
            else
            {
                chosenAIList.Clear();
                if(OnTargetChange != null)
                {
                    //Set target to null
                    OnTargetChange(Vector3.zero, false);
                    //Remove all registered functions
                    OnTargetChange = null;

                }
            }

        }
        //right mouse button down
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit)
            {
                //if there functions registered for this event then raise it
                if (OnTargetChange != null)
                    OnTargetChange(hit.transform.position, true);
            }
            //if user clicked on object without collider 
            else
            {
                //TODO
                //create transform data from mouse click location
                // Transform position = new Transform();
                Vector3 pos = Input.mousePosition;
                pos = Camera.main.ScreenToWorldPoint(pos);
                pos.z = 0;

                if (OnTargetChange != null)
                    OnTargetChange(pos, true);
            }
        }

    }

    private void AddAIPlayer(GameObject hitedGameObj, IPotatoAI pbAI)
    {
        chosenAIList.Add(hitedGameObj);
        OnTargetChange += pbAI.goTo;
    }

    private void RemoveAIPlayer(GameObject hitedGameObj, IPotatoAI pbAI)
    {
        chosenAIList.Remove(hitedGameObj);
        OnTargetChange -= pbAI.goTo;
    }
}
