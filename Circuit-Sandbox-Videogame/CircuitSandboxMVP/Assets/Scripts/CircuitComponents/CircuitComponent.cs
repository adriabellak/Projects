using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class CircuitComponent
{
    public ITilemap tilemap;
    public Vector3Int location;
    public bool on;
    public HashSet<CircuitComponent> ins;
    public HashSet<CircuitComponent> outs;

    public CircuitComponent(Vector3Int _location, ITilemap _tilemap){
        location = _location;
        on = false;
        ins = new HashSet<CircuitComponent>();
        outs = new HashSet<CircuitComponent>();
        tilemap = _tilemap;
    }

    public virtual void Connect(){}

    public virtual bool CheckIns(){return true;}

    // Obeys status parameter
    public void Turn(bool status) {
        on = status;
        foreach(CircuitComponent outp in outs) {
            if (outp is Wire && (outp.on != status)) {
                outp.Turn(status);
                tilemap.RefreshTile(outp.location);
            }
            else if ((outp is LogicGate) || (outp is NotGate) || (outp is OutputComponent)) {
                outp.Turn(outp.CheckIns());
                tilemap.RefreshTile(outp.location);
            }
        }
    }

    // disconnect() que tenga bfs
    public void Disconnect() {
        // Debug.Log("Disconnect at " + location);
        foreach(CircuitComponent inp in ins) {
            inp.outs.Remove(this);
            tilemap.RefreshTile(inp.location);
        }
        foreach(CircuitComponent outp in outs) {
            outp.ins.Remove(this);
            if (outp.on) {
                //Debug.Log("BFS at " + outp.location);
                outp.BFS();
            }
            tilemap.RefreshTile(outp.location);
        }
    }

    public void BFS()
    {
        int maxIterations = 100;

        HashSet<CircuitComponent> visited = new HashSet<CircuitComponent>();
        HashSet<CircuitComponent> gates = new HashSet<CircuitComponent>();
        Queue<CircuitComponent> componentQueue = new Queue<CircuitComponent>();
        CircuitComponent currentComponent = this;
        componentQueue.Enqueue(currentComponent);
        bool foundInput = false;

        while(componentQueue.Count != 0 && maxIterations > 0)
        {
            maxIterations --;

            currentComponent = componentQueue.Dequeue();
            visited.Add(currentComponent);
            // si encuentra un input prendido
            if (currentComponent is InputComponent && currentComponent.on) {
                foundInput = true;
            }
            else if(currentComponent is LogicGate || currentComponent is NotGate)
            {
                gates.Add(currentComponent);
            }
            else
            {
                // cambie de ins a outs
                foreach(CircuitComponent neighbor in currentComponent.ins){
                    if(!visited.Contains(neighbor))
                    {
                        componentQueue.Enqueue(neighbor);
                    }
                }
                
                foreach (CircuitComponent neighbor in currentComponent.outs)
                {
                if(!visited.Contains(neighbor) && (neighbor is LogicGate || neighbor is NotGate || neighbor is OutputComponent)) {
                    gates.Add(currentComponent);
                }
                }
            }
        }

        foreach (CircuitComponent component in visited) {
            component.on = foundInput;
            tilemap.RefreshTile(component.location);
        }
        foreach (CircuitComponent gate in gates) {
            gate.Turn(gate.CheckIns());
        }
    }
}