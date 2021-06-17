using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Grid grid;
    public GameObject scope;
    public void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int location = grid.WorldToCell(worldPosition);

        scope.transform.position = grid.CellToWorld(location) + new Vector3(0.15f, 0.15f, 0);
    }
}
