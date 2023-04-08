using System;
using UnityEngine;

namespace Ligofff.CustomSOIcons.Editor
{
    [Serializable]
    public class CustomSOIconsSettings
    {
        [SerializeField]
        private Sprite defaultIcon;
        
        [Range(0f, 1f), SerializeField]
        private float iconsScale = 0.5f;

        [SerializeField]
        private Sprite backgroundImage;
        
        [Range(0f, 1f), SerializeField]
        private float backgroundImageScale = 0.5f;

        public float BackgroundScale => backgroundImageScale;
        public float IconsScale => iconsScale;
        public Sprite DefaultIcon => defaultIcon;
        public Sprite DefaultBackground => backgroundImage;
    }
}