public interface ISceneDependencyReceiver
{
    void Inject(EventBus eventBus, LevelManager levelManager);
}
