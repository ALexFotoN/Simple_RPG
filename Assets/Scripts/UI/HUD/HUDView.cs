using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour, IHUDView
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Image _abilityCooldownFill;
    [SerializeField] private Button _menuButton;

    public event Action OnMenuButtonClicked;

    private void Awake()
    {
        if (_menuButton != null)
            _menuButton.onClick.AddListener(() => OnMenuButtonClicked?.Invoke());
    }

    public void UpdateHealth(int current, int max)
    {
        if (_healthSlider != null)
            _healthSlider.value = (float)current / max;
        if (_healthText != null)
            _healthText.text = $"{current}/{max}";
    }

    public void UpdateAbilityCooldown(float remaining)
    {
        if (_abilityCooldownFill != null)
            _abilityCooldownFill.fillAmount = remaining;
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}