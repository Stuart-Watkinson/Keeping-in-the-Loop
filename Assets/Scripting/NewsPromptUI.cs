using System.Collections;
using TMPro;
using UnityEngine;

public class NewsPromptUI : MonoBehaviour
{
    [SerializeField] private string[] m_newsPrompt;
    private int m_promptIndex;

    private IEnumerator NewsPrompts()
    {
        this.gameObject.GetComponent<TMP_Text>().text = m_newsPrompt[m_promptIndex];
        yield return new WaitForSeconds(15);
        m_promptIndex++;
        StartCoroutine(NewsPrompts());
    }
}
