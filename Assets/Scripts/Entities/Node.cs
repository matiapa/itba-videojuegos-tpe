using System;
using System.Net;
using UnityEngine;

public class Node : MonoBehaviour, IBuildHolder {
    private GameObject _building;

    public GameObject Building => _building;

    private void Update()
    {
        if (_building != null && !_building)
        {
            _building = null;
        }
    }

    public bool PlaceBuild(GameObject buildable) {
        if (_building != null)
        {
            try
            {
                if (_building.GetComponent<Turret>().FillingLifeController.IsDead)
                    print("Turret is dead");
                else
                    print("Can't build");
            }
            catch (Exception e)
            {
                buildable.GetComponent<BuildController>().Build(this.transform);
                _building = buildable;
                return true;
            }
            return false;
        }

        buildable.GetComponent<BuildController>().Build(this.transform);
        _building = buildable;

        return true;
    }
    
    public bool Allows(GameObject buildable) {
        if (buildable.GetComponent<Turret>())
            return true;
        else
            return false;
    }
    
}
