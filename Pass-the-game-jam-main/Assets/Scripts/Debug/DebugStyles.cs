using UnityEngine;

#if UNITY_EDITOR
public static class DebugStyles
{
    private static GUIStyle debugLabelStyle;

    public static GUIStyle DebugLabelStyle
    {
        get
        {
            if (debugLabelStyle == null)
            {
                var style = new GUIStyle();
                style.normal.textColor = Color.white;
                style.fontSize = 16;
                style.richText = true;
                debugLabelStyle = style;
            }

            return debugLabelStyle;
        }
    }
}
#endif