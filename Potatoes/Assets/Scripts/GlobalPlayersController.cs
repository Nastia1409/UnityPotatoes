
using System;
using UnityEngine;

namespace Globals
{
    public enum UpdateOption { Location };
    //global Singelton class which will control selected gameObjects
    //every gameObject may register for events in the class
    public class GlobalPlayersController : MonoBehaviour
    {
        static object _lockInit = new object();
        //declare event type (which functions can be registered to the event)
        public delegate void LocationUpdateHandler(Vector2 newLocation);
        //event to which every gameObject can register.
        private event LocationUpdateHandler OnUpdateLocation = null;

        #region Create Singelton instance
        public static GlobalPlayersController Instance;// = null;

        private void Awake()
        {
            Instance = this;
        }

        //prevent from other classes to create instances from this class
        private GlobalPlayersController()
        {
        }
        #endregion


        public void RegisterForLocationUpdates(LocationUpdateHandler handler)
        {
            //if OnUpdateLocation is empty or it does not contain handler then register it.
            if (!IsHandlerRegisteredFor(UpdateOption.Location, handler))
                OnUpdateLocation += handler;
        }


        public void UnRegisterFromLocationUpdates(LocationUpdateHandler handler)
        {
            //if OnUpdateLocation is not empty check if it contains handler and un register it.
            if (IsHandlerRegisteredFor(UpdateOption.Location, handler))
                OnUpdateLocation -= handler;
        }


        public bool IsHandlerRegisteredFor(UpdateOption uOp, LocationUpdateHandler handler)
        {
            Delegate checkEvent = null;
            //Select the event by UpdateOption
            switch (uOp)
            {
                case UpdateOption.Location:
                    checkEvent = OnUpdateLocation;
                    break;
                default:
                    break;
            }
            //check if the handler exists in the chosen event
            if (checkEvent != null)
            {
                foreach (Delegate existingHandler in OnUpdateLocation.GetInvocationList())
                {
                    if (existingHandler == handler)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void ClearEventHandlers(UpdateOption uOp)
        {
            Delegate eventToClear = null;
            //Select the event by UpdateOption
            switch (uOp)
            {
                case UpdateOption.Location:
                    eventToClear = OnUpdateLocation;
                    break;
                default:
                    break;
            }
            eventToClear = null;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;
                if (OnUpdateLocation != null)
                    OnUpdateLocation(target);

            }
        }
    }
}