using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatforms : MonoBehaviour
{
    public float movementSpeed = 0.5f, distanceToTravel, waitTime = 2.5f;
    private float countdown;
    Vector3 startingPosition, destinationPosition, travelingPosition;
    bool changeDirection, waitInPosition;
    private enum MovementType { None, UpDown, Sideways, BackForward }
    [SerializeField] MovementType movementType = new MovementType();

    // Start is called before the first frame update
    void Start()
    {
        //movementSpeed = 0.5f;
        startingPosition = this.transform.localPosition;
        destinationPosition = this.transform.localPosition;
        travelingPosition = destinationPosition;
        changeDirection = true;
        //waitTime = 2.5f;
        waitInPosition = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(changeDirection)
            ChangePlatformDirection();
      
        if (movementSpeed != 0)
            MovePlatform();
    }

    private void MovePlatform()
    {
        //if the distance is reached then change direction
        if (Vector3.Distance(this.transform.localPosition, travelingPosition) <= 0)
        {
            //reverse the position so it goes back
            travelingPosition = (travelingPosition == startingPosition) ? destinationPosition : startingPosition;
            waitInPosition = true;
        }

        if (!waitInPosition)
        {
            //move platform on the direction 
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, travelingPosition, movementSpeed * Time.deltaTime);
        }
        else
        {
            //stop platform in position until timer ends
            countdown += Time.deltaTime;
            if (countdown >= waitTime)
            {
                waitInPosition = false;
                countdown = 0;
            }
        }
    }

    private bool CanChangeDirection()
    {
        /*TODO
         * is platform on starting position for the next direction?
         */
        if (this.transform.localPosition == startingPosition)
            return true;

        return false;
    }

    private void ChangePlatformDirection()
    {
        if(!CanChangeDirection()) { return; }

        ChangePositons();

        //check which direction change and add the ammount to travel to the original position.
        switch (movementType)
        {
            case MovementType.Sideways:
                destinationPosition.x += distanceToTravel;
                break;

            case MovementType.UpDown:
                destinationPosition.y += distanceToTravel;
                break;

            case MovementType.BackForward:
                destinationPosition.z += distanceToTravel;
                break;

            default:
                //MovePlatform(Vector3.zero);
                break;
        }
        changeDirection = false;
    }

    private void ChangePositons()
    {
        startingPosition = this.transform.localPosition;
        destinationPosition = startingPosition;
        travelingPosition = destinationPosition;
    }
}
