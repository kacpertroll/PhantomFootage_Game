using System;
using UnityEngine;
using UnityEngine.Playables;

//  ScreenFx © NullTale - https://x.com/NullTale
namespace ScreenFx
{
    [Serializable]
    public class ScreenBehaviour : PlayableBehaviour
    {
        [Tooltip("Screen sprite scale")]
        public float            _scale = 1f;
        [Tooltip("Screen sprite color")]
        public Color            _color = Color.black;
        [Tooltip("Screen sprite image")]
        public Optional<Sprite> _image;
    }
}