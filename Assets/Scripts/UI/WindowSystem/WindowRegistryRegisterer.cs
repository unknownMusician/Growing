using AreYouFruits.DependencyInjection;
using AreYouFruits.MonoBehaviourUtils.Unity;
using UnityEngine;

namespace Growing.UI.WindowSystem
{
    [RequireComponent(typeof(WindowRegistry))]
    public sealed class WindowRegistryRegisterer : MonoBehaviour
    {
        private WindowRegistryHolder holder;
        
        private void Awake()
        {
            Context.Container.Resolve(out holder);
        }

        private void OnEnable()
        {
            holder.Value = gameObject.GetComponentOrThrow<WindowRegistry>();
        }

        private void OnDisable()
        {
            if (holder.Value == gameObject.GetComponentOrThrow<WindowRegistry>())
            {
                holder.Value = null;
            }
        }
    }
}