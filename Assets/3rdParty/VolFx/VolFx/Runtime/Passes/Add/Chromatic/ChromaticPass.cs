using UnityEngine;

//  VolFx © NullTale - https://x.com/NullTale
namespace VolFx
{
    [ShaderName("Hidden/VolFx/Chromatic")]
    public class ChromaticPass : VolFx.Pass
    {
        private static readonly int s_Weight    = Shader.PropertyToID("_Weight");
        private static readonly int s_Radial    = Shader.PropertyToID("_Radial");
        private static readonly int s_Intensity = Shader.PropertyToID("_Intensity");
        private static readonly int s_Alpha     = Shader.PropertyToID("_Alpha");
        private static readonly int s_R         = Shader.PropertyToID("_R");
        private static readonly int s_G         = Shader.PropertyToID("_G");
        private static readonly int s_B         = Shader.PropertyToID("_B");
        private static readonly int s_Rw        = Shader.PropertyToID("_Rw");
        private static readonly int s_Gw        = Shader.PropertyToID("_Gw");
        private static readonly int s_Bw        = Shader.PropertyToID("_Bw");
		
		public override string ShaderName => string.Empty;
        
        private float _angle;

        // =======================================================================
        public override void Init()
        {
            _material.SetVector(s_R, Vector2.left);
            _material.SetVector(s_G, Vector2.right);
            _material.SetVector(s_B, Vector2.up);
        }

        public override bool Validate(Material mat)
        {
            var settings = Stack.GetComponent<ChromaticVol>();

            if (settings.IsActive() == false)
                return false;
            
            _angle = settings._Angle.value * (Mathf.PI * 2f);
            _angle %= (Mathf.PI * 2f);
            
            mat.SetFloat(s_Intensity, settings._Intensity.value * 0.07f);
                
            if (settings._Mono.value)
                _chromaTwo();
            else
                _chromaThree();
            
            return true;
            
            // =======================================================================
            void _chromaTwo()
            {
                var aspect = Screen.width / (float)Screen.height;
                var step = (Mathf.PI * 2f) / 2f;

                mat.SetVector(s_R, (_angle + step * 0f).ToNormal() * new Vector2(1f / aspect, 1f));
                mat.SetVector(s_G, (_angle + step * 1f).ToNormal() * new Vector2(1f / aspect, 1f));
                mat.SetVector(s_B, Vector4.zero);

                mat.SetFloat(s_Weight, settings._Weight.value);
                mat.SetFloat(s_Radial, settings._Radial.value);

                var split = settings._Split.value;
                var sat   = settings._Sat.value;
                var ca = Color.HSVToRGB(Mathf.Abs(.0f + split) % 1, sat, 1);
                var cb = Color.HSVToRGB(Mathf.Abs(.5f + split) % 1, sat, 1);
                var cc = Color.clear;
                mat.SetColor(s_Rw, ca);
                mat.SetColor(s_Gw, cb);
                mat.SetColor(s_Bw, cc);

                mat.SetFloat(s_Alpha, settings._Alpha.value >= 0f ? Mathf.Lerp(.5f, 3f, settings._Alpha.value) : Mathf.Lerp(0f, .5f, 1f + settings._Alpha.value));
            }

            void _chromaThree()
            {
                var aspect = Screen.width / (float)Screen.height;
                var step = (Mathf.PI * 2f) / 3f;
                
                _angle = settings._Angle.value * (Mathf.PI * 2f);
                _angle %= (Mathf.PI * 2f);
            
                mat.SetVector(s_R, (_angle + step * 0f).ToNormal() * new Vector2(1f / aspect, 1f));
                mat.SetVector(s_G, (_angle + step * 1f).ToNormal() * new Vector2(1f / aspect, 1f));
                mat.SetVector(s_B, (_angle + step * 2f).ToNormal() * new Vector2(1f / aspect, 1f));

                mat.SetFloat(s_Weight, settings._Weight.value);
                mat.SetFloat(s_Radial, settings._Radial.value);

                var split = settings._Split.value;
                var sat   = settings._Sat.value;
                mat.SetColor(s_Rw, Color.HSVToRGB(Mathf.Abs(.00f + split) % 1, sat, 1));
                mat.SetColor(s_Gw, Color.HSVToRGB(Mathf.Abs(.33f + split) % 1, sat, 1));
                mat.SetColor(s_Bw, Color.HSVToRGB(Mathf.Abs(.66f + split) % 1, sat, 1));

                mat.SetFloat(s_Alpha, settings._Alpha.value >= 0f ? Mathf.Lerp(1f / 3f, 3f, settings._Alpha.value) : Mathf.Lerp(0f, 1f / 3f, 1f + settings._Alpha.value));
            }
        }
    }
}