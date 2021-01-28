using System.Collections.Generic;
using UnityEngine;

// Color themes from https://www.color-hex.com/color-palettes/
public readonly struct ColorThemes
{
    public readonly Dictionary<string, Color32> Colors;

    public ColorThemes(bool primary)
    {
        Colors = new Dictionary<string, Color32>();

        // Fill up colors
        if (primary)
        {
            // Add primary colors for objects up front
            Colors.Add("brown", new Color32(199, 193, 189, 255));
            Colors.Add("lemon-sapphire", new Color32(243, 202, 79, 255));
            Colors.Add("smiling-ear-to-ear", new Color32(244, 93, 76, 255));
            Colors.Add("retro-tipo", new Color32(204, 48, 225, 255));
        }
        // TODO: Levels 1, 2, 3, 4, 5
        else
        {
            // Add secondary colors for objects toward the background
            Colors.Add("brown", new Color32(141, 128, 121, 255));
            Colors.Add("lemon-sapphire", new Color32(7, 25, 42, 255));
            Colors.Add("smiling-ear-to-ear", new Color32(161, 219, 178, 255));
            Colors.Add("retro-tipo", new Color32(40, 68, 126, 255));
        }
    }
}

public struct ThemeNames
{
    public static Dictionary<int, string> Names { get; } = new Dictionary<int, string>()
    {
        {0, "brown"},
        {1, "lemon-sapphire"},
        {2, "smiling-ear-to-ear"},
        {3, "retro-tipo"}
    };
}