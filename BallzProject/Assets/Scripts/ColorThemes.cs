using System.Collections.Generic;
using UnityEngine;

// Color themes from https://www.color-hex.com/color-palettes/
public class ColorThemes
{
    public Dictionary<byte, Dictionary<byte, Color32>> Colors;

    public ColorThemes()
    {
        Colors = new Dictionary<byte, Dictionary<byte, Color32>>()
        {
            {
                0, // color depth level - 0 is in the background
                new Dictionary<byte, Color32>()
                {
                    { 0, new Color32(7, 25, 42, 255) },     // lemon sapphire
                    { 1, new Color32(40, 68, 126, 255) },   // retro tipo
                    { 2, new Color32(161, 219, 178, 255) }, // smiling ear to ear
                    { 3, new Color32(87, 6, 6, 255) }       // Nudecolors
                }
            },
            {
                1,
                new Dictionary<byte, Color32>()
                {
                    { 0, new Color32(16, 57, 76, 255) },
                    { 1, new Color32(118, 142, 255, 255) },
                    { 2, new Color32(254, 229, 173, 255) },
                    { 3, new Color32(128, 35, 35, 255) }
                }
            },
            {
                2,
                new Dictionary<byte, Color32>()
                {
                    { 0, new Color32(16, 98, 129, 255) },
                    { 1, new Color32(43, 19, 121, 255) },
                    { 2, new Color32(250, 202, 102, 255) },
                    { 3, new Color32(167, 72, 72, 255) }
                }
            },
            {
                3,
                new Dictionary<byte, Color32>()
                {
                    { 0, new Color32(246, 244, 224, 255) },
                    { 1, new Color32(73, 0, 249, 255) },
                    { 2, new Color32(247, 165, 65, 255) },
                    { 3, new Color32(203, 98, 98, 255) }
                }
            },
            {
                4,
                new Dictionary<byte, Color32>()
                {
                    { 0, new Color32(243, 202, 79, 255) },
                    { 1, new Color32(204, 48, 225, 255) },
                    { 2, new Color32(244, 93, 76, 255) },
                    { 3, new Color32(214, 120, 120, 255) }
                }
            },
        };
    }
}