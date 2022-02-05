using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Building",menuName = "ScriptableObjects/Building", order = 1)]
public class Building : ScriptableObject
{
    public string name;

    public Sprite icon;

    public List<TypeTile> types;
    
    public GameObject buildingPrefab;
    
    public ResourceCostStruct[] resourceCosts = new ResourceCostStruct[2];

    public ResourceCostStruct[] income = new ResourceCostStruct[2];

    public ResourceCostStruct[] passiveCost = new ResourceCostStruct[2];
    
    public Ameliorations[] Ameliorations = new Ameliorations[3];
    
    public bool overrideTile;
    public bool pollutingTile;

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