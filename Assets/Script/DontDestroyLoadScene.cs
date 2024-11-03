using UnityEngine.SceneManagement;
using UnityEngine;

public class DontDestroyLoadScene : MonoBehaviour
{
    public new GameObject[] objects;
    
    
    public static DontDestroyLoadScene instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyLoadScene !!!!");
            return;
        }

        instance = this;
        foreach (GameObject _object in objects)
        {
            DontDestroyOnLoad(_object);
        }
    }
    
    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element,SceneManager.GetActiveScene());
        }
    }

    
}
