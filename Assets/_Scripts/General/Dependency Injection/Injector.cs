using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using UnityEngine;

namespace ARRanger.DependencyInjection
{
    /// <summary>
    /// Attribute used to mark fields or methods that should be injected by a dependency injection container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public sealed class InjectAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// Attribute used to mark a method as a provider for dependency injection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ProvideAttribute : PropertyAttribute
    {
    }

    /// <summary>
    /// The Injector class is responsible for dependency injection in the application.
    /// It provides methods to register providers, inject dependencies into objects, validate dependencies, and clear dependencies.
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public class Injector : Singleton<Injector>
    {
        const BindingFlags k_BindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        readonly Dictionary<Type, object> registry = new Dictionary<Type, object>();

        protected override void Awake()
        {
            base.Awake();

            var providers = FindMonoBehaviours().OfType<IDependencyProvider>();
            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }

            var injectables = FindMonoBehaviours().Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
        }

        private void Inject(object instance)
        {
            var type = instance.GetType();

            var injectableFields = type.GetFields(k_BindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                if (injectableField.GetValue(instance) != null)
                {
                    Debug.LogWarning($"[Injector] Field {type.Name}.{injectableField.Name} is already set, skipping injection");
                    continue;
                }
                var fieldType = injectableField.FieldType;
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null)
                {
                    throw new InvalidOperationException($"Failed to inject {fieldType.Name} into {type.Name}");
                }
                injectableField.SetValue(instance, resolvedInstance);
            }

            var injectableMethods = type.GetMethods(k_BindingFlags)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableMethod in injectableMethods)
            {
                var requiredParameters = injectableMethod.GetParameters().Select(parameter => parameter.ParameterType).ToArray();
                var resolvedInstances = requiredParameters.Select(Resolve).ToArray();
                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
                {
                    throw new InvalidOperationException($"Failed to inject {type.Name}.{injectableMethod.Name}");
                }
                injectableMethod.Invoke(instance, resolvedInstances);
            }
        }

        public void ValidateDependencies()
        {
            var monoBehaviours = FindMonoBehaviours();
            var providers = monoBehaviours.OfType<IDependencyProvider>();
            var providedDependencies = GetProvidedDependencies(providers);

            var invalidDependencies = monoBehaviours
                .SelectMany(mb => mb.GetType().GetFields(k_BindingFlags), (mb, field) => new { mb, field })
                .Where(t => Attribute.IsDefined(t.field, typeof(InjectAttribute)))
                .Where(t => !providedDependencies.Contains(t.field.FieldType) && t.field.GetValue(t.mb) == null)
                .Select(t => $"[Validation] {t.mb.GetType().Name} is missing dependency {t.field.FieldType.Name}");

            var invalidDependencyList = invalidDependencies.ToList();

            if (!invalidDependencyList.Any())
            {
                Debug.Log("[Validation] All dependencies are valid");
            }
            else
            {
                Debug.LogError($"[Validation] {invalidDependencyList.Count} dependencies are invalid");
                foreach (var invalidDependency in invalidDependencyList)
                {
                    Debug.LogError(invalidDependency);
                }
            }
        }

        /// <summary>
        /// Retrieves the set of provided dependencies from a collection of dependency providers.
        /// </summary>
        /// <param name="providers">The collection of dependency providers.</param>
        /// <returns>A HashSet containing the types of the provided dependencies.</returns>
        HashSet<Type> GetProvidedDependencies(IEnumerable<IDependencyProvider> providers)
        {
            var providedDependencies = new HashSet<Type>();
            foreach (var provider in providers)
            {
                var methods = provider.GetType().GetMethods(k_BindingFlags);
                foreach (var method in methods)
                {
                    if (!Attribute.IsDefined(method, typeof(ProvideAttribute)))
                        continue;
                    var returnType = method.ReturnType;
                    providedDependencies.Add(returnType);
                }
            }

            return providedDependencies;
        }

        /// <summary>
        /// Clears all dependencies in the MonoBehaviour objects that have the [Inject] attribute.
        /// </summary>
        public void ClearDependencies()
        {
            foreach (var monoBehaviour in FindMonoBehaviours())
            {
                var type = monoBehaviour.GetType();
                var injectableFields = type.GetFields(k_BindingFlags)
                    .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

                foreach (var injectableField in injectableFields)
                {
                    injectableField.SetValue(monoBehaviour, null);
                }
            }

            Debug.Log("[Injector] Cleared all dependencies");
        }

        object Resolve(Type type)
        {
            registry.TryGetValue(type, out var resolvedInstance);
            return resolvedInstance;
        }

        static bool IsInjectable(MonoBehaviour obj)
        {
            var members = obj.GetType().GetMembers(k_BindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }

        private void RegisterProvider(IDependencyProvider provider)
        {
            var methods = provider.GetType().GetMethods(k_BindingFlags);

            foreach (var method in methods)
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute)))
                    continue;
                var returnType = method.ReturnType;
                var providedInstance = method.Invoke(provider, null);
                if (providedInstance != null)
                {
                    registry.Add(returnType, providedInstance);
                }
                else
                {
                    throw new InvalidOperationException($"Provider {provider.GetType().Name} returned null for type {returnType.Name}");
                }
            }
        }

        static MonoBehaviour[] FindMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
        }

    }
}
