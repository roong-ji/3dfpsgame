using UnityEngine;
using TMPro;

public class UI_Gold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _goldUIText;

    private void Start()
    {
        GoldManager.Instance.Bind(UpdateGoldUIText);
    }

    private void OnDestroy()
    {
        if (GoldManager.Instance == null) return;
        GoldManager.Instance.UnBind(UpdateGoldUIText);
    }

    private void UpdateGoldUIText(int gold)
    {
        _goldUIText.text = gold.ToString();
    }
}
