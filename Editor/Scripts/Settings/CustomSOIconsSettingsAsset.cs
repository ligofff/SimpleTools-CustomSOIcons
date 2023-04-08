using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ligofff.CustomSOIcons.Editor
{
    public class CustomSOIconsSettingsAsset : ScriptableObject
    {
        public CustomSOIconsSettings baseSettings;

        [SerializeField]
        private List<TypeIconOverrides> overrides;

        public CustomSOIconsSettings GetSettings(Object asset)
        {
            if (overrides == null) return null;
            if (overrides.FirstOrDefault(over => over.typeName == asset.GetType().Name) is { } iconOverride)
            {
                return iconOverride.settings;
            }
            else
            {
                return baseSettings;
            }
        }

        private void OnValidate()
        {
            EditorCustomSOIcons.Refresh();
        }
    }
}