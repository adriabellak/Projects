using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NotGate : CircuitComponent
{

    public NotGate(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}

    // cambiar de izquierda a derecha nada mas
    public override void Connect(){
        List<Vector3Int> possibleNeighbors = new List<Vector3Int>();
        
        Vector3Int rightNeighbor = location + new Vector3Int(1, 0, 0);
        possibleNeighbors.Add(rightNeighbor);
        
        Vector3Int leftNeighbor = location + new Vector3Int(-1, 0, 0);
        possibleNeighbors.Add(leftNeighbor);

        Vector3Int twoStepsLeftNeighbor = location + new Vector3Int(-2, 0, 0);

        if (Circuit.circuitComponents.TryGetValue(twoStepsLeftNeighbor, out CircuitComponent gate) && (gate is LogicGate)) {
            ins.Add(gate);
            gate.outs.Add(this);
        }
        
        foreach(Vector3Int neighborLocation in possibleNeighbors){
            if (Circuit.circuitComponents.TryGetValue(neighborLocation, out CircuitComponent neighbor)) {
                if (neighborLocation == rightNeighbor && (neighbor is OutputComponent || neighbor is LogicGate)){
                    outs.Add(neighbor);
                    neighbor.ins.Add(this);
                }
                else if (neighbor is InputComponent && neighborLocation == leftNeighbor){
                    ins.Add(neighbor);
                    neighbor.outs.Add(this);
                }
                else if (neighbor is Wire) {
                    if (neighborLocation == leftNeighbor) {
                        ins.Add(neighbor);
                        neighbor.outs.Add(this);
                    }
                    else if (neighborLocation == rightNeighbor) {
                        outs.Add(neighbor);
                        neighbor.ins.Add(this);
                    }
                }
            }
        }

        Turn(CheckIns());
    }

    public override bool CheckIns() {
        if (ins.Count == 0) {
            return true;
        }
        else {
            return !ins.First().on;
        }
    }
}