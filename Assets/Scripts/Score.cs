using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    [SerializeField] private Text textScore;
    [SerializeField] private static Score instance;
    private int score = 0;

    public static Score Instance
    {
        get => instance;
    }

    private void Start()
    {
        instance = this;
        UpdeteScore();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdeteScore();
    }
    public void UpdeteScore()
    {
        
        textScore.text = $"Score: {score}";
    }
}