using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class LogicGate : CircuitComponent
{
    
    public TileBase previousTile = null;
    // cablecitos dobles
    public LogicGate(Vector3Int _location, ITilemap _tilemap) : base(_location, _tilemap) {}

    //connect two to the right
    public override void Connect(){
        List<Vector3Int> possibleNeighbors = new List<Vector3Int>();

        Vector3Int upperNeighbor = location + new Vector3Int(0, 1, 0);
        possibleNeighbors.Add(upperNeighbor);

        Vector3Int lowerNeighbor = location + new Vector3Int(0, -1, 0);
        possibleNeighbors.Add(lowerNeighbor);
        
        Vector3Int leftNeighbor = location + new Vector3Int(-1, 0, 0);
        possibleNeighbors.Add(leftNeighbor);

        Vector3Int twoStepsLeftNeighbor = location + new Vector3Int(-2, 0, 0);

        if (Circuit.circuitComponents.TryGetValue(twoStepsLeftNeighbor, out CircuitComponent gate) && (gate is LogicGate)) {
            ins.Add(gate);
            gate.outs.Add(this);
        }
        
        foreach(Vector3Int neighbors in possibleNeighbors){
            if (Circuit.circuitComponents.TryGetValue(neighbors, out CircuitComponent neighboring)) {
                ins.Add(neighboring);
                neighboring.outs.Add(this);
            }
        }

        Vector3Int twoStepsRightNeighbor = location + new Vector3Int(2, 0, 0);
        if (Circuit.circuitComponents.TryGetValue(twoStepsRightNeighbor, out CircuitComponent neighbor)) {
            outs.Add(neighbor);
            neighbor.ins.Add(this);
        }

        Turn(CheckIns());
    }
}