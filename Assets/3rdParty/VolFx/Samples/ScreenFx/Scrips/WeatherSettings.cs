using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//  ScreenFx © NullTale - https://twitter.com/NullTale/
namespace ScreenFx
{
    [VolumeComponentMenuForRenderPipeline("Weather (ScreenFxSample)", typeof(UniversalRenderPipeline))]
    internal class WeatherSettings : VolumeComponent
    {
        public ClampedFloatParameter _snow   = new ClampedFloatParameter(0, 0, 1);
        public ClampedFloatParameter _sun    = new ClampedFloatParameter(0, 0, 1);
        public ClampedFloatParameter _clouds = new ClampedFloatParameter(0, 0, 1);
    }
}