using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Building",menuName = "ScriptableObjects/Building", order = 1)]
public class Building : ScriptableObject
{
    public string name;

    public List<TypeTile> types;
    
    public GameObject buildingPrefab;
    public Resource resource;
    
    public int price;
    
    public ResourceCostStruct[] resourceCosts = new ResourceCostStruct[2];

    public ResourceCostStruct[] income = new ResourceCostStruct[2];

    public ResourceCostStruct[] passiveCost = new ResourceCostStruct[2];
    
    public Ameliorations[] Ameliorations = new Ameliorations[3];
    
    public bool overrideTile;

}

[Serializable]
public struct ResourceCostStruct
{
    public Resource resource;
    public int Count;
}

[Serializable]
public struct Ameliorations
{
    public Building building;
    public List<ResourceCostStruct> ressourceCosts;
}