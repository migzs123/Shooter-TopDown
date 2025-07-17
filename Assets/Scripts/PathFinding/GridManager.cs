using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [Header("Tilemaps")]
    public Tilemap groundTilemap;
    public Tilemap wallTilemap;

    // Matriz para armazenar nossos nodes
    private Node[,] grid;

    private int width, height;
    private Vector2Int offset;

    private void Awake()
    {
        createGrid();
    }

    private void createGrid()
    {
        // pega os limites da área ocupada pelo groundTilemap
        BoundsInt bounds = groundTilemap.cellBounds;
        width = bounds.size.x;
        height = bounds.size.y;
        // offset para converter coordenadas de célula em índice [0..width)
        offset = new Vector2Int(-bounds.xMin, -bounds.yMin);

        grid = new Node[width, height];

        // varre todas as células
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector2Int gridPos = new Vector2Int(x + offset.x, y + offset.y);
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                // se houver um tile de parede nessa célula, não é caminhável
                bool isWalkable = wallTilemap.GetTile(cellPos) == null;

                // cria o node na posição de índice [gridPos.x, gridPos.y]
                grid[gridPos.x, gridPos.y] = new Node(gridPos, isWalkable);
            }
        }
    }

    public List<Node> getNeighbors(Node node) {
        List<Vector2Int> offsets = new List<Vector2Int> { 
            new Vector2Int(0,+1),
            new Vector2Int(0,-1),
            new Vector2Int(+1,0),
            new Vector2Int(-1,0),
            new Vector2Int(+1,+1),
            new Vector2Int(-1,+1),
            new Vector2Int(+1,-1),
            new Vector2Int(-1,-1)
        };

        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int offset in offsets)
        {
            Vector2Int neighboorPos = new Vector2Int(node.getGridPos.x + offset.x, node.getGridPos.y + offset.y);
            if(neighboorPos.x >=0 && neighboorPos.y >= 0)
            {
                if (neighboorPos.x < width && neighboorPos.y < height) {
                    Node result = grid[neighboorPos.x, neighboorPos.y];
                    if (result.getIsWalkable)
                    {
                        neighbors.Add(result);
                    }
                }
            }
        }

        return neighbors;
    }

    public void ResetAllNodes()
    {
        if (grid == null) return; 

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y].reset(); 
                }
            }
        }
    }

    public Node nodeFromWorldPosition(Vector2 worldPos)
    {
        Vector3Int cell = groundTilemap.WorldToCell(worldPos);
        Vector2Int gridPos = new Vector2Int(cell.x + offset.x, cell.y + offset.y);
        // cuidado com limites!
        if (gridPos.x >= 0 && gridPos.x < width && gridPos.y >= 0 && gridPos.y < height)
            return grid[gridPos.x, gridPos.y];
        return null;
    }

    public Vector2 NodeToWorldPosition(Node node)
    {
        Vector3Int cellPos = new Vector3Int(node.getGridPos.x - offset.x, node.getGridPos.y - offset.y, 0);
        Vector3 worldPosition = groundTilemap.CellToWorld(cellPos);
        return worldPosition + groundTilemap.cellSize / 2;
    }
}
