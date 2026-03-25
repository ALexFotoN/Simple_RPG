using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    //[SerializeField]
    //private BuildManager _buildManager;
    //public BuildManager BuildManager => _buildManager;

    private void Awake()
    {
        ServiceLocator.Initialize();
        //ServiceLocator.CurrentSericeLocator.RegisterService<BuildManager>(_buildManager);
    }
}