using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceholderBuffer : NotGate
{
    public PlaceholderBuffer(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}
    public override bool CheckIns() {
        return false;
    }
}
