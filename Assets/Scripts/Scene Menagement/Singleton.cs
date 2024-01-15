using UnityEngine;

/// <summary>
/// Acts similar to a singleton but when creating a new instance, it overwrites the previous one.
/// </summary>
/// <typeparam name="T">The type of the MonoBehaviour instance.</typeparam>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Gets the singleton instance of type T.
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        // Set the static instance to the current instance.
        Instance = this as T;
    }

    /// <summary>
    /// Called when the application is quitting.
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        // Set the static instance to null and destroy the game object.
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// This class transforms a static instance into a basic singleton.
/// </summary>
/// <typeparam name="T">The type of the MonoBehaviour instance.</typeparam>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        // If an instance already exists, do not create a new one.
        if (Instance != null)
        {
            return;
        }
        // Call the Awake method from the base class to set the static instance.
        base.Awake();
    }
}