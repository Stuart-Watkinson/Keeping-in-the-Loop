using System.Collections;
using TMPro;
using UnityEngine;

public class ResultsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_quotaText;
    [SerializeField] private TextMeshProUGUI m_revenueText;
    [SerializeField] private TextMeshProUGUI m_payText;
    [SerializeField] private GameObject m_resultsScreen;
    [SerializeField] private ScoreManager m_scoreManager;
    [SerializeField] private RoundController m_roundController;
    [SerializeField] private GameObject[] m_resultsText;

    private bool m_won;

    private Coroutine m_showResultsDelay;

    private void Awake()
    {
        m_scoreManager.m_OnRoundEnd += ShowResults;
    }

    private void Start()
    {
        m_resultsScreen.SetActive(false);
        for (int i = 0; i < m_resultsText.Length; i++)
        {
            m_resultsText[i].SetActive(false);
        }
    }

    private void ShowResults(float quota, float endRevenue)
    {
        m_resultsScreen.SetActive(true);
        m_quotaText.text = quota.ToString();
        m_revenueText.text = endRevenue.ToString();
        m_payText.text = (endRevenue - quota).ToString();

        int result = (endRevenue - quota < 0) ? WinOrLose(1) : WinOrLose(0);
    }

    private IEnumerator ResultsDelay(int result)
    {
        yield return new WaitForSeconds(1.5f);
        m_resultsText[result].SetActive(true);
    }

    private int WinOrLose(int results)
    {
        StartCoroutine(ResultsDelay(results));
        return results;
    }
}
