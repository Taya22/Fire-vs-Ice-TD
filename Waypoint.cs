using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    const  int gridSize = 10;
    private Vector2Int gridPos;
    public Waypoint exploredFrom;
    public bool isPlacable = true;

    private void Start()
    {
        isPlacable = true;
        transform.Find("WaypointFire").gameObject.SetActive(false);
    }

    // public int GetGridRange()
    // {
    //     return gridSize;
    // }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacable)
            {
                FindObjectOfType<TowerPlacer>().CreateNewTower(this);
            }
        }
        else if(Input.GetMouseButton(1))
        {
            if (isPlacable)
            {
                FindObjectOfType<BrazSpawnerPlacer>().PlaceBraz(this);
            }
        }
    } 
}
