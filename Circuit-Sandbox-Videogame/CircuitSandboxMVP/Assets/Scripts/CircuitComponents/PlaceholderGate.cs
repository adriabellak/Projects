using System.Linq;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceholderGate : LogicGate
{
    public PlaceholderGate(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}

    public override bool CheckIns() {
        return false;
    }
}