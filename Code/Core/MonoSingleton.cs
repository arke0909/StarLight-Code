using UnityEngine;

public class Monosingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = FindAnyObjectByType<T>();
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
}