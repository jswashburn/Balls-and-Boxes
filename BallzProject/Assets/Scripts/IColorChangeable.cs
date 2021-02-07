using UnityEngine;

public interface IColorChangeable
{   
    byte Depth { get; }
    void ChangeColor(Color32 color);
}