using System.Collections.Generic;
using System.Linq;
using Growing.Utils;
using UnityEngine;

namespace Growing.UI.WindowSystem
{
    public sealed class WindowRegistry : MonoBehaviour
    {
        [SerializeField] private SerializedPair<WindowType, WindowComponent>[] windows;
        
        public IReadOnlyDictionary<WindowType, WindowComponent> Windows => windows.ToDictionary(w => w.Key, w => w.Value);
    }
}