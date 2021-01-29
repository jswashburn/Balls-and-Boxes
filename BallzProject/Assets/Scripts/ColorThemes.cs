using System.Collections.Generic;
using UnityEngine;

// Color themes from https://www.color-hex.com/color-palettes/
public struct ColorThemes
{
    public Dictionary<int, Color32> Colors;
    public static readonly int numThemes = 4;

    public ColorThemes(bool primary)
    {
        Colors = new Dictionary<int, Color32>();

        // Fill up colors
        if (primary)
        {
            // Add primary colors for objects up front
            // Colors.Add(0, new Color32(199, 193, 189, 255));  // brown
            Colors.Add(1, new Color32(243, 202, 79, 255));      // lemon sapphire
            Colors.Add(2, new Color32(244, 93, 76, 255));       // smiling ear to ear
            Colors.Add(3, new Color32(204, 48, 225, 255));      // retro-tipo
        }
        // TODO: Levels 1, 2, 3, 4, 5
        // TODO: Change starting color theme from brown to something less terrible
        else
        {
            // Add secondary colors for objects toward the background
            // Colors.Add(0, new Color32(141, 128, 121, 255));
            Colors.Add(1, new Color32(7, 25, 42, 255));
            Colors.Add(2, new Color32(161, 219, 178, 255));
            Colors.Add(3, new Color32(40, 68, 126, 255));
        }
    }
}