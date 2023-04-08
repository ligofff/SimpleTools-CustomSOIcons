using System;
using System.Reflection;
using UnityEngine;

namespace Ligofff.CustomSOIcons.Editor
{
    public static class AssetIconsUtils
    {
        public static T GetValue<T>(this MemberInfo memberInfo, object forObject)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return (T) ((FieldInfo)memberInfo).GetValue(forObject);
                case MemberTypes.Property:
                    return (T) ((PropertyInfo)memberInfo).GetValue(forObject);
                default:
                    throw new NotImplementedException();
            }
        }

        public static Type GetMemberType(this MemberInfo info)
        {
            if (info is PropertyInfo propertyInfo) return propertyInfo.PropertyType;
            if (info is FieldInfo fieldInfo) return fieldInfo.FieldType;
            return null;
        }
        
        public static void GUIDrawSprite(Rect rect, Sprite sprite)
        {
            if (sprite == null) return;
            Rect spriteRect = sprite.rect;
            Texture2D tex = sprite.texture;
            GUI.DrawTextureWithTexCoords(rect, tex, 
                new Rect(spriteRect.x / tex.width, spriteRect.y / tex.height, spriteRect.width / tex.width, spriteRect.height / tex.height));
        }
    }
}