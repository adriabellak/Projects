using System.Linq;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OrGate : LogicGate
{
    public OrGate(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}

    // Checks inputs and returns true or false according to gate truth table
    public override bool CheckIns() {
        if (ins.Count == 2) {
            bool a = ins.First().on;
            bool b = ins.Skip(1).First().on;
            return a || b;
        }
        else if (ins.Count == 1){
            bool a = ins.First().on;
            return a;
        }
        else
        {
            return false;
        }
    }
}