using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    [SerializeField] private SpriteRenderer spriteRenderer;
    private float horizontalLimit;

    #endregion
    
    #region Unity lifecycle

    private void Start()
    {
        Vector2 screenSizeWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float padWidthWorld = spriteRenderer.bounds.size.x;

        horizontalLimit = screenSizeWorld.x - padWidthWorld / 2;
    }

    private void Update()
    {
        if (Game.IsPaused)
        {
            return;
        }
        Vector3 positionInPixels = Input.mousePosition;
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);

        Vector3 padPosition = positionInWorld;
        padPosition.y = transform.position.y;

        padPosition.x = Mathf.Clamp(padPosition.x, -horizontalLimit, horizontalLimit);

        transform.position = padPosition;
    }

    #endregion
}