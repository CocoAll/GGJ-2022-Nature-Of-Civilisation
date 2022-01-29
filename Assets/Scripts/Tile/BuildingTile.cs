using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTile : Tile
{
    [SerializeField]
    private Building building;

    public Building Building { get { return building; } set { if (building == null) this.building = value; } }

    public override bool IsBuildable()
    {
        return false;
    }

    public override bool IsNature()
    {
        return false;
    }

    public override string ModalInfos()
    {
        return building.name;
    }
}
