using UnityEngine;

public interface IBuildable
{
    int Cost { get; }
    
    void Build(Transform transform);
    
}
