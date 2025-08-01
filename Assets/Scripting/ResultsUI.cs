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
    [SerializeField] private GameObject m_lossText;

    private Coroutine m_showResultsDelay;

    private void Awake()
    {
        m_scoreManager.m_OnRoundEnd += ShowResults;
    }

    private void Start()
    {
        m_resultsScreen.SetActive(false);
        m_lossText.SetActive(false);
        //m_showResultsDelay = ResultsDelay();
    }

    private void ShowResults(float quota, float endRevenue)
    {
        m_resultsScreen.SetActive(true);
        m_quotaText.text = quota.ToString();
        m_revenueText.text = endRevenue.ToString();
        m_payText.text = (endRevenue - quota).ToString();

        if ((endRevenue - quota) < 0)
        {
            OnLoss();
        }
    }

    private IEnumerator ResultsDelay()
    {
        yield return new WaitForSeconds(1.5f);
        m_lossText.SetActive(true);
    }

    private void OnLoss()
    {
        StartCoroutine(ResultsDelay());

    }
}
