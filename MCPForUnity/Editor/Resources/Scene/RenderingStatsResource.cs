#if UNITY_2021_2_OR_NEWER  // disabled on 2020.3 (depends on disabled tool namespace)
using System;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Tools.Graphics;
using Newtonsoft.Json.Linq;

namespace MCPForUnity.Editor.Resources.Scene
{
    [McpForUnityResource("get_rendering_stats")]
    public static class RenderingStatsResource
    {
        public static object HandleCommand(JObject @params)
        {
            try
            {
                return RenderingStatsOps.GetStats(@params ?? new JObject());
            }
            catch (Exception e)
            {
                McpLog.Error($"[RenderingStatsResource] Error: {e}");
                return new ErrorResponse($"Error getting rendering stats: {e.Message}");
            }
        }
    }
}
#endif
