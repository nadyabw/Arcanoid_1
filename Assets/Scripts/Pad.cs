using UnityEngine;

public class Pad : MonoBehaviour
{
    private float horizontalLimit;

    private void Start()
    {
        // размер (Х на У) экрана в юнитах(клетках)
        Vector2 screenSizeWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // ширина ракетки в юнитах(клетках)
        float padWidthWorld = GetComponent<BoxCollider2D>().bounds.size.x;

        horizontalLimit = screenSizeWorld.x - padWidthWorld / 2;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 pixelsPos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pixelsPos);

        Vector3 padPos = new Vector3(worldPos.x, transform.position.y, 0);

        if (padPos.x < -horizontalLimit)
        {
            padPos.x = -horizontalLimit;
        }
        else if (padPos.x > horizontalLimit)
        {
            padPos.x = horizontalLimit;
        }

        transform.position = padPos;
    }
}
