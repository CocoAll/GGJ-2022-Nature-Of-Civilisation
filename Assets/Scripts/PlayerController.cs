using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Building buildingToConstruct;

    [SerializeField]
    private BuildingReference selectedBuilding;

    [SerializeField]
    private SignalSender resourcesUpdated;

    Tile hoverTile;
    PlayerInputs inputs;

    private void Awake()
    {
        inputs = new PlayerInputs();
        inputs.Inputs.Action.performed += _ => OnMousePressed();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out hit))
        {
            Tile tile = hit.transform.gameObject.GetComponent<Tile>();
            if(hoverTile == null || hoverTile != tile)
            {
                hoverTile = tile;
            }
        }
        else if(hoverTile != null)
        {
            hoverTile = null;
        }
    }

    public void OnBuildingSelected()
    {
        if(selectedBuilding.building != null)
        {
            bool canBeBuild = true;
            foreach(ResourceCostStruct resourceCost in selectedBuilding.building.resourceCosts)
            {
                if (resourceCost.resource.quantity < resourceCost.Count)
                {
                    canBeBuild = false;
                    break;
                }
            }
            if (canBeBuild)
            {
                buildingToConstruct = selectedBuilding.building;
            }
        }
    }
    
    public bool OnAemliorationSelected(BuildingTile buildingTile)
    {
        bool canBeBuild = true;
        
        if (buildingTile.Building.Ameliorations.Length == 0)
        {
            return false;
        }

        
        foreach(ResourceCostStruct resourceCost in buildingTile.Building.Ameliorations[0].ressourceCosts)
        {
            if (resourceCost.resource.quantity < resourceCost.Count)
            {
                canBeBuild = false;
                break;
            }
        }

        return canBeBuild;
    }
    
    private void OnMousePressed()
    {
        if(!hoverTile.IsNature() && OnAemliorationSelected((BuildingTile) hoverTile))
        {
            BuildingTile buildingTile = (BuildingTile) hoverTile;
            AmeliorationsBuilding(buildingTile);
            foreach (ResourceCostStruct resourceCost in buildingTile.Building.Ameliorations[0].ressourceCosts)
            {
                resourceCost.resource.quantity -= resourceCost.Count;
            }
            resourcesUpdated.Raise();

            buildingToConstruct = null;
        }
        
        if (hoverTile == null || !hoverTile.IsBuildable() | buildingToConstruct == null) return;

        if (buildingToConstruct.types.Contains(hoverTile.type))
        {
            if (!buildingToConstruct.overrideTile) ConstructBuilding((NatureTile)hoverTile);
            else ConstructBuilding(hoverTile, buildingToConstruct);
 
            foreach (ResourceCostStruct resourceCost in buildingToConstruct.resourceCosts)
            {
                resourceCost.resource.quantity -= resourceCost.Count;
            }
            resourcesUpdated.Raise();

            buildingToConstruct = null;
        }
    }

    private void ConstructBuilding(NatureTile tile)
    {
        tile.ConstructOnTile(buildingToConstruct);
        GameObject buildingObj = Instantiate(buildingToConstruct.buildingPrefab, tile.transform);
        GameController.addNaturalTileWithBuilding(tile);
    }

    private void ConstructBuilding(Tile tile, Building building)
    {
        BuildingTile buildingObj = Instantiate(building.buildingPrefab, tile.transform.position, tile.transform.rotation).GetComponent<BuildingTile>();
        buildingObj.Building = building;
        GameController.addBuildingTile(buildingObj);
        GridManager.UpdateGridElement(buildingObj);
        Destroy(tile.gameObject);
    }
    
    private void AmeliorationsBuilding(BuildingTile buildingTile)
    {
        BuildingTile buildingObj = Instantiate(buildingTile.Building.Ameliorations[0].building.buildingPrefab, buildingTile.transform.position, buildingTile.transform.rotation).GetComponent<BuildingTile>();
        buildingObj.Building = buildingTile.Building.Ameliorations[0].building;
        GameController.addBuildingTile(buildingObj);
        GridManager.UpdateGridElement(buildingObj);
        Destroy(buildingTile.gameObject);
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
