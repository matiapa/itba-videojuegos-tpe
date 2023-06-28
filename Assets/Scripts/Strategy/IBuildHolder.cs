using UnityEngine;

public interface IBuildHolder
{
    GameObject Building { get; }

    bool PlaceBuild(GameObject objectToBuild);
}
