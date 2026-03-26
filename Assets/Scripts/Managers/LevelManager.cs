using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    private EventBus _eventBus;
    private LevelTemplateSO[] _levelTemplates;
    private int _currentLevelIndex;

    public string CurentSceneName 
    {
        get
        {
            if(_levelTemplates != null && _levelTemplates.Length > 0)
            {
                return _levelTemplates[_currentLevelIndex].SceneName;
            }
            return "1_Menu";
        }
    }

    public LevelTemplateSO CurentLevel
    {
        get
        {
            if (_levelTemplates != null && _levelTemplates.Length > 0)
            {
                return _levelTemplates[_currentLevelIndex];
            }
            return null;
        }
    }

    public LevelManager(EventBus eventBus)
    {
        _eventBus = eventBus;

        _levelTemplates = Resources.LoadAll<LevelTemplateSO>("");

        _eventBus.Subscribe<LevelCompletedEvent>(OnLevelCompleted);
    }

    private void LoadLevel(int index)
    {
        if (index < 0 || index >= _levelTemplates.Length) return;
        _currentLevelIndex = index;
        SceneManager.LoadScene(_levelTemplates[_currentLevelIndex].SceneName);
    }

    private void OnLevelCompleted(LevelCompletedEvent evt)
    {
        int nextIndex = _currentLevelIndex + 1;
        if (nextIndex < _levelTemplates.Length)
        {
            LoadLevel(nextIndex);
        }
        else
        {
            OpenMenu();
        }
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("1_Menu");
    }

    public void OpenGame()
    {
        LoadLevel(_currentLevelIndex);
    }
}
