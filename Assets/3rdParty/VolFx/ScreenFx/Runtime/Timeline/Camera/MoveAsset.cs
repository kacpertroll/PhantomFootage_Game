using System;
using UnityEngine;
using UnityEngine.Playables;

//  ScreenFx © NullTale - https://x.com/NullTale
namespace ScreenFx
{
    [Serializable]
    public class MoveAsset : PlayableAsset
    {
        [InplaceField(nameof(CameraBehaviour._offset), nameof(CameraBehaviour._fov), nameof(CameraBehaviour._roll))]
        public CameraBehaviour _template;

        // =======================================================================
        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
#if UNITY_EDITOR
            if (UnityEngine.Object.FindObjectOfType<ScreenFx>() == null)
                Debug.LogWarning($"ScreenFx : the Scene has no CinemachineCamera with ScreenFx component on it");
#endif
            var playable = ScriptPlayable<CameraBehaviour>.Create(graph, _template);

            return playable;
        }
    }
}