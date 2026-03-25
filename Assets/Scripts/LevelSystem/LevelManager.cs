using UnityEngine;

public class LevelManager
{
    private LevelTemplateSO[] _levelTemplates;

    public string CurentSceneName 
    {
        get
        {
            if(_levelTemplates != null && _levelTemplates.Length > 0)
            {
                return _levelTemplates[0].SceneName;
            }
            return "1_Menu";
        }
    }


    public LevelManager()
    {
        _levelTemplates = Resources.LoadAll<LevelTemplateSO>("");
    }
}
