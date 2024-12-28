using System;
using UnityEngine;
using UnityEngine.Playables;

//  ScreenFx © NullTale - https://x.com/NullTale
namespace ScreenFx
{
    [Serializable]
    public class ScreenAsset : PlayableAsset
    {
        [InplaceField(nameof(ScreenBehaviour._color), nameof(ScreenBehaviour._scale), nameof(ScreenBehaviour._image))]
        public ScreenBehaviour m_Template;

        // =======================================================================
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            var playable = ScriptPlayable<ScreenBehaviour>.Create(graph, m_Template);
            var beh      = playable.GetBehaviour();

            return playable;
        }
    }
}