using UnityEngine;

public class CmdBuild: ICommand {
    private IBuildHolder _buildHolder;
    private GameObject objectToBuild;

    public CmdBuild(IBuildHolder _buildHolder, GameObject objectToBuild) {
        this._buildHolder = _buildHolder;
        this.objectToBuild = objectToBuild;
    }

    public void Execute() => _buildHolder.PlaceBuild(objectToBuild);
}
