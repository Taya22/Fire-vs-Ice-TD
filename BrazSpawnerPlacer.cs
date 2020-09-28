using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazSpawnerPlacer : MonoBehaviour
{
    [SerializeField] private Pathfinder BrazSpawner;
    private bool brazPlaced = false;

    public void PlaceBraz(Waypoint newBrazWaypoint)
    {
        if (!brazPlaced)
        {
            var newBraz = Instantiate(BrazSpawner, newBrazWaypoint.transform.position, Quaternion.identity);
            newBraz.searchCenter = newBrazWaypoint;
            newBraz.startWaypoint = newBrazWaypoint;
            newBrazWaypoint.isPlacable = false;

            brazPlaced = true;
        }
    }
}
