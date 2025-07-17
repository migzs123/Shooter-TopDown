using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private GridManager grid;

    public PathFinder()
    {
        this.grid = GameObject.Find("Grid").GetComponent<GridManager>();
    }

    public List<Node> FindPath(Vector2 startWorldPos, Vector2 targetWorldPos) {

        Node targetNode = grid.nodeFromWorldPosition(targetWorldPos);

        List<Node> openList = new List<Node>
        {
            grid.nodeFromWorldPosition(startWorldPos)
        };

        openList[0].setGCost(0);

        List<Node> closedList = new List<Node>();
        Node lowerNode = null;


        while (openList.Count > 0) {
            int minFCost = int.MaxValue;
            foreach (Node node in openList) {
                if (node.getFCost() < minFCost)
                {
                    minFCost = node.getFCost();
                    lowerNode = node;
                }
            }

            if (lowerNode == null)
            { 
                return new List<Node>();
            }

            openList.Remove(lowerNode);
            closedList.Add(lowerNode);

            if (lowerNode == targetNode)
            {
                return RebuildPath(targetNode);
            }

            foreach (Node neighbor in grid.getNeighbors(lowerNode)) {
                if (closedList.Contains(neighbor))
                    continue;

                int dx = Mathf.Abs(neighbor.getGridPos.x - lowerNode.getGridPos.x);
                int dy = Mathf.Abs(neighbor.getGridPos.y - lowerNode.getGridPos.y);
                int moveCost = (dx + dy == 2) ? 14 : 10;
                int tentativeGCost = lowerNode.getGCost + moveCost;

                if (tentativeGCost < neighbor.getGCost || !openList.Contains(neighbor))
                {
                    neighbor.calculateCosts(lowerNode, moveCost, targetNode);
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }
        return new List<Node>();

    }

    private List<Node> RebuildPath(Node finalNode)
    {
        List<Node> caminho = new List<Node>();
        Node atual = finalNode;

        while (atual != null)
        {
            caminho.Add(atual);
            atual = atual.parent; 
        }

        caminho.Reverse(); 
        return caminho;
    }
}
