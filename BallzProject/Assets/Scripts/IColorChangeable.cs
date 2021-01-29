public interface IColorChangeable
{   
    bool Primary { get; set; } // The lighter objects
    // TODO: Implement depth system using levels
    void ChangeColor(int theme);
}