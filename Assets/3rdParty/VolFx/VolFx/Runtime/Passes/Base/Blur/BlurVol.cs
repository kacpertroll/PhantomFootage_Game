using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//  VolFx © NullTale - https://x.com/NullTale
namespace VolFx
{
    [Serializable, VolumeComponentMenu("VolFx/Blur")]
    public sealed class BlurVol : VolumeComponent, IPostProcessComponent
    {
        public ClampedFloatParameter         m_Radius   = new ClampedFloatParameter(0, 0, 1);
        public ClampedFloatParameter         m_Radial   = new ClampedFloatParameter(0, 0, 1);
        public ClampedIntParameter           m_Samples  = new ClampedIntParameter(9, 3, 18);
        public ClampedFloatParameter         m_Aspect   = new ClampedFloatParameter(0, -1, 1);
        public NoInterpClampedFloatParameter m_Angle    = new NoInterpClampedFloatParameter(0, -360f, 360f);
        public CurveParameter                m_Adaptive = new CurveParameter(new CurveValue(AnimationCurve.Linear(1, 1, 1, 1)), false);

        // =======================================================================
        // Can be used to skip rendering if false
        public bool IsActive() => active && (m_Radius.value > 0 || m_Radial.value > 0);

        public bool IsTileCompatible() => false;
    }
}