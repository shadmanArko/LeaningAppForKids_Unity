using System.Collections.Generic;
using UnityEngine;

public static class ColorData
{
    public readonly static Dictionary<Color, string> colorDictionary = new Dictionary<Color, string>
    {
        {Color.white, "White" },
        {Color.black, "Black" },
        {Color.blue, "Blue" },
        {Color.green, "Green" },
        {Color.red, "Red" },
        {new Color(255, 165, 0), "Orange" },
        {new Color(143, 0, 255), "Violet" },
        {new Color(135,206,235), "Sky Blue" }

    };
}
