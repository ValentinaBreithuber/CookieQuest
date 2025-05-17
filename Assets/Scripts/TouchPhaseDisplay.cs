using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TouchPhaseDisplay : MonoBehaviour
{
    public Text phaseDisplayText;
    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;
    public PlayerMovement playerMovement;

    void Update()
    {
        if(Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began) {
            Vector2 touchPosition = touch.position;

            // Round x and y components to integers
            int xPos = Mathf.RoundToInt(touchPosition.x);
            int yPos = Mathf.RoundToInt(touchPosition.y);

            if(xPos>=(0) && xPos<=(200)) {
                playerMovement.LeftRight(-1);
                Debug.Log("Position LEFT");
            } else
            if(xPos>=(200) && xPos<=(1000)) {
                playerMovement.LeftRight(1);
                Debug.Log("Position RIGHT");
            } else
            if(xPos>=(2000)) {
                playerMovement.Jump();
                Debug.Log("Position JUMP");
            }

            Debug.Log("Position "+touch.position);
                
            }
        } else if (Time.time - timeTouchEnded > displayTime) {
            phaseDisplayText.text = "";
        }
    }
}
