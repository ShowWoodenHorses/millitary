using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseOver = false;

    private void OnDestroy()
    {
        mouseOver = false;
        UIManager.main.SetHoveringState(false);
    }

    private void OnDisable()
    {
        mouseOver = false;
        UIManager.main.SetHoveringState(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        UIManager.main.SetHoveringState(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        UIManager.main.SetHoveringState(false);
        gameObject.SetActive(false);
    }

    public bool IsMouseOver()
    {
        return mouseOver;
    }
}
