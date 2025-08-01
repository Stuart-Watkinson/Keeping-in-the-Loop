using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_scoreText;
    [SerializeField] private TextMeshProUGUI m_multiText;
    private float m_currentScore = 0;
    private float m_goalScore;
    private float m_scoreMultiplier = 1;
    [SerializeField] private MouseController m_mouseController;

    private void Start()
    {
        m_mouseController.m_OnScoreAwarded += OnScoreChanged;
        m_scoreText.text = m_currentScore.ToString();
        m_multiText.text = m_scoreMultiplier.ToString();
    }

    private void OnScoreChanged(float score)
    {
        m_currentScore += (score) * m_scoreMultiplier;
        if (score > 0)
        {
            m_scoreMultiplier += 0.1f;
        }
        else
        {
            m_scoreMultiplier = 1;
        }
        Debug.Log("<--Debug--> Multi:" + m_scoreMultiplier);
        m_multiText.text = Math.Round(m_scoreMultiplier, 1).ToString();
        m_scoreText.text = m_currentScore.ToString();
    }
}
