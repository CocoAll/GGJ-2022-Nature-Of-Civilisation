using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool isGameOver;
    [SerializeField]
    private float timeBetweenResourcesUpdate = 1.5f;

    private static List<NatureTile> naturalTilesWithBuilding;
    private static List<BuildingTile> buildingTiles;
    [SerializeField]
    private static List<Resource> resources;

    [SerializeField]
    private SignalSender updateResourcesSignal;

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
            updateResourcesSignal.Raise();

        } while (!isGameOver);
    }
}
