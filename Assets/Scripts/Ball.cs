using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speedValue;
    [SerializeField] private float forceValue;
    [SerializeField] private Transform padTransform;

    private bool isStarted = false;

    private void Update()
    {
        if (!isStarted)
        {
            Vector3 pos = transform.position;
            pos.x = padTransform.position.x;
            transform.position = pos;

            if(Input.GetMouseButtonDown(0))
            {
                StartBall();
            }
        }

        rb.velocity = speedValue * (rb.velocity.normalized);
    }

    private void StartBall()
    {
        isStarted = true;
        float forceX = Random.Range(forceValue / 5, forceValue / 2);//сила по х 
        int randDir = Random.Range(0, 2); // случайной число
        if(randDir == 0)
        {
            forceX *= -1f;
        }

        float forceY = Mathf.Sqrt((forceValue * forceValue) - (forceX * forceX)); // квадрат 

        Vector2 force = new Vector2(forceX, forceY) * speedValue;
        rb.AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Game.Instance.HandleBallLoss();
    }
}
