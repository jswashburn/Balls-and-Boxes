using UnityEngine;

public interface IColorChangeable
{   
    byte Level { get; }
    void ChangeColor(Color32 color);
}