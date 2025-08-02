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
    [SerializeField] private Collider2D[] m_colliders;
    private float m_radius;

    private void OnSpawnText(GameObject text)
    {
        bool spawnSpaceFree = false;
        int numBeforeRejection = 0;
        Vector2 position = Vector2.one;

        while (!spawnSpaceFree)
        {
            position = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            spawnSpaceFree = PreventSpawnOverlap(position);

            if (numBeforeRejection >= 50)
            {
                Debug.Log("<--Debug--> Broke due to safety net");
                break;
            }

            numBeforeRejection++;
        }

        m_currentScreenText.Add(Instantiate(text, position, Quaternion.identity));
    }

    private bool PreventSpawnOverlap(Vector2 spawnPos)
    {
        m_colliders = Physics2D.OverlapCircleAll(transform.position, m_radius);

        for (int i = 0; i < m_colliders.Length; i++)
        {
            Vector3 centerPoint = m_colliders[i].bounds.center;
            float width = m_colliders[i].bounds.extents.x;
            float height = m_colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
            {
                if (spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                {
                    return false;
                }
            }
        }
        return true;
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
            OnSpawnText(m_text);
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
