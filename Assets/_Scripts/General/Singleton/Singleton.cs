using System;
using UnityEngine;

namespace ARRanger
{
    /// <summary>
    /// A generic singleton class that ensures only one instance of a MonoBehaviour-derived class exists in the scene.
    /// </summary>
    /// <typeparam name="T">The type of the singleton instance.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        protected static T instance;
        public static bool HasInstance => instance != null;
        public static T TryGetInstance() => HasInstance ? instance : null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name + "Auto-Generated");
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        protected virtual void Awake()
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying) return;

            instance = this as T;
        }
    }
}
