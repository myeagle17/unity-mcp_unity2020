#if UNITY_2021_2_OR_NEWER  // disabled on 2020.3 (depends on disabled tool namespace)
using System;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Tools.Graphics;
using Newtonsoft.Json.Linq;

namespace MCPForUnity.Editor.Resources.Scene
{
    [McpForUnityResource("get_volumes")]
    public static class VolumesResource
    {
        public static object HandleCommand(JObject @params)
        {
            try
            {
                return VolumeOps.ListVolumes(@params ?? new JObject());
            }
            catch (Exception e)
            {
                McpLog.Error($"[VolumesResource] Error listing volumes: {e}");
                return new ErrorResponse($"Error listing volumes: {e.Message}");
            }
        }
    }
}
#endif
