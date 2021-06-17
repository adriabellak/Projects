using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wire : CircuitComponent
{

    public Wire(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}

    public override void Connect(){
        List<Vector3Int> possibleNeighbors = new List<Vector3Int>();

        Vector3Int upperNeighbor = location + new Vector3Int(0, 1, 0);
        possibleNeighbors.Add(upperNeighbor);

        Vector3Int lowerNeighbor = location + new Vector3Int(0, -1, 0);
        possibleNeighbors.Add(lowerNeighbor);
        
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
                if ((neighbor is OutputComponent || neighbor is LogicGate) || (neighbor is NotGate && neighborLocation == rightNeighbor)){
                    outs.Add(neighbor);
                    neighbor.ins.Add(this);
                }
                else if ((neighbor is InputComponent) || (neighbor is NotGate && neighborLocation == leftNeighbor)){
                    ins.Add(neighbor);
                    neighbor.outs.Add(this);
                }
                else if (neighbor is Wire) {
                    outs.Add(neighbor);
                    neighbor.outs.Add(this);
                    ins.Add(neighbor);
                    neighbor.ins.Add(this);
                }
            }
        }
        Turn(CheckIns());
    }

    // returns true if at least one input is turned on
    public override bool CheckIns() {
        foreach(CircuitComponent neighbor in ins) {
            if (neighbor.on) {
                return true;
            }
        }
        // if no inputs were on, return false
        return false;
    }
}