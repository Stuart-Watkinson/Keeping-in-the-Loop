using System;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    [SerializeField] private float m_roundLength;
    [SerializeField] private float m_roundTimer = 0;

    [SerializeField] private TextSpawner m_textSpawner;
    public bool m_timerRunning = true;

    [SerializeField] private float m_staticChance;
    private float m_currentChanceDefault;
    private int m_checkForStatic = 100;
    private int m_currentRound;
    private float m_roundChanceIncrease;
    private float m_timeBetweenChecks;

    public event Action<bool> m_endOfRound;

    private void Start()
    {
        m_currentChanceDefault = m_staticChance;
        m_roundChanceIncrease = (float)(0.1 * m_currentRound + 1);
        m_staticChance = 10 + m_roundChanceIncrease;
    }
    private void OnStatic()
    {
        while (!m_timerRunning)
        {
            if (!m_textSpawner.m_isSpawning)
            {
                m_timerRunning = false;
                m_textSpawner.StartCoroutine(m_textSpawner.m_spawnCoroutine);
            }
            else
            {
                m_textSpawner.m_spawnCoroutine = m_textSpawner.SpawnDuration();
                m_timerRunning = true;
            }
        }
    }

    private void OnRoundEnd(float quota, float endRevenue)
    {
        m_endOfRound?.Invoke(true);
    }

    private void FixedUpdate()
    {
        if (m_timerRunning && !m_textSpawner.m_isSpawning)
        {
            m_roundTimer += Time.deltaTime;
            if (m_timeBetweenChecks >= 3)
            {
                m_checkForStatic = UnityEngine.Random.Range(0, 101);
                m_timeBetweenChecks = 0;
            }
            m_timeBetweenChecks += Time.deltaTime;
            m_staticChance += m_roundChanceIncrease * Time.deltaTime;
            if (m_checkForStatic <= m_staticChance)
            {
                m_timerRunning = false;
                m_staticChance = m_currentChanceDefault;
                OnStatic();
            }
            if (m_roundTimer >= m_roundLength)
            {
                m_timerRunning = false;
                OnRoundEnd(0, 0);
            }
        }
    }
}
