using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureTile : Tile
{
    [SerializeField]
    private string name;
    [SerializeField]
    private Ressource ressource;
    [SerializeField]
    private float amountRessourceAvailable;
    [SerializeField]
    private Building building;
}
