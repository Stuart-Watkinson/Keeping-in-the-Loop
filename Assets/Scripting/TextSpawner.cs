using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class controls the text spawning on randomly screen
/// </summary>
public class TextSpawner : MonoBehaviour
{
    private List<GameObject> m_currentScreenText = new List<GameObject>();
    [SerializeField] private Collider2D[] m_colliders;
    [SerializeField] private float m_radius = 11;
    [SerializeField] private GameObject m_text;
    [SerializeField] private float m_spawnTimer = 0;
    public IEnumerator m_spawnCoroutine;
    public bool m_isSpawning;
    [SerializeField] RoundController m_roundController;
    [SerializeField] private NewsPromptUI m_newsPromptUI;

    private void Start()
    {
        m_spawnCoroutine = SpawnDuration();
    }

    public IEnumerator SpawnDuration()
    {
        StopCoroutine(m_newsPromptUI.NewsPrompts());
        m_isSpawning = true;
        while (m_spawnTimer < 20)
        {
            OnSpawnText(m_text);
            m_spawnTimer++;
            yield return new WaitForSeconds(1);
        }
        m_spawnTimer = 0;
        DestroyText();
        m_isSpawning = false;
        m_roundController.m_staticScreen.enabled = false;
        StartCoroutine(m_newsPromptUI.NewsPrompts());
    }

    private void OnSpawnText(GameObject text)
    {
        bool spawnSpaceFree = false;
        int numBeforeRejection = 0;
        Vector2 position = Vector2.one;

        while (!spawnSpaceFree)
        {
            position = new Vector2(UnityEngine.Random.Range(-5.0f, 5.0f), UnityEngine.Random.Range(-5.0f, 5.0f));
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

    public void DestroyText()
    {
        for (int i = 0; i < m_currentScreenText.Count; i++)
        {
            Destroy(m_currentScreenText[i]);
        }
    }
}
