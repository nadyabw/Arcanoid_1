using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private static Game instance;
    public static Game Instance { get => instance; }

    [SerializeField] private Text scoreText;
    private int scoreValue = 0;

    [SerializeField] private FlyingMessage scoreMessagePrefab;

    private void Start()
    {
        instance = this;

        UpdateScoreText();
    }

    public void HandleBallLoss()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleBlockDestroy(Block block)
    {
        scoreValue += block.ScoreForDestroy;
        UpdateScoreText();

        //////////////////  летящий текст очков
        FlyingMessage fm = Instantiate(scoreMessagePrefab);
        fm.Init(block.transform.position, $"+{block.ScoreForDestroy}");
        //////////////////

        // удаляем блок с небольшой задержкой в 0,1 сек
        Destroy(block.gameObject, 0.1f);
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {scoreValue}";
    }
}
