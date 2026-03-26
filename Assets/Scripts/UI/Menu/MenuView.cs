using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour, IMenuView
{
    [SerializeField] 
    private Button playButton;
    [SerializeField] 
    private Button exitButton;

    public event Action OnPlayClicked;
    public event Action OnExitClicked;

    private void Awake()
    {
        playButton.onClick.AddListener(() => OnPlayClicked?.Invoke());
        exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}