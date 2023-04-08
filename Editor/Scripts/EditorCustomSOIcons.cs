using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;
using UnityEditor.Callbacks;
using Object = UnityEngine.Object;

namespace Ligofff.CustomSOIcons.Editor
{ 
    public class EditorCustomSOIcons
    {
        private static Dictionary<Type, ICustomEditorIconDeclarator> _iconDeclarators;
        
        private static Dictionary<string, Tuple<Func<Sprite>, CustomSOIconsSettings>> _iconGettersCache;

        private static CustomSOIconsSettingsAsset _settings;
        
        [DidReloadScripts]
        private static void EditorCustomSOIconsStart()
        {
            RefreshCaches();
            EditorApplication.projectWindowItemOnGUI += ItemOnGUI;
        }
        
        public static void Refresh()
        {
            RefreshCaches();
            _settings = CustomAssetIconsSettingsMenu.GetSettings();
        }

        private static void RefreshCaches()
        {
            _iconDeclarators = new Dictionary<Type, ICustomEditorIconDeclarator>();
            _iconGettersCache = new Dictionary<string, Tuple<Func<Sprite>, CustomSOIconsSettings>>();
            CollectDeclarators();
        }
        
        private static void CollectDeclarators()
        {
            var declarators = 
                TypeCache.GetTypesDerivedFrom<ICustomEditorIconDeclarator>()
                    .Select(Activator.CreateInstance)
                    .OfType<ICustomEditorIconDeclarator>();
            
            foreach (var declarator in declarators)
            {
                _iconDeclarators.Add(declarator.DeclareType, declarator);
            }
        }

        private static void ItemOnGUI(string guid, Rect rect)
        {
            var getter = GetOrCreateIconGetter(guid);
            
            if (getter == null) return;

            DrawFromGetter(getter.Item1, rect, getter.Item2);
        }

        public static Tuple<Func<Sprite>, CustomSOIconsSettings> GetOrCreateIconGetter(string guid)
        {
            if (_settings is null)
                _settings = CustomAssetIconsSettingsMenu.GetSettings();
            
            if (TryGetFromCache(guid, out Tuple<Func<Sprite>, CustomSOIconsSettings> cachedGetter))
                return cachedGetter;
            
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);

            if (asset is null) return null;

            Func<Sprite> iconGetter = GetIconGetter(asset);
            var settings = _settings.GetSettings(asset);

            var newGetter = new Tuple<Func<Sprite>, CustomSOIconsSettings>(iconGetter, settings);
            
            _iconGettersCache.Add(guid, newGetter);

            return newGetter;
        }

        public static Sprite GetIconCompletely(string guid)
        {
            var iconGetter = GetOrCreateIconGetter(guid);
            var icon = GetIconFromGetter(iconGetter.Item1, iconGetter.Item2);

            return icon;
        }
        
        private static void DrawFromGetter(Func<Sprite> getter, Rect rect, CustomSOIconsSettings settings)
        {
            var icon = GetIconFromGetter(getter, settings);
            
            if (icon == null) return;

            DrawIcon(rect, icon, settings);
        }

        private static Sprite GetIconFromGetter(Func<Sprite> getter, CustomSOIconsSettings settings)
        {
            if (settings == null) return null;

            if (getter == null && settings.DefaultIcon == null) return null;

            var icon = getter?.Invoke() ?? settings.DefaultIcon;
            
            if (icon == null) return null;

            return icon;
        }
        
        private static Func<Sprite> GetIconGetter(Object asset)
        {
            Func<Sprite> getter;
            
            if (TryGetDeclaratorIconGetter(asset, out getter))
            {
                return getter;
            }

            if (TryGetAttributeIconGetter(asset, out getter))
            {
                return getter;
            }

            return null;
        }

        private static bool TryGetDeclaratorIconGetter(Object asset, out Func<Sprite> getter)
        {
            var assetType = asset.GetType();
            
            if (_iconDeclarators.ContainsKey(assetType))
            {
                var iconDeclarator = _iconDeclarators[assetType];
                getter = () => iconDeclarator.GetIcon(asset);
                return true;
            }

            getter = null;
            return false;
        }

        private static bool TryGetAttributeIconGetter(Object asset, out Func<Sprite> getter)
        {
            var assetType = asset.GetType();

            var iconField = GetIconField(assetType);

            if (iconField is { })
            {
                getter = () => iconField.GetValue<Sprite>(asset);
                return true;
            }
            
            getter = null;
            return false;
        }
        
        private static bool TryGetFromCache(string assetGuid, out Tuple<Func<Sprite>, CustomSOIconsSettings> spriteGetter)
        {
            if (!_iconGettersCache.ContainsKey(assetGuid))
            {
                spriteGetter = null;
                return false;
            }

            spriteGetter = _iconGettersCache[assetGuid];
            return true;
        }
        
        private static MemberInfo GetIconField(Type assetType)
        {
            var members = assetType.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            for (int i = 0; i < members.Length; i++)
            {
                var member = members[i];
                if (member.GetCustomAttribute<CustomAssetIconAttribute>() is {})
                {
                    if (member.GetMemberType() == typeof(Sprite))
                        return member;
                }
            }

            return null;
        }

        private static void DrawIcon(Rect rect, Sprite icon, CustomSOIconsSettings settings)
        {
            if (settings.DefaultBackground != null)
            {
                DrawBackgroundIcon(rect, settings.DefaultBackground, settings);
            }
            
            if (rect.height > rect.width)
                rect.height = rect.width;
            else
                rect.width = rect.height;

            rect.height *= settings.IconsScale;
            rect.width *= settings.IconsScale;
            AssetIconsUtils.GUIDrawSprite(rect, icon);
        }
        
        private static void DrawBackgroundIcon(Rect rect, Sprite icon, CustomSOIconsSettings settings)
        {
            if (rect.height > rect.width)
                rect.height = rect.width;
            else
                rect.width = rect.height;

            rect.height *= settings.BackgroundScale;
            rect.width *= settings.BackgroundScale;
            AssetIconsUtils.GUIDrawSprite(rect, icon);
        }
    }
}

