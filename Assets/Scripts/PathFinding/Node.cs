using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Vector2Int gridPos;
    private bool isWalkable;
    private int gCost, hCost;
    public Node parent;


    public Node(Vector2Int gridPos, bool isWalkable)
    {
        this.gridPos = gridPos;
        this.isWalkable = isWalkable;
        this.gCost = 0; 
        this.hCost = 0;
        this.parent = null;

    }

    public Vector2Int getGridPos {  get { return gridPos; } }

    public int getGCost { get => gCost; }
    public int getHCost { get => hCost; }

    public bool getIsWalkable { get => isWalkable; }

    public int getFCost() { 
        return gCost + hCost;
    }

    public void calculateCosts(Node parent, int movementCost, Node target) { 
        this.gCost = parent.getGCost + movementCost;
        this.hCost = heuristic(this, target);
        this.parent = parent;
    }

    public int heuristic(Node node, Node target)
    {
        int dx = Mathf.Abs(node.getGridPos.x - target.getGridPos.x);
        int dy = Mathf.Abs(node.getGridPos.y - target.getGridPos.y);
        return dx + dy;
    }

    public void reset()
    {
        this.gCost = 0;
        this.hCost = 0;
        this.parent = null;
    }

    public void setParent(Node node)
    {
       this.parent = node; 
    }

    public void setGCost(int cost)
    {
        this.gCost = cost;
    }
}
