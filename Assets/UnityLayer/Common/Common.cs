using UnityEngine;

/// <summary>
///     Common data model.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Common<T> : MonoBehaviour where T : Object
{
    /// <summary>
    ///     Gets this.
    /// </summary>
    /// <returns></returns>
    public static T Get()
    {
        return FindObjectOfType<T>();
    }
}