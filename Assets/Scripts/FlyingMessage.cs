using UnityEngine;

public class FlyingMessage : MonoBehaviour
{
    [SerializeField] private TextMesh messageText;
    [SerializeField] private Color positiveColor = new Color(0.0f, 1.0f, 0.0f);

    [SerializeField] private float flyingSpeed = 1.5f;
    [SerializeField] private float lifeTime = 1.5f;
    private float timeToDissapear;

    public void Init(Vector2 pos, string text)
    {
        messageText.text = text;
        messageText.color = positiveColor;

        timeToDissapear = lifeTime;

        transform.position = pos;
    }

    void Update()
    {
        timeToDissapear -= Time.deltaTime;

        messageText.transform.Translate(0, flyingSpeed * Time.deltaTime, 0);

        Color color = messageText.color;
        // плавно меняем прозрачность текста от 1 в начале до 0,5 в конце полета (перед уничтожением)
        float alpha = (1f + timeToDissapear / lifeTime) / 2;
        color.a = alpha;
        messageText.color = color;

        if(timeToDissapear <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
