using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

//  ScreenFx © NullTale - https://x.com/NullTale
namespace ScreenFx
{
    [TrackColor(0.09803922f, 0.09803922f, 0.09803922f)]
    [TrackClipType(typeof(ScreenAsset))]
    public class ScreenTrack : TrackAsset
    {
        public RenderMode _renderMode   = RenderMode.ScreenSpaceCamera;
        public int        _sortingOrder = 10000;
        [Tooltip("Use screen shot of the asset image (Runtime only, Screen shot will be taken when asset start playing)")]
        public bool       _screenShot;
        [Tooltip("Alpha multiplayer for track assets, to control general flashes intensity")]
        public float      _alphaMul = 1f;
        [Layer]
        public int        _layer;
        
        // =======================================================================
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixerTrack = ScriptPlayable<ScreenMixer>.Create(graph, inputCount);
            
            var mixer  = mixerTrack.GetBehaviour();
            mixer._sortingOrder = _sortingOrder;
            mixer._renderMode   = _renderMode;
            mixer._screenShot   = _screenShot;
            mixer._weight       = _alphaMul;
            mixer._layer        = _layer;
            mixer._name         = name;

            return mixerTrack;
        }
    }
}