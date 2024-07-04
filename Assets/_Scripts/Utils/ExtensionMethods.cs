using UnityEngine;

// Useful extension methods for builtin Unity classes
public static class ExtensionMethods
{

    #region GameObject Extensions
    /// <summary>
    /// Gets or adds a component of type T to the specified GameObject.
    /// If the component already exists, it is returned. Otherwise, a new component is added and returned.
    /// </summary>
    /// <typeparam name="T">The type of component to get or add.</typeparam>
    /// <param name="go">The GameObject to get or add the component to.</param>
    /// <param name="component">The reference to store the component.</param>
    public static void GetOrAddComponent<T>(this GameObject go, out T component) where T : Component
    {
        component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
    }

    #endregion

    #region Vector3 Extensions
    /// <summary>
    /// Modifies the components of a Vector3 object with optional new values.
    /// </summary>
    /// <param name="vector">The Vector3 object to modify.</param>
    /// <param name="x">The new value for the x component. If null, the original value is preserved.</param>
    /// <param name="y">The new value for the y component. If null, the original value is preserved.</param>
    /// <param name="z">The new value for the z component. If null, the original value is preserved.</param>
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        vector.x = x ?? vector.x;
        vector.y = y ?? vector.y;
        vector.z = z ?? vector.z;
        return vector;
    }

    /// <summary>
    /// Calculates the distance between two Vector3 points.
    /// </summary>
    /// <param name="vector">The starting point.</param>
    /// <param name="other">The ending point.</param>
    /// <returns>The distance between the two points.</returns>
    public static float DistanceTo(this Vector3 vector, Vector3 other) => Vector3.Distance(vector, other);

    #endregion

    #region Transform Extensions
    /// <summary>
    /// Retrieves the GameObject associated with the specified Transform.
    /// </summary>
    /// <param name="transform">The Transform to retrieve the GameObject from.</param>
    /// <param name="gameObject">The GameObject associated with the Transform.</param>
    public static void GetGameObject(this Transform transform, out GameObject gameObject) => gameObject = transform.gameObject;

    /// <summary>
    /// Resets the transformation of a Transform component.
    /// </summary>
    /// <param name="transform">The Transform component to reset.</param>
    public static void ResetTransformation(this Transform transform)
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        transform.localScale = Vector3.one;
    }
    #endregion
}
