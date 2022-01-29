using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public TypeTile type;
    
    public abstract bool IsNature();

    public abstract bool IsBuildable();

    public abstract string ModalInfos();
}
