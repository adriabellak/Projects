using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Circuit
{
    public static Dictionary<Vector3Int, CircuitComponent> circuitComponents = new Dictionary<Vector3Int, CircuitComponent>();

    // public Circuit(Tilemap _tilemap)
    // {
    //     tilemap = _tilemap;
    //     circuitComponents = new Dictionary<Vector3Int, CircuitComponent>();
    // }
    public static void AddComponent(Vector3Int location, CircuitComponent component)
    {
        RemoveComponent(location);
        circuitComponents.Add(location, component);
        component.Connect();
    }

    // public void AddComponent(Vector3Int location, Type type){
    //     circuitComponents.Add(location, new CircuitComponent(location, this) as type);
    //     circuitComponents.Item[location].Connect();
    // }

    public static void RemoveComponent(Vector3Int location){
        if(circuitComponents.ContainsKey(location))
        {
            circuitComponents[location].Disconnect();
            circuitComponents.Remove(location);
        }
    }

    public static void ClearCricuit()
    {
        circuitComponents = new Dictionary<Vector3Int, CircuitComponent>();
    }
}