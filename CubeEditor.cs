using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{

    private Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    
    void Update()
    {
        //SnapToGrid();
        UpdateLable();
    }

    // private void SnapToGrid()
    // {
    //     int gridSize = waypoint.GetGridRange();
    //     transform.position = new Vector3(
    //         waypoint.GetGridPos().x * gridSize, 
    //         0f, 
    //         waypoint.GetGridPos().y * gridSize
    //         );
    // }

    private void UpdateLable()
    {
        string lableText = waypoint.GetGridPos().x + "," + waypoint.GetGridPos().y;
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = lableText;
        gameObject.name = lableText;
    }
}
