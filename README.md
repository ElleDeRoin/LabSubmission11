# Lab Submission 11
Group Members: Elle DeRoin, Cami Alarcon-Fernandez, Lauren Holloway

Video: https://youtu.be/dAl6RmhHjIw?si=sxITU_TH7ZUE_8WY

How does each pathfinding algorithms calculate and prioritize paths?  

BFS works on unweighted grids and uses a queue to search in layers. Every neighbor of the start first, then their neighbors, and so on until the goal is reached. The only priority is first-in, first-out, and since every step costs the same, the first time it reaches the goal is also the shortest path with the fewest steps. Dijkstra's algorithm is like BFS for weighted graphs. It always expands the node with the lowest total cost from the start, g(n), so it finds the cheapest path, but it also spreads out in every direction and spends time exploring away from the goal. A* is Dijkstra's algorithm adapted to include heuristics, so it prioritizes by f(n) = g(n) + h(n), where h(n) is an estimate of the remaining distance. Given that this estimate doesn't overestimate the distance, A* still finds the most optimal path while exploring less nodes. 

What challenges arise when dynamically updating obstacles in real-time?  

The main problem is that the current path can become invalid when an obstacle is put on it, making it so we have to rerun FindPath again after AddObstacle. We also have to clear the old path list first, so that the gizmos stop drawing the leftover cells. A new obstacle can also block the goal completely, so the code has to handle the "Path not found" case without breaking, and we need to make sure obstacles can't be placed on the start or goal cell. Also, rerunning a full search every time something changes can get expensive on big grids.

Which algorithm should you choose and how should you adapt it for larger grids or open-world settings?

When it comes to most types of games, A* would probably be best since it's fast and still finds the shortest path. BFS is fine for small unweighted grids and Dijkstra's is better for when there's no single known goal, like finding the nearest item. For larger grids or open worlds, A* can be sped up with Jump Point Search, which skips unnecessary nodes on uniform grids, or with hierarchical pathfinding, in which a general route is planned using the abstract version of the map first and then details are filled in. Using a NavMesh is another option, as it uses far fewer nodes than a tile grid.

What would your approach be if you were to add weighted cells (e.g., "difficult terrain" areas)?  

BFS can't handle weights because it treats every step as equal, so it would therefore need to be swapped for Dijkstra's or A*. Each cell would store a movement cost instead of just 0 or 1 and the queue would become a priority queue that always picks the cheapest option first. A neighbor only gets updated if the new route to it costs less than the one already found. The gizmos could also be colored by terrain type so the difficult areas are easy to see.
