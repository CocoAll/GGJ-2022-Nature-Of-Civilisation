using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureTile : Tile
{
    [SerializeField]
    private Resource ressource;
    [SerializeField]
    private float amountRessourceAvailable;
    [SerializeField]
    private bool isBuildable;
    [SerializeField]
    private Building building;

    
    public Building Building { get { return building; } }
    public Resource Ressource { get { return ressource; } }
    public float AmountRessourceAvailable { get { return amountRessourceAvailable; } }

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
        this.building = building;
        isBuildable = false;
    }
}
