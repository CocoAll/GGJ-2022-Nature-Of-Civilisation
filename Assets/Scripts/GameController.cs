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
    private SignalSender updateResourcesSignal;

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
