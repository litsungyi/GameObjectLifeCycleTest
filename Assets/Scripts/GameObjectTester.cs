using UnityEngine;

public class GameObjectTester : MonoBehaviour
{
    private void Awake()
    {
        GameObjectTestRoot.Log(name, nameof(Awake));
    }

    private void Start()
    {
        GameObjectTestRoot.Log(name, nameof(Start));
    }
	
    private void OnEnable()
    {
        GameObjectTestRoot.Log(name, nameof(OnEnable));
    }

    private void OnDisable()
    {
        GameObjectTestRoot.Log(name, nameof(OnDisable));
    }

    private void OnDestroy()
    {
        GameObjectTestRoot.Log(name, nameof(OnDestroy));
    }
}
