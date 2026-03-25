public class MenuModel : IMenuModel
{
    public string SceneNameForPlay { get; set; }

    public MenuModel(string sceneName)
    {
        SceneNameForPlay = sceneName;
    }
}