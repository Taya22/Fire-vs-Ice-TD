using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public bool pathFired;
    [SerializeField] public Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    private bool isRunning = true;
    public Waypoint searchCenter;
    private List<Waypoint> path = new List<Waypoint>();

    private void Start()
    {
        pathFired = false;
        var endPoint = GameObject.Find("EndWaypoint").GetComponent<Waypoint>();
        endWaypoint = endPoint;
        if (path.Count == 0)
        { 
            LoadBlocks();
            BreathFirstSearch();
            CreatePath();
            StartCoroutine(FirePathRoutine());
        }
    }

    public List<Waypoint> Getpath ()
    {
        return path;
    }

    private Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    private void CreatePath()
    {
        path.Add(endWaypoint);
        endWaypoint.isPlacable = false;

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous.isPlacable = false;
            previous = previous.exploredFrom;
        }
        path.Add(startWaypoint);
        startWaypoint.isPlacable = false;
        path.Reverse();
    }

    private void BreathFirstSearch()
    {
        queue.Enqueue(startWaypoint);
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            IfSearchingFromStart();
            ExploreNeighbours();
        }
    }

    private void IfSearchingFromStart()
    {
        if (searchCenter == endWaypoint)
        {
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoord = searchCenter.GetGridPos() + direction;

            if (grid.ContainsKey(neighbourCoord))
            {
                QueueNewNeighbour(neighbourCoord);
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourCoord)
    {
        Waypoint neighbour = grid[neighbourCoord];
        if (neighbour.isExplored || queue.Contains(neighbour))
        {
            //do nothing
        }
        else
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block" + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }
    IEnumerator FirePathRoutine()
    {
        foreach (var waypoint in path)
        {
            waypoint.transform.Find("WaypointFire").gameObject.SetActive(true);
            yield return new WaitForSeconds(.25f);
        }
    
        pathFired = true;
    }
}
