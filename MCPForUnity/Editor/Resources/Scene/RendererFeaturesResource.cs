#if UNITY_2021_2_OR_NEWER  // disabled on 2020.3 (depends on disabled tool namespace)
using System;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Tools.Graphics;
using Newtonsoft.Json.Linq;

namespace MCPForUnity.Editor.Resources.Scene
{
    [McpForUnityResource("get_renderer_features")]
    public static class RendererFeaturesResource
    {
        public static object HandleCommand(JObject @params)
        {
            try
            {
                return RendererFeatureOps.ListFeatures(@params ?? new JObject());
            }
            catch (Exception e)
            {
                McpLog.Error($"[RendererFeaturesResource] Error: {e}");
                return new ErrorResponse($"Error listing renderer features: {e.Message}");
            }
        }
    }
}
#endif
