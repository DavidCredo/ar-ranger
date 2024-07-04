using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace ARRanger
{
    /// <summary>
    /// A base class for creating persistent singletons in Unity.
    /// </summary>
    /// <typeparam name="T">The type of the singleton component.</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public bool AutoUnparentOnAwake = true;
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

            if (AutoUnparentOnAwake)
            {
                transform.SetParent(null);
            }

            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
