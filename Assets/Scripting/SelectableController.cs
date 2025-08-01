using TMPro;
using UnityEngine;

public class SelectableController : MonoBehaviour
{
    [SerializeField] private TextSO[] m_textSOArray;
    private TextSO m_currentText;

    private void Start()
    {
        m_currentText = m_textSOArray[UnityEngine.Random.Range(0, m_textSOArray.Length)];
        this.gameObject.GetComponent<TMP_Text>().text = m_currentText.m_text;
    }

    public void OnSelected()
    {

    }
}
