using UnityEngine;

public interface IBuildHolder
{
    GameObject Building { get; }

    bool PlaceBuild(GameObject objectToBuild);

    bool Allows(GameObject objectToBuild);
}
