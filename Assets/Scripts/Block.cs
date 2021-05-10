using System;
using UnityEngine;


public class Block : MonoBehaviour
{
    #region Variables
    // [SerializeField] private Life lifeImagePrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int NumberHit;
    [SerializeField] private int scoreForDestroy;
    [SerializeField] private Sprite[] stateSprites; //массив для картинок
    [SerializeField] private bool isUnderstroyable = false; // видимый
    [SerializeField] private bool isInvisible = false; // не видимый

    private Collider2D blockCollider;

    private int currentHits;

    #endregion

    #region Properties

    public int ScoreForDestroy => scoreForDestroy;

    #endregion

    #region Events

    public static event Action<Block> OnDestroy;
    public static event Action<bool> OnCreate;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        UpdateView();
        if(isInvisible)
        {
            blockCollider = GetComponent<Collider2D>();
            SetAccessible(false); 
        }
        OnCreate?.Invoke(isUnderstroyable); // ??
    }

    #endregion

    #region Private methods

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!isUnderstroyable) 
        {
            Hit();
        }
    }
    private void SetAccessible(bool isAccessible) // сделать видимым
    {
        if (isAccessible) // если невидимый то сделать видимым
        {
            blockCollider.isTrigger = false;

            Color clr = spriteRenderer.color;
            clr.a = 1.0f; 
            spriteRenderer.color = clr;
        }
        else // иначе стновится тригером
        {
            blockCollider.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SetAccessible(true);
    }

    private void Hit() // проверка удара
    {
        currentHits++;
        if (currentHits == NumberHit) // если удар последний
        {
            OnDestroy?.Invoke(this);
            Destroy(gameObject);
        }
        else
        {
            UpdateView();
        }
    }

    private void UpdateView() // обновление картинки
    {
        if (currentHits < stateSprites.Length)
        {
            spriteRenderer.sprite = stateSprites[currentHits];
        }
    }

    #endregion
}