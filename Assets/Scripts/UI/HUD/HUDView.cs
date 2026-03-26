using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour, IHUDView
{
    [SerializeField]
    private RectTransform _hpValueImage;
    [SerializeField] 
    private TMP_Text _healthText;
    [SerializeField] 
    private Image _abilityCooldownFill;
    [SerializeField] 
    private TMP_Text _questsText;

    private float _hpImageMax;

    public void UpdateHealth(int current, int max)
    {
        if (_hpImageMax == 0)
        {
            _hpImageMax = _hpValueImage.sizeDelta.x;
        }
        if (_hpValueImage)
        {
            _hpValueImage.sizeDelta = new Vector2(_hpImageMax * (current / (float)max), _hpValueImage.sizeDelta.y);
        }
        if (_healthText != null)
        {
            _healthText.text = $"{current}/{max}";
        }
    }

    public void UpdateAbilityCooldown(float remaining)
    {
        if (_abilityCooldownFill != null)
            _abilityCooldownFill.fillAmount = 1 - remaining;
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    public void UpdateQuests(string text) => _questsText.text = text;
}