using UnityEngine;

public class DontDestroyARSession : MonoBehaviour
{
    private static DontDestroyARSession instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
