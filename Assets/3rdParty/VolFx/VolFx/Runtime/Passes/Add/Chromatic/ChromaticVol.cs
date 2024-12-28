using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//  VolFx © NullTale - https://x.com/NullTale
namespace VolFx
{
    [Serializable, VolumeComponentMenu("VolFx/Chromatic")]
    public sealed class ChromaticVol : VolumeComponent, IPostProcessComponent
    {
        public ClampedFloatParameter         _Weight    = new ClampedFloatParameter(1f, 0f, 1f);
        public ClampedFloatParameter         _Intensity = new ClampedFloatParameter(0f, -1f, 1f);
        public ClampedFloatParameter         _Radial    = new ClampedFloatParameter(0f, -1f, 1f);
        public ClampedFloatParameter         _Alpha     = new ClampedFloatParameter(0f, -1f, 1f);
        public ClampedFloatParameter         _Angle     = new ClampedFloatParameter(0f, -1f, 1f);
        public NoInterpClampedFloatParameter _Split     = new NoInterpClampedFloatParameter(0f, -1f, 1f);
        public ClampedFloatParameter         _Sat       = new ClampedFloatParameter(1f, 0f, 1f);
        public BoolParameter                 _Mono      = new BoolParameter(false, false);
        
        // =======================================================================
        public bool IsActive() => active && (_Intensity.value != 0f || _Radial.value != 0f && _Weight.value > 0f);

        public bool IsTileCompatible() => true;
    }
}