using System.Collections.Generic;
using UnityEngine;
public class Pathfinding : MonoBehaviour
{
    private List<Vector3Int> path = new List<Vector3Int>();
    public Vector3Int start = new Vector3Int(0, 1);
    public Vector3Int goal = new Vector3Int(4, 4);
    private Vector3Int next;
    private Vector3Int current;
    private Vector3Int[] directions = new Vector3Int[]
    {
        new Vector3Int(1, 0),
        new Vector3Int(-1, 0),
        new Vector3Int(0, 1),
        new Vector3Int(0, -1)
    };
    private int[,] grid = new int[,]
    {
        { 0, 1, 0, 0, 0 },
        { 0, 1, 0, 1, 0 },
        { 0, 0, 0, 1, 0 },
        { 0, 1, 1, 1, 0 },
        { 0, 0, 0, 0, 0 }
    };
    private void Start()
    {
        
        FindPath(start, goal);
    }
    private void OnDrawGizmos()
    {
        float cellSize = 1f;
        // Draw grid cells
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, 0, y * cellSize);
                Gizmos.color = grid[y, x] == 1 ? Color.black : Color.white;
                Gizmos.DrawCube(cellPosition, new Vector3(cellSize, 0.1f,
                cellSize));
            }
        }
        // Draw path
        foreach (var step in path)
        {
            Vector3 cellPosition = new Vector3(step.x * cellSize, 0, step.y *
            cellSize);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(cellPosition, new Vector3(cellSize, 0.1f, cellSize));
        }
        // Draw start and goal
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(start.x * cellSize, 0, start.y * cellSize), new
        Vector3(cellSize, 0.1f, cellSize));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(goal.x * cellSize, 0, goal.y * cellSize), new
        Vector3(cellSize, 0.1f, cellSize));
    }
    private bool IsInBounds(Vector3Int point)
    {
        return point.x >= 0 && point.x < grid.GetLength(1) && point.y >= 0 &&
        point.y < grid.GetLength(0);
    }
    private void FindPath(Vector3Int start, Vector3Int goal)
    {
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        frontier.Enqueue(start);
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int,
        Vector3Int>();
        cameFrom[start] = start;
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            if (current == goal)
            {
                break;
            }
            foreach (Vector3Int direction in directions)
            {
                next = current + direction;
                if (IsInBounds(next) && grid[next.y, next.x] == 0 && !
                cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }
        if (!cameFrom.ContainsKey(goal))
        {
            Debug.Log("Path not found.");
            return;
        }
        // Trace path from goal to start
        Vector3Int step = goal;
        while (step != start)
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Add(start);
        path.Reverse();
    }
}
