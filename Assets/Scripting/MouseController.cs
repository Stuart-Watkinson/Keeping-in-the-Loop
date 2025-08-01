using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    private GameObject m_currentSelected;
    [SerializeField] LayerMask m_selectLayer;
    private Color m_defaultColour;
    [SerializeField] private Color m_selectedColour;
    private InputAction m_selectAction;
    private TMP_Text m_currentRender;

    private void Awake()
    {
        m_selectAction = InputSystem.actions.FindAction("Click");
        m_selectAction.performed += OnSelect;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hitText = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.down, 1.0f, m_selectLayer);

        if (hitText)
        {
            HoverText(hitText.transform.gameObject);
        }
        else
        {
            if (m_currentSelected != null)
            {
                m_currentSelected.GetComponent<TMP_Text>().color = m_defaultColour;
                m_currentSelected = null;
            }
        }
    }

    private void HoverText(GameObject hoverObject)
    {
        if (!hoverObject.GetComponent<SelectableController>().m_selected)
        {
            if (m_currentSelected != null)
            {
                m_currentSelected.GetComponent<TMP_Text>().color = m_defaultColour;
            }
            m_currentSelected = hoverObject;
            m_currentRender = m_currentSelected.GetComponent<TMP_Text>();
            if (m_defaultColour != m_currentRender.color)
            {
                m_defaultColour = m_currentRender.color;
            }
            m_currentRender.color = m_selectedColour;
        }
    }

    /// <summary>
    /// On interaction displays whether the text was a correct/incorrect answer
    /// </summary>
    /// <param name="context"></param>
    private void OnSelect(InputAction.CallbackContext context)
    {
        if (m_currentSelected != null)
        {
            if (m_currentSelected.GetComponent<SelectableController>().OnSelected())
            {
                m_currentRender.color = Color.green;
            }
            else
            {
                m_currentRender.color = Color.red;
            }
            m_currentSelected = null;
        }
    }
}
