using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Big_Block : MonoBehaviour
{
    #region Unity lifecycle

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int NumberHit;
    [SerializeField] private int scoreForDestroy;
    [SerializeField] private Sprite[] stateSprites; //массив для картинок
    
    private int currentHits;
 


    private void Start()
    {
        UpdateView();
        
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        Hit();
    }

    private void Hit() // проверка удара
    {
        currentHits++;
        if (currentHits == NumberHit) // если удар последний
        {
            Score.Instance.AddScore(scoreForDestroy);
            Destroy(gameObject);
        }

        UpdateView();
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