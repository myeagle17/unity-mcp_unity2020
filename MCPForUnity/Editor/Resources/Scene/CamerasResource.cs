#if UNITY_2021_2_OR_NEWER  // disabled on 2020.3 (depends on disabled tool namespace)
using System;
using System.Collections.Generic;
using System.Linq;
using MCPForUnity.Editor.Helpers;
using MCPForUnity.Editor.Tools.Cameras;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace MCPForUnity.Editor.Resources.Scene
{
    [McpForUnityResource("get_cameras")]
    public static class CamerasResource
    {
        public static object HandleCommand(JObject @params)
        {
            try
            {
                return CameraControl.ListCameras(@params ?? new JObject());
            }
            catch (Exception e)
            {
                McpLog.Error($"[CamerasResource] Error listing cameras: {e}");
                return new ErrorResponse($"Error listing cameras: {e.Message}");
            }
        }
    }
}
#endif
