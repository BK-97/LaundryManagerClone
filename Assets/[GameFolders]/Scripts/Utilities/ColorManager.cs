using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ColorManager : Singleton<ColorManager>
{
    public List<Colors> Colors;
    public Color GetColorCode(EnumTypes.ColorTypes colorType)
    {
        for (int i = 0; i < Colors.Count; i++)
        {
            if (Colors[i].colorType == colorType)
                return Colors[i].colorCode;
        }
        return Color.white;
    }
}
[Serializable]
public class Colors
{
    public Color colorCode;
    public EnumTypes.ColorTypes colorType;

    public Colors(Color color,EnumTypes.ColorTypes colType)
    {
        colorCode = color;
        colorType = colType;
    }
}