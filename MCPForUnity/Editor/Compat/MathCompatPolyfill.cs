// System.Math.Clamp does not exist on Unity 2020.3 (.NET Standard 2.0).
// `McpMath.Clamp(...)` call sites are rewritten to `McpMcpMath.Clamp(...)`.
// Global namespace so it is reachable everywhere without a using.
internal static class McpMath
{
    public static int Clamp(int value, int min, int max) => value < min ? min : (value > max ? max : value);
    public static long Clamp(long value, long min, long max) => value < min ? min : (value > max ? max : value);
    public static float Clamp(float value, float min, float max) => value < min ? min : (value > max ? max : value);
    public static double Clamp(double value, double min, double max) => value < min ? min : (value > max ? max : value);
}
