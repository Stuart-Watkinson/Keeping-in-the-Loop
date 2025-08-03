using System.Collections;
using TMPro;
using UnityEngine;

public class NewsPromptUI : MonoBehaviour
{
    [SerializeField] private string[] m_newsPrompt;
    private int m_promptIndex;

    private void Start()
    {
        StartCoroutine(NewsPrompts());
    }
    public IEnumerator NewsPrompts()
    {
        this.gameObject.GetComponent<TMP_Text>().text = m_newsPrompt[m_promptIndex];
        if (m_promptIndex == 0)
        {
            yield return new WaitForSeconds(3);
        }
        else
        {
            yield return new WaitForSeconds(15);
        }
        m_promptIndex++;
        StartCoroutine(NewsPrompts());
    }
}
