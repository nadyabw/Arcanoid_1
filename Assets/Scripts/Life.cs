using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    #region Variables

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private int offsetBetweenX = 10;

    #endregion

    #region Properties

    public int OffsetBetweenX => offsetBetweenX;
   

    public RectTransform RectTransform => rectTransform;
    

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        image.sprite = sprites[0];
    }

    #endregion

    public void SetAvailable(bool isAvalaible)
    {
        if (!isAvalaible)
        {
            image.sprite = sprites[1];
        }
    }

    public float GetImagesOffsetX(int imagesNumber)
    {
        float totalWidth = (GetWidth() + offsetBetweenX) * (imagesNumber - 1);

        return -(totalWidth / 2);
    }

    #region Private methods

    public float GetWidth()
    {
        return image.sprite.rect.width;
    }

    #endregion
}