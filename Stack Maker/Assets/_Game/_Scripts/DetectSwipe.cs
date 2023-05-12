using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectSwipe : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private Vector2 swipeDelta;

    private bool isTouched = false;

    private const float SWIPE_THRESHHOLD = 100;

    public UnityAction swipeRight;
    public UnityAction swipeLeft;
    public UnityAction swipeUp;
    public UnityAction swipeDown;

    void Start()
    {
        isTouched = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isTouched = true;
            fingerDownPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            fingerDownPos = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;
        if (fingerDownPos != Vector2.zero)
        {

            if (Input.touches.Length != 0)
            {

                swipeDelta = Input.touches[0].position - fingerDownPos;
                DetectorSwipe();
            }

            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - fingerDownPos;
                DetectorSwipe();

            }
            else if (Input.GetMouseButton(0))
            {
                fingerDownPos = (Vector2)Input.mousePosition;
            }
            swipeDelta = Vector2.zero;
        }
    }
    void DetectorSwipe()
    {
        if (swipeDelta.sqrMagnitude > SWIPE_THRESHHOLD)
        {

            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < 0)
                {
                    this.swipeLeft();
                }
                else
                {
                    this.swipeRight();
                }
            }
            else
            {
                if (y < 0)
                {
                    this.swipeDown();
                }
                else
                {
                    this.swipeUp();
                }
            }
            if (Input.touches.Length != 0)
            {
                fingerDownPos = Input.touches[0].position;

            }
        }
    }
}
