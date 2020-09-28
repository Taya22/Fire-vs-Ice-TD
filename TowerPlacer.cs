using System.Collections.Generic;
using UnityEngine;


public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private Tower Tower;
    [SerializeField] private int TowerLimit = 3;
    Queue<Tower> _towers = new Queue<Tower>();
    [SerializeField] private Transform TowerParent;

    public void CreateNewTower(Waypoint newBaseWaypoint)
    {
        if (FindObjectOfType<Pathfinder>().pathFired)
        {
            if (_towers.Count < TowerLimit)
            {
                var newTower = Instantiate(Tower, newBaseWaypoint.transform.position, Quaternion.identity);
                newTower.transform.parent = TowerParent;
                newTower.baseWaypoint = newBaseWaypoint;
                newBaseWaypoint.isPlacable = false;
            
                _towers.Enqueue(newTower);
            }
            else
            {
                var oldTower = _towers.Dequeue();
                oldTower.baseWaypoint.isPlacable = true;
                newBaseWaypoint.isPlacable = false;
                oldTower.baseWaypoint = newBaseWaypoint;

                oldTower.transform.position = newBaseWaypoint.transform.position;
            
                _towers.Enqueue(oldTower);
            }
        }
    }  
}
