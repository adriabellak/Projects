using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class BufferGate : NotGate
{
    public BufferGate(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}
    public override bool CheckIns() {
        if (ins.Count == 0) {
            return false;
        }
        else {
            return ins.First().on;
        }
    }
}
