using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject pauseViewPrefab;
    [SerializeField] private GameObject gameOverPrefab;
    private GameObject pauseView;
    private GameObject gameOverView;
    [SerializeField] private Life lifeImagePrefab;
    [SerializeField] private Text textScore;
    [SerializeField] private int lifesAtStart = 3;
    private static int totalScore = 0;
    
    private int blocksNumberToFinish = 0;
    private List<Life> lifes = new List<Life>();
    private static int lifesLeft = 0;
    private static bool isPaused;

    public static bool IsPaused
    {
        get => isPaused;
       
    }

    public static int TotalScore => totalScore;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        isPaused = false;
        if (lifesLeft == 0)
        {
            lifesLeft = lifesAtStart;
            totalScore = 0;
        }
        UpdateLifeImages();
        UpdeteScoreText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    private void OnEnable()
    {
        AddHandlers();
    }

    private void OnDisable()
    {
        RemoveHandlers();
    }

    #endregion

    #region Private methods

    private void AddHandlers()
    {
        Block.OnCreate += HandleBlockCreate;
        Block.OnDestroy += HandleBlockDestroy;
        Floor.onBallLoss += HandleBallLoss;
        PauseView.OnClose += HandlePauseClose;
        GameOverView.OnClose += HandleGameOverClose;
    }

    private void RemoveHandlers()
    {
        Block.OnCreate -= HandleBlockCreate;
        Block.OnDestroy -= HandleBlockDestroy;
        Floor.onBallLoss -= HandleBallLoss;
        PauseView.OnClose -= HandlePauseClose;
        GameOverView.OnClose -= HandleGameOverClose;
    }

    private void HandleGameOverClose()
    {
        SceneManager.LoadScene(0);
    }
    private void HandlePauseClose()
    {
        TogglePause();
    }
    private void HandleBallLoss(GameObject ball)
    {
        lifesLeft--;
        UpdateLifeImages();

        if (lifesLeft > 0)
        {
            Instantiate(ball);
        }
        else
        {
            isPaused = true;
            gameOverView = Instantiate(gameOverPrefab, canvasTransform);
        }
    }

    private void HandleBlockDestroy(Block block)
    {
        blocksNumberToFinish--;
        AddScore(block.ScoreForDestroy);

        if (blocksNumberToFinish == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void HandleBlockCreate(bool isUndestroyable)
    {
        if (!isUndestroyable)
        {
            blocksNumberToFinish++;
        }
    }
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseView = Instantiate(pauseViewPrefab, canvasTransform);
        }
        else
        {
            Time.timeScale = 1f;
            Destroy(pauseView);
        }
    }

    private void AddScore(int value)
    {
        totalScore += value;
        UpdeteScoreText();
    }

    private void UpdeteScoreText()
    {
        textScore.text = $"Score: {totalScore}";
    }
    private void UpdateLifeImages()
    {
        if (lifes.Count == 0)
        {
            Life l;
            Vector2 pos = new Vector2(0f, 0f);
            for (int i = 0; i < lifesAtStart; i++)
            {
                float offsetX = lifeImagePrefab.GetImagesOffsetX(lifesAtStart);

                l = Instantiate(lifeImagePrefab, canvasTransform);
                pos.x = offsetX + (l.GetWidth() + l.OffsetBetweenX) * i;
                l.RectTransform.anchoredPosition = pos;
                if (i >= lifesLeft)
                {
                    l.SetAvailable(false);
                }
                lifes.Add(l);
            }
        }
        else
        {
            for (int i = 0; i < lifes.Count; i++)
            {
                if (i >= lifesLeft)
                {
                    Life li = lifes[i];
                    li.SetAvailable(false);
                }
            }
        }
    }
   
    

    #endregion
}