using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isGameOver;

    [SerializeField]
    private List<Resource> resources;

    [SerializeField]
    private float timeBetweenResourcesUpdate = 1f;

    [SerializeField]
    private SignalSender updateResourcesSignal;

    private static List<NatureTile> naturalTilesWithBuilding;
    private static List<BuildingTile> buildingTiles;

    private void Awake()
    {
        foreach (Resource resource in resources)
        {
            resource.quantity = 0;
        }
    }

    private void Start()
    {
        StartCoroutine(UpdateResources());
    }

    private IEnumerator UpdateResources()
    {
        do
        {
            yield return new WaitForSeconds(timeBetweenResourcesUpdate);
            CalculResourceChangement();
            updateResourcesSignal.Raise();

        } while (!isGameOver);
    }

    //TODO Refacto c'est moche
    private void CalculResourceChangement()
    {
        foreach(NatureTile tile in naturalTilesWithBuilding)
        {
            bool canProduce = true;
            Building building = tile.Building;
            foreach(ResourceCostStruct cost in building.passiveCost)
            {
                if(cost.resource.quantity - cost.Count < 0)
                {
                    cost.resource.quantity = 0;
                    canProduce = false;
                }
                else
                {
                    cost.resource.quantity -= cost.Count;
                }
            }

            if (!canProduce) continue;

            foreach (ResourceCostStruct income in building.income)
            {
                income.resource.quantity += income.Count;
            }
        }
        foreach (BuildingTile tile in buildingTiles)
        {
            bool canProduce = true;
            Building building = tile.Building;
            foreach (ResourceCostStruct cost in building.passiveCost)
            {
                if (cost.resource.quantity - cost.Count < 0)
                {
                    cost.resource.quantity = 0;
                    canProduce = false;
                }
                else
                {
                    cost.resource.quantity -= cost.Count;
                }
            }

            if (!canProduce) continue;

            foreach (ResourceCostStruct income in building.income)
            {
                income.resource.quantity += income.Count;
            }
        }
    }

    public static void addNaturalTileWithBuilding(NatureTile tile)
    {
        naturalTilesWithBuilding.Add(tile);
    }

    public static void addBuildingTile(BuildingTile tile)
    {
        buildingTiles.Add(tile);
    }
}
