using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables
    private float horizontalLimit;

    #endregion
    
    #region Unity lifecycle

    private void Start()
    {
        Vector2 screenSizeWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float padWidthWorld = GetComponent<BoxCollider2D>().bounds.size.x;

        horizontalLimit = screenSizeWorld.x - padWidthWorld / 2;
    }

    private void Update()
    {
        Vector3 positionInPixels = Input.mousePosition;
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);

        Vector3 padPosition = positionInWorld;
        padPosition.y = transform.position.y;

        if (padPosition.x < -horizontalLimit)
        {
            padPosition.x = -horizontalLimit;
        }
        else if (padPosition.x > horizontalLimit)
        {
            padPosition.x = horizontalLimit;
        }

        transform.position = padPosition;
    }

    #endregion
}