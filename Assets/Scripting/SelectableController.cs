using TMPro;
using UnityEngine;

public class SelectableController : MonoBehaviour
{
    [SerializeField] private TextSO[] m_textSOArray;
    public TextSO m_currentText;
    [HideInInspector] public bool m_selected = false;

    private void Start()
    {
        m_currentText = m_textSOArray[UnityEngine.Random.Range(0, m_textSOArray.Length)];
        this.gameObject.GetComponent<TMP_Text>().text = m_currentText.m_text[Random.Range(0, m_currentText.m_text.Length)];
    }

    public bool OnSelected()
    {
        m_selected = true;
        if (m_currentText.m_awardedScore > 0)
        {
            Destroy(this.gameObject, 1.0f);
            return true;
        }
        else
        {
            Destroy(this.gameObject, 1.0f);
            return false;
        }

    }
}
