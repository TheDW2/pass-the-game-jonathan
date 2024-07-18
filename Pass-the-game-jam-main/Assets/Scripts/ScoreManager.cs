using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TMP_Text scoreText;

    int score = 0;
    int kills = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    // Update is called once per frame
    public void AddScore(int count)
    {
        score += count;
        UpdateScore();
    }

    public void RemoveScore(int count)
    {
        if (score > 0)
        {
            score -= count;
            UpdateScore();
        }
    }

    public void AddKill()
    {
        kills++;
        UpdateScore();
    }

    public int GetScore()
    {
        return score;
    }
    public int GetKills()
    {
        return kills;
    }

    void UpdateScore()
    {
        scoreText.text = $"Fast Food (For the Mob) (The Crime One): {score.ToString()}\nBody Count (The Killing One) : {kills.ToString()}";
    }
}
