using UnityEngine;

public interface IColorChangeable
{   
    byte Level { get; }
    // TODO: Implement depth system using levels
    void ChangeColor(Color32 color);
}