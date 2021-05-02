using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] stateSprites;

    [SerializeField] private int scoreForDestroy = 5;
    [SerializeField] private int hitsToDestroy = 1;
    private int currentHits = 0;

    public int ScoreForDestroy {get => scoreForDestroy;}

    private void Start()
    {
        UpdateView();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit();   
    }

    private void HandleHit()
    {
        currentHits++;
        if(currentHits == hitsToDestroy)
        {
            Game.Instance.HandleBlockDestroy(this);
            return;
        }

        UpdateView();
    }

    private void UpdateView()
    {
        if(currentHits < stateSprites.Length)
        {
            spriteRenderer.sprite = stateSprites[currentHits];
        }
    }
}
