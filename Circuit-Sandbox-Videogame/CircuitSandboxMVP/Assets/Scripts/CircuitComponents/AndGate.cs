using System.Linq;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AndGate : LogicGate
{
    public AndGate(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}

    // Checks inputs and returns true or false according to gate truth table
    public override bool CheckIns() {
        if (ins.Count == 2) {
            bool a = ins.First().on;
            bool b = ins.Skip(1).First().on;
            return a && b;
        }
        else {
            return false;
        }
    }
}