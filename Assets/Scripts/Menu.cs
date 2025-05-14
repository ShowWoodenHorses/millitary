using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI moneyUI;
    [SerializeField] private Animator anim;

    private bool isOpenMenu = true;

    private void OnGUI()
    {
        moneyUI.text = MoneyManager.instance.money.ToString();
    }

    public void ToggleMenu()
    {
        isOpenMenu = !isOpenMenu;
        anim.SetBool("isOpenMenu", isOpenMenu);
    }

    public void SetSelected()
    {

    }
}
