using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public enum Quality { Common, Uncommon, Rare, Epic }
public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        {Quality.Common, "#9EC5AB" },
        {Quality.Uncommon, "#00ff00ff"},
        {Quality.Rare, "#0E6BECFF" },
        {Quality.Epic, "#A712D8FF"},
    };

    public static Dictionary<Quality, string> MyColors
    {
        get
        {
            return colors;
        }
    }
}
