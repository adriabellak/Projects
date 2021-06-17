using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputComponent : CircuitComponent
{

    public InputComponent(Vector3Int _location, ITilemap _tilemap, bool _on) : base(_location, _tilemap)
    {
        on = _on;
    }

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
        
        foreach(Vector3Int neighborLocation in possibleNeighbors){
            if (Circuit.circuitComponents.TryGetValue(neighborLocation, out CircuitComponent neighbor)) {
                outs.Add(neighbor);
                neighbor.ins.Add(this);
                // if (neighbor is Output || neighbor is LogicGate){
                //     outs.Add(neighbor);
                //     neighbor.ins.Add(this);
                // }
                // // no se si tomar en cuenta que puedan tener inputs como vecinos
                // // else if (neighbor is Input){
                // //     ins.Add(neighbor);
                // //     neighbor.outs.Add(this);
                // // }
                // else if (neighbor is Wire) {
                //     outs.Add(neighbor);
                //     neighbor.ins.Add(this);
                // }
            }
        }

        // turn checkins o turn on??
        // Turn(CheckIns());
        Turn(on);
    }

    // tal vez no es necesario checkins para inputs
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