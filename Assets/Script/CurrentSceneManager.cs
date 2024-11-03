
using UnityEngine;

public class CurrentSceneManager : MonoBehaviour
{
    public static CurrentSceneManager instance;

    private void Awake()
    {
        if (instance!=null)
        {
            Debug.LogWarning("Il y a plus d'une instance de CurrentSceneManager !!!!");
            return;
        }

        instance = this;
    }
    public bool isPlayerPresentByDefault = false;
}
