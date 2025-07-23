using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    private float baseCooldownTime = 0.5f;
    private float cooldownTime;
    private bool canFire = true;
    public Transform bulletParent;

    [Header("Orbit Settings")]
    [SerializeField] private float orbitDistance = 0.7f;
    [SerializeField] private float rotationSmoothness = 10f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask wallLayer;
    private Camera mainCam;

    [Header("Power Ups")]
    public PowerUpsManager powerUpsManager;

    private WeaponSounds weaponSounds;

    private void Start()
    {
        mainCam = Camera.main;
        cooldownTime = baseCooldownTime;
        weaponSounds = gameObject.GetComponent<WeaponSounds>();

        if (powerUpsManager == null)
        {
            Debug.LogError("PowerUpsManager não encontrado no GameObject.");
        }

        // Posição inicial da arma
        if (playerTransform)
        {
            transform.position = playerTransform.position + Vector3.up * orbitDistance;
        }
    }

    private void Update()
    {
        UpdateCooldown();
        RotateAndOrbit();

        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    private void UpdateCooldown()
    {
        if (powerUpsManager == null) return;

        switch (powerUpsManager.getItem())
        {
            case 1:  // Power-up de velocidade
                cooldownTime = baseCooldownTime / 5f;
                break;
            case 3:  // Power-up de explosão
                cooldownTime = baseCooldownTime * 2f;
                break;
            default:
                cooldownTime = baseCooldownTime;
                break;
        }
    }

    private void RotateAndOrbit()
    {
        if (!playerTransform) return;

        // Obtém posição do mouse no mundo
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        // Calcula direção e ângulo
        Vector2 direction = (mouseWorldPos - playerTransform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Suavização da rotação
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSmoothness * Time.deltaTime
        );

        // Verifica colisão com paredes
        float adjustedDistance = orbitDistance;
        RaycastHit2D hit = Physics2D.Raycast(
            playerTransform.position,
            direction,
            orbitDistance,
            wallLayer
        );

        if (hit.collider != null)
        {
            adjustedDistance = hit.distance * 0.9f; // Reduz um pouco para garantir
        }

        // Mantém a órbita ao redor do jogador
        transform.position = playerTransform.position + (Vector3)direction * adjustedDistance;
    }

    public void Fire()
    {
        if (!canFire) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation, bulletParent);

        // Aplica efeitos de power-up
        if (powerUpsManager != null && powerUpsManager.getItem() == 3)
        {
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.explosionBullet = true;
            }
        }

        // Dispara a bala
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Rigidbody2D não encontrado no bulletPrefab.");
        }

        StartCoroutine(FireCooldown());
    }

    private IEnumerator FireCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(cooldownTime);
        canFire = true;
    }

    // Debug visual para verificar o raycast
    private void OnDrawGizmos()
    {
        if (playerTransform)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(playerTransform.position, transform.position);
        }
    }
}
