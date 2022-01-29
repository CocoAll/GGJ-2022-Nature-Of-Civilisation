using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int tileSize = 5;
    [SerializeField]
    private List<Tile> tileList;
    private Dictionary<Vector2, Tile> grid;
    [SerializeField]
    private bool isAutoGenerated = false;

    private void Awake()
    {
        grid = new Dictionary<Vector2, Tile>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.isAutoGenerated)
        {

        }
        else if(tileList != null && tileList.Count > 0)
        {
            this.GenerateGridFromExistingTiles();
        }
    }

    private void GenerateGridFromExistingTiles()
    {
        foreach (Tile tile in this.tileList)
        {
            Vector2 tilePosition = new Vector2(tile.transform.position.x / tileSize, tile.transform.position.z / tileSize);
            grid.Add(tilePosition, tile);
            //Debug.Log(tilePosition);
        }
    }
}
