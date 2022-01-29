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

    Tile hoverTile;
    PlayerInputs inputs;
    Mouse mouse;

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


    private void OnMousePressed()
    {
        if(hoverTile != null && hoverTile.IsBuildable() && buildingToConstruct != null && buildingToConstruct.type == hoverTile.type)
        {
            ConstructBuilding((NatureTile)hoverTile);
        }
        else
        {
            Debug.Log("Nothing to construct");
        }
    }

    private void ConstructBuilding(NatureTile tile)
    {
        Debug.Log("On va construire " + buildingToConstruct.name + " sur une tile " + tile.Ressource.name);
        tile.ConstructOnTile(buildingToConstruct);
        
        GameObject buildingobj = Instantiate(buildingToConstruct.buildingPrefab, tile.transform.position, tile.transform.rotation);
        
        buildingToConstruct = null;
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
