using UnityEngine;


public class Ball : MonoBehaviour
{
    #region Variables

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float gameSpeed = 7f;
    [SerializeField] private float startForceValue = 350f;
    [SerializeField] private float startOffsetY = -3.1f;
    
    [SerializeField] private Transform padTransform;
    [SerializeField] private TrailRenderer trailRenderer;

    [SerializeField] private Vector2 minRandomDirection = new Vector2(-1f, 1f);
    [SerializeField] private Vector2 maxRandomDirection = new Vector2(0.85f, 1f);

    private bool isStarted;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        trailRenderer.enabled = false;
    }

    private void Update()
    {
        if (Game.IsPaused)
        {
            return;
        }
        if (!isStarted)
        {
            Vector3 padPosition = padTransform.position;
            padPosition.y = startOffsetY;

            transform.position = padPosition;


            if (Input.GetMouseButtonDown(0))
            {
                StartBall();
            }
        }
    }

    #endregion

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(isStarted)
        {
            rb.velocity = gameSpeed * (rb.velocity.normalized);
        }
    }

    #region Private methods

    private void StartBall()
    {
        isStarted = true;
        trailRenderer.enabled = true;

        float dirX = Random.Range(minRandomDirection.x, maxRandomDirection.x);
        float dirY = Random.Range(minRandomDirection.y, maxRandomDirection.y);

        Vector2 force = (new Vector2(dirX, dirY).normalized) * startForceValue;
        rb.AddForce(force);
    }

    #endregion
}