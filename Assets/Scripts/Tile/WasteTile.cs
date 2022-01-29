using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteTile : Tile
{
    public override bool IsBuildable()
    {
        return false;
    }

    public override bool IsNature()
    {
        return false;
    }

    public override string ModalInfos()
    {
        return "";
    }
}
