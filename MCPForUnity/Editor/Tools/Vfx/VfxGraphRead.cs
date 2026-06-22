using Newtonsoft.Json.Linq;
using UnityEngine;

#if UNITY_VFX_GRAPH
using UnityEngine.VFX;
#endif

#if UNITY_2021_2_OR_NEWER  // disabled on Unity 2020.3 (non-core tool / 2021.2+ APIs)
namespace MCPForUnity.Editor.Tools.Vfx
{
    /// <summary>
    /// Read operations for VFX Graph (VisualEffect component).
    /// Requires com.unity.visualeffectgraph package and UNITY_VFX_GRAPH symbol.
    /// </summary>
    internal static class VfxGraphRead
    {
#if !UNITY_VFX_GRAPH
        public static object GetInfo(JObject @params)
        {
            return new { success = false, message = "VFX Graph package (com.unity.visualeffectgraph) not installed" };
        }
#else
        public static object GetInfo(JObject @params)
        {
            VisualEffect vfx = VfxGraphCommon.FindVisualEffect(@params);
            if (vfx == null)
            {
                return new { success = false, message = "VisualEffect not found" };
            }

            return new
            {
                success = true,
                data = new
                {
                    gameObject = vfx.gameObject.name,
                    assetName = vfx.visualEffectAsset?.name ?? "None",
                    aliveParticleCount = vfx.aliveParticleCount,
                    culled = vfx.culled,
                    pause = vfx.pause,
                    playRate = vfx.playRate,
                    startSeed = vfx.startSeed
                }
            };
        }
#endif
    }
}
#endif
