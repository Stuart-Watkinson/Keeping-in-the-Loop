using TMPro;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private GameObject m_currentSelected;
    private TextMeshProUGUI m_textRenderer;
    //Don't use OnMouseOver as it needs to be attached to each object, raycasts would probably work
}
