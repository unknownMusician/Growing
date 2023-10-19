using System;
using System.Linq;
using System.Reflection;
using AreYouFruits.DependencyInjection;
using AreYouFruits.DependencyInjection.TypeResolvers;
using AreYouFruits.Nullability;
using UnityEngine;

namespace Growing.Utils
{
    [DefaultExecutionOrder(-1010)]
    [DisallowMultipleComponent]
    public sealed class ComponentDiInitializer : MonoBehaviour
    {
        [SerializeField] private string[] initializerMethodNames = { "Inject" };

        private void Awake()
        {
            InjectGameObject();
        }

        private void InjectGameObject()
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (GetInitializerMethodInfo(component).TryGet(out var methodInfo))
                {
                    Invoke(methodInfo, component);
                }
            }
        }

        private Optional<MethodInfo> GetInitializerMethodInfo(Component component)
        {
            var type = component.GetType();

            Optional<MethodInfo> result = default;

            foreach (var initializerMethodName in initializerMethodNames)
            {
                var methodInfo =
                    type.GetMethod(initializerMethodName, BindingFlags.Instance | BindingFlags.Public);

                if (methodInfo is not null && !methodInfo.IsGenericMethod)
                {
                    result.Set(
                        () => methodInfo, 
                        _ => throw new ArgumentOutOfRangeException(
                            $"{type.FullName} contains more than one initializer of supported names."));
                }
            }

            return result;
        }

        private static void Invoke(MethodInfo methodInfo, object methodOwner)
        {
            var parameterInfos = methodInfo.GetParameters();

            var parameters = ResolveParameters(parameterInfos);

            methodInfo.Invoke(methodOwner, parameters);
        }

        private static object[] ResolveParameters(ParameterInfo[] parameterInfos)
        {
            return parameterInfos
                .Select(p => Context.Container.Resolve(p.ParameterType))
                .ToArray();
        }
    }
}