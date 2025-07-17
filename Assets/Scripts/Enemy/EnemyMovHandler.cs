using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovHandler : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed =2f;
    [SerializeField] private GridManager gridManager;
    private PathFinder pathFinder;

    private List<Node> currentPath; 
    private int currentPathIndex;   
    private Node targetNode;
    private Vector2 lastTargetPosition;
    private float positionChangeThreshold = 0.5f;

    [Header("Configurações de Pathfinding")]
    [SerializeField] private float pathUpdateInterval = 0.5f;
    private float pathUpdateTimer; 


    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        pathFinder = new PathFinder();
        if (gridManager == null)
        {
            gridManager = FindObjectOfType<GridManager>();
            if (gridManager == null)
            {
                Debug.LogError("GridManager não encontrado! O inimigo não se moverá.");
                enabled = false;
                return;
            }
        }
        pathUpdateTimer = pathUpdateInterval;
    }

    void Update()
    {
        if (player == null || gridManager == null) return;

        pathUpdateTimer -= Time.deltaTime;
        if (pathUpdateTimer <= 0f)
        {
            RequestNewPath();
            pathUpdateTimer = pathUpdateInterval; 
        }

        if (currentPath != null && currentPath.Count > 0 && currentPathIndex < currentPath.Count)
        {
            targetNode = currentPath[currentPathIndex];

            Vector3 worldTargetPos = gridManager.NodeToWorldPosition(targetNode);

            transform.position = Vector2.MoveTowards(transform.position, worldTargetPos, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, worldTargetPos) < 0.2f)
            {
                currentPathIndex++; 

                if (currentPathIndex >= currentPath.Count)
                {
                    Debug.Log("Inimigo chegou ao final do caminho calculado.");
                    currentPath = null; 
                }
            }
        }
    }

    void RequestNewPath()
    {
        if (Vector2.Distance(player.position, lastTargetPosition) > positionChangeThreshold)
        {
            gridManager.ResetAllNodes();

            Node targetNode = gridManager.nodeFromWorldPosition(player.position);
            if (targetNode != null)
            {
                Vector2 fixedTarget = gridManager.NodeToWorldPosition(targetNode);
                currentPath = pathFinder.FindPath(transform.position, fixedTarget);

                if (currentPath != null && currentPath.Count > 0)
                    currentPathIndex = 0;
                else
                    currentPath = null;

                lastTargetPosition = player.position;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (currentPath != null && currentPath.Count > 0 && gridManager != null)
        {
            Gizmos.color = Color.red; 
            for (int i = 0; i < currentPath.Count - 1; i++)
            {
                Vector3 start = gridManager.NodeToWorldPosition(currentPath[i]);
                Vector3 end = gridManager.NodeToWorldPosition(currentPath[i + 1]);
                Gizmos.DrawLine(start, end);
                Gizmos.DrawSphere(start, 0.15f); 
            }

            Gizmos.DrawSphere(gridManager.NodeToWorldPosition(currentPath[currentPath.Count - 1]), 0.15f);
        }

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
}
