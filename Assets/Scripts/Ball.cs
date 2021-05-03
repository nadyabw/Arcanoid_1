using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedValue = 7f;
    [SerializeField] private float forceValue = 1.5f;
    [SerializeField] private Transform padTransform;
    
    private bool isStarted;

    #endregion


    #region Unity lifecycle

    private void Update()
    {
        if (!isStarted)
        {
            Vector3 padPosition = padTransform.position;
            padPosition.y = transform.position.y;

            transform.position = padPosition;


            if (Input.GetMouseButtonDown(0))
            {
                StartBall();
            }
        }
        else
        {
            rb.velocity = speedValue * (rb.velocity.normalized);
        }
    }

    #endregion


    #region Private methods

    private void StartBall()
    {
        isStarted = true;
        float forceX = Random.Range(forceValue * 0.2f, forceValue * 0.6f);
        int randDir = Random.Range(0, 2);
        if (randDir == 0)
        {
            forceX *= -1f;
        }

        float forceY = Mathf.Sqrt((forceValue * forceValue) - (forceX * forceX));

        Vector2 force = new Vector2(forceX, forceY) * speedValue;
        rb.AddForce(force);
    }

    #endregion
}