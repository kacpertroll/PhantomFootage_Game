using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

//  ScreenFx © NullTale - https://x.com/NullTale
namespace ScreenFx
{
    public class ScreenMixer : PlayableBehaviour
    {
        public static WaitForEndOfFrame s_WaitLateUpdate = new WaitForEndOfFrame();
        
        public  int           _sortingOrder;
        public  RenderMode    _renderMode;
        public  bool          _screenShot;
        private Sprite        _screenShotTex;
        private ScreenOverlay _handle;
        public  float         _weight;
        public  int           _layer;
        public string         _name;

        // =======================================================================
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            _handle = new ScreenOverlay(_sortingOrder, _renderMode, _layer, _name);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            _handle?.Dispose();
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var inputCount = playable.GetInputCount();

            // calculate weights
            var scale       = 0f;
            var color       = Color.clear;
            var imageWeight = 0f;

            Optional<Sprite> image = null;

            var fullWeight = 0f;
            var soloInput = 0;

            for (var n = 0; n < inputCount; n++)
            {
                // get clips data
                var inputWeight   = playable.GetInputWeight(n);
                if (inputWeight <= 0f)
                    continue;

                soloInput  =  n;
                fullWeight += inputWeight;

                var inputPlayable = (ScriptPlayable<ScreenBehaviour>)playable.GetInput(n);
                var behaviour     = inputPlayable.GetBehaviour();

                scale += behaviour._scale * inputWeight;
                color += behaviour._color * inputWeight;

                if (imageWeight < inputWeight)
                {
                    image       = (behaviour._image.Enabled) ? behaviour._image : null;
                    imageWeight = inputWeight;
                }
            }

            fullWeight *= _weight;
            
            if (fullWeight > 0f)
            {
                _handle.Open();
                _takeScreen();
            }
            else
            {
                _handle.Close();
                _releaseScreen();
                return;
            }

            // if single input, blend alpha, do nothing with scale
            if (fullWeight < 1f)
            {
                var behaviour = ((ScriptPlayable<ScreenBehaviour>)playable.GetInput(soloInput)).GetBehaviour();

                scale = behaviour._scale;
                color = behaviour._color.MulA(fullWeight);
            }

            _handle.Scale  = scale.ToVector2XY();
            _handle.Color  = color;
            _handle.Sprite = image != null ? image.Value : (_screenShot ? _screenShotTex : null);
        }
        
        // =======================================================================
        private void _takeScreen()
        {
            if (Application.isPlaying == false)
                return;
            
            if (_screenShotTex != null)
                return;
            
            _screenShotTex = Utils.s_SpriteClear;
            ScreenFx.Instance.StartCoroutine(_takeScreenShot());

            // -----------------------------------------------------------------------
            IEnumerator _takeScreenShot()
            {
                yield return s_WaitLateUpdate;
                var tex = ScreenCapture.CaptureScreenshotAsTexture();
                tex.filterMode = FilterMode.Point;
                _screenShotTex = Sprite.Create(tex, tex.GetRect(), new Vector2(0.5f, 0.5f), Mathf.Max(tex.width, tex.height));
            }
        }

        private void _releaseScreen()
        {
            if (Application.isPlaying == false)
                return;
            
            if (_screenShotTex)
            {
                Object.Destroy(_screenShotTex.texture);
                Object.Destroy(_screenShotTex);
                _screenShotTex = null;
            }
        }
    }
}