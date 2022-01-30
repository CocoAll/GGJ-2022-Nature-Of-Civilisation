using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NatureTile : Tile
{
    public int amountRessourceAvailable;
    [SerializeField]
    private bool isBuildable;
    [SerializeField]
    private Building building;

    [SerializeField]
    private GameObject toDesactivate;

    public Building Building { get { return building; } set { if (building == null) this.building = value; } }

    public override bool IsBuildable()
    {
        return this.isBuildable;
    }

    public override bool IsNature()
    {
        return true;
    }

    public override string ModalInfos()
    {
        throw new System.NotImplementedException();
    }

    public void ConstructOnTile(Building building)
    {
        if(this.toDesactivate != null)
        {
            this.toDesactivate.SetActive(false);
        }
        this.building = building;
        isBuildable = false;
    }
}
