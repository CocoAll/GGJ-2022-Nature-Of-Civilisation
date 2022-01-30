using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static bool isGameOver;

    [SerializeField]
    private List<Resource> resources;

    [SerializeField]
    private float timeBetweenResourcesUpdate = 0.8f;

    [SerializeField]
    private SignalSender updateResourcesSignal;
    [SerializeField]
    private SignalSender gameOverSignal;

    private static List<NatureTile> naturalTilesWithBuilding;
    private static List<BuildingTile> buildingTiles;
    private static int initialPlanetHealth = 0;
    private static int currentPlanetHealth = 0;

    private static float timer = 0.0f;

    private void Awake()
    {
        naturalTilesWithBuilding = new List<NatureTile>();
        buildingTiles = new List<BuildingTile>();
        foreach (Resource resource in resources)
        {
            if(resource.name != "Argent")
            {
                resource.quantity = 0;
            }
            else
            {
                resource.quantity = 20;
            }
        }
    }

    private void Update()
    {
        if((float)GameController.currentPlanetHealth / (float)GameController.initialPlanetHealth < 0.1f)
        {
            GameController.isGameOver = true;
            gameOverSignal.Raise();
            Time.timeScale = 0.0f;
        }
        else
        {
            GameController.timer += Time.deltaTime;
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
            CalculResourceChangement();
            updateResourcesSignal.Raise();
            yield return new WaitForSeconds(timeBetweenResourcesUpdate);

        } while (!isGameOver);
    }

    //TODO Refacto c'est moche
    private void CalculResourceChangement()
    {
        int amountResourceConsummed = 0;
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
                amountResourceConsummed += income.Count;
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
                if(income.resource.name != "Argent" && income.resource.name != "Main d'oeuvre")
                {
                    amountResourceConsummed += income.Count;
                }
            }
        }
        ReducePlanetHealth(amountResourceConsummed);
    }

    public static void addNaturalTileWithBuilding(NatureTile tile)
    {
        naturalTilesWithBuilding.Add(tile);
    }

    public static void addBuildingTile(BuildingTile tile)
    {

        buildingTiles.Add(tile);
    }

    public static void SetInitialPlanetHealth(int init)
    {
        GameController.initialPlanetHealth = init;
        GameController.currentPlanetHealth =  init;
    }

    public static int GetCurrentPlanetHealth()
    {
        return currentPlanetHealth;
    }

    public static void ReducePlanetHealth(int amount)
    {
        GameController.currentPlanetHealth -= amount;
    }

    public static float GetTimer()
    {
        return timer;
    }
}
