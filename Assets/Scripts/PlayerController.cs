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

    private void OnMousePressed()
    {
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
        else
        {
            Debug.Log("Nothing to construct");
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

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
