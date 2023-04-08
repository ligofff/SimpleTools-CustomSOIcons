#if ODIN_INSPECTOR

using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ligofff.CustomSOIcons.Editor
{
    public class OdinAssetIconsFieldDrawer<T> : OdinValueDrawer<T> where T : Object
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
            
            var rect = ValueEntry.Property.LastDrawnValueRect;

            if (ValueEntry.SmartValue == null) return;
            if (!AssetDatabase.Contains(ValueEntry.SmartValue)) return;
            
            var guid = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(ValueEntry.SmartValue));
            
            var icon = EditorCustomSOIcons.GetIconCompletely(guid.ToString());
            
            if (icon == null) return;
            
            var textureRect = rect;
            textureRect.size = new Vector2(20f, 20f);

            if (label != null)
                textureRect.x += EditorGUIUtility.labelWidth;
            else
                textureRect.x -= 5f;
            
            AssetIconsUtils.GUIDrawSprite(textureRect, icon);
        }
    }
}

#endif