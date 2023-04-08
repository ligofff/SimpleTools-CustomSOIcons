using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ligofff.CustomSOIcons.Editor
{
    public interface ICustomEditorIconDeclarator
    {
        Type DeclareType { get; }
        Sprite GetIcon(Object asset);
    }
}