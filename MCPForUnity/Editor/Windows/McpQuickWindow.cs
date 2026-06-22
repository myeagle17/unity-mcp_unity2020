using System.Threading.Tasks;
using MCPForUnity.Editor.Services;
using UnityEditor;
using UnityEngine;

namespace MCPForUnity.Editor.Windows
{
    /// <summary>
    /// Minimal IMGUI window to start/stop the MCP bridge on Unity 2020.3.
    /// Replaces the full UXML window (which uses 2021.2+ UI Toolkit elements that
    /// crash on 2020.3). One button: Start MCP.
    /// </summary>
    public class McpQuickWindow : EditorWindow
    {
        private string _status = "";

        [MenuItem("Window/MCP For Unity/Quick Start (MCP)", priority = 0)]
        public static void Open()
        {
            var w = GetWindow<McpQuickWindow>("MCP Quick");
            w.minSize = new Vector2(320, 170);
            w.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("MCP for Unity — Quick Start", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            bool running = false;
            int port = 0;
            try
            {
                running = MCPServiceLocator.Bridge.IsRunning;
                port = MCPServiceLocator.Bridge.CurrentPort;
            }
            catch { /* services not ready */ }

            EditorGUILayout.LabelField("Bridge", running ? $"Running (stdio, port {port})" : "Stopped");

            EditorGUILayout.Space();
            using (new EditorGUI.DisabledScope(running))
            {
                if (GUILayout.Button("Start MCP", GUILayout.Height(40)))
                    _ = StartAsync();
            }
            using (new EditorGUI.DisabledScope(!running))
            {
                if (GUILayout.Button("Stop MCP"))
                    _ = StopAsync();
            }

            if (GUILayout.Button("Refresh"))
                Repaint();

            if (!string.IsNullOrEmpty(_status))
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(_status, MessageType.Info);
            }
        }

        private async Task StartAsync()
        {
            _status = "Starting (stdio)…";
            Repaint();
            try
            {
                // Force the stdio TCP bridge: the editor listens on port 6400 and writes
                // ~/.unity-mcp/unity-mcp-status-<hash>.json, which the stdio MCP server reads
                // to discover this Unity instance. (HTTP mode does not write that file and can
                // hit localhost-proxy issues.)
                EditorConfigurationCache.Instance.SetUseHttpTransport(false);

                bool ok = await MCPServiceLocator.Bridge.StartAsync();
                _status = ok
                    ? $"MCP stdio bridge running on port {MCPServiceLocator.Bridge.CurrentPort}. The MCP client can now connect."
                    : "Bridge.StartAsync returned false — check Console for details.";
            }
            catch (System.Exception e)
            {
                _status = "Error: " + e.Message;
            }
            Repaint();
        }

        private async Task StopAsync()
        {
            _status = "Stopping…";
            Repaint();
            try
            {
                await MCPServiceLocator.Bridge.StopAsync();
                _status = "Stopped.";
            }
            catch (System.Exception e)
            {
                _status = "Error: " + e.Message;
            }
            Repaint();
        }
    }
}
