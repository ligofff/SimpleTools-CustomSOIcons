using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Ligofff.CustomSOIcons.Editor
{
    public static class CustomAssetIconsSettingsMenu
    {
        [MenuItem("Ligofff/Open custom SO icons settings")]
        private static void OpenSettings()
        {
            var settings = GetSettings();
            Selection.SetActiveObjectWithContext(settings, settings);
        }
        
        [MenuItem("Ligofff/Refresh custom SO icons")]
        private static void RefreshIcons()
        {
            EditorCustomSOIcons.Refresh();
        }

        public static CustomSOIconsSettingsAsset GetSettings()
        {
            var allSettings = AssetDatabase.FindAssets($"t:{nameof(CustomSOIconsSettingsAsset)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CustomSOIconsSettingsAsset>)
                .ToList();
            
            if (allSettings.Count == 0) return CreateSettings();
            
            return allSettings[0];
        }
        
        private static CustomSOIconsSettingsAsset CreateSettings()
        {
            var newSettings = ScriptableObject.CreateInstance<CustomSOIconsSettingsAsset>();
            
            var filename = "CustomSOIconsSettings.asset";
            var path = "Assets/Resources/";

            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            AssetDatabase.CreateAsset(newSettings, path + filename);

            return newSettings;
        }
    }
}