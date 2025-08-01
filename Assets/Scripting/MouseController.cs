using TMPro;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    private GameObject m_currentSelected;
    private Color m_defaultColour;
    [SerializeField] private Color m_selectedColour;
    //private GameObject m_mouse;
    [SerializeField] LayerMask m_selectLayer;

    //private void Start()
    //{
    //    //m_mouse = this.gameObject;
    //}

    private void FixedUpdate()
    {
        //m_mouse.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hitText = Physics2D.Raycast((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.down, 1.0f, m_selectLayer);

        Debug.DrawRay((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.down, m_selectedColour, 0.1f);

        if (hitText)
        {
            if (m_currentSelected != null)
            {
                m_currentSelected.GetComponent<TMP_Text>().color = m_defaultColour;
            }
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
        m_currentSelected = hoverObject;
        if (m_defaultColour != m_currentSelected.GetComponent<TMP_Text>().color)
        {
            m_defaultColour = m_currentSelected.GetComponent<TMP_Text>().color;
        }
        m_currentSelected.GetComponent<TMP_Text>().color = m_selectedColour;
    }
}
