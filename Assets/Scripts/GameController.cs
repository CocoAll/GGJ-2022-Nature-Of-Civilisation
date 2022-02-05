using System;
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
    [SerializeField]
    private GameObject wasteTile;

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
        if ((float)GameController.currentPlanetHealth / (float)GameController.initialPlanetHealth < 0.1f)
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
                if (tile.amountRessourceAvailable - income.Count >= 0)
                {
                    income.resource.quantity += income.Count;
                    amountResourceConsummed += income.Count;
                }
                else
                {
                    if(tile.amountRessourceAvailable > 0)
                    {
                        income.resource.quantity += tile.amountRessourceAvailable;
                        amountResourceConsummed += tile.amountRessourceAvailable;
                        tile.amountRessourceAvailable = 0;
                    }
                }  
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

            if (tile.Building.pollutingTile)
            {
                Vector2 tilePos = new Vector2(tile.transform.position.x, tile.transform.position.z);
                List<Tile> neighbors = GridManager.GetTileNeighbor(tilePos);

                foreach(Tile neighbor in neighbors)
                {
                    if(neighbor is NatureTile)
                    {
                        NatureTile nature = (NatureTile)neighbor;
                        nature.amountRessourceAvailable -= 2;
                        if (nature.amountRessourceAvailable <= 0)
                        {
                            NatureTileToWaste(nature);
                        }
                    }
                }

            }

            if (!canProduce) continue;

            foreach (ResourceCostStruct income in building.income)
            {
                income.resource.quantity += income.Count;
            }
        }
        ReducePlanetHealth(amountResourceConsummed);
    }

    private void NatureTileToWaste(NatureTile nature)
    {
        GameObject waste = Instantiate(wasteTile, nature.transform.position, nature.transform.rotation);
        GridManager.UpdateGridElement(waste.GetComponent<WasteTile>());
        Destroy(nature.gameObject);
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
