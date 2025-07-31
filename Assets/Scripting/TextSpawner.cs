using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class controls the text spawning on randomly screen
/// </summary>
public class TextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_text;
    private Coroutine m_spawnTimer;
    private List<GameObject> m_currentScreenText = new List<GameObject>();
    private int m_timerTest = 0;

    private void SpawnText(GameObject text)
    {
        Vector2 position = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
        m_currentScreenText.Add(Instantiate(text, position, Quaternion.identity));
    }

    private void DestroyText()
    {
        for (int i = 0; i < m_currentScreenText.Count; i++)
        {
            Destroy(m_currentScreenText[i]);
        }
    }

    private IEnumerator SpawnTimer()
    {
        if (m_timerTest < 10)
        {
            SpawnText(m_text);
            yield return new WaitForSeconds(1);
            m_timerTest++;
        }
        else
        {
            DestroyText();
            m_timerTest = 0;
            m_currentScreenText.Clear();
        }
        StartCoroutine(SpawnTimer()); //Loops the coroutine
    }

    private void Start()
    {
        StartCoroutine(SpawnTimer());
    }
}
