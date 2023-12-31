﻿using UnityEngine;

public class Floor : MonoBehaviour, IBuildHolder {

    private GameObject _building;

    public GameObject Building => _building;

    public bool PlaceBuild(GameObject buildable) {
        if (_building != null)
            return false;
        
        buildable.GetComponent<BuildController>().Build(this.transform);
        _building = buildable;

        return true;
    }

    public bool Allows(GameObject buildable) {
        if (buildable.GetComponent<Bomb>())
            return true;
        else
            return false;
    }
    
}
