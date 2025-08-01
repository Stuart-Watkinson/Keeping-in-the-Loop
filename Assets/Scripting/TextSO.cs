using UnityEngine;

[CreateAssetMenu(fileName = "TextSO", menuName = "Scriptable Objects/TextSO")]
public class TextSO : ScriptableObject
{
    public string m_text;
    public bool m_isSelected;
    public int m_awardedScore;
}
