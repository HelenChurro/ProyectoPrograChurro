using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 5f;

    [Header("Interaction & Combat")]
    [SerializeField] private float interactableRadius = 1.5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Weapon Inventory (SO)")]
    [SerializeField] private InventoryItemSO currentWeapon;
    [SerializeField] private InventoryItemSO primaryWeapon;
    [SerializeField] private InventoryItemSO secondaryWeapon;
    [Header("Resources")]
    [Header("Player Health")]

    [SerializeField] private int playerHealth = 100;

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Recibiste DAÑO Vida: " + playerHealth);

        if (playerHealth <= 0)
        {
            Debug.Log("¡Moyiste");
            Destroy(gameObject);
            // Aquí puedes reiniciar la escena o mostrar pantalla de Game Over
        }
    }
    public int totalResources = 0;

    private float attackTimer;

    private void Update()
    {
        Movement();

        Looking();

        CambioArma();

        MeleeAttack();

        Interactable();
    }
    public List<InventoryItemSO> inventory = new List<InventoryItemSO>();

    public void AddItem(InventoryItemSO item)
    {
        inventory.Add(item);
        Debug.Log("Obtuviste: " + item.itemName);
    }

    private void CambioArma()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentWeapon == primaryWeapon && secondaryWeapon != null)
            {
                currentWeapon = secondaryWeapon;
                Debug.Log("Cambiado a arma secundaria: " + currentWeapon.itemName);
            }
            else if (currentWeapon == secondaryWeapon && primaryWeapon != null)
            {
                currentWeapon = primaryWeapon;
                Debug.Log("Cambiado a arma primaria: " + currentWeapon.itemName);
            }
        }
    }
    public void AddResources(int amount)
    {
        totalResources += amount;
        Debug.Log("Tienes recursos: " + totalResources);
    }
    private void MeleeAttack()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (attackTimer > 0) return;


            float damageToDeal = currentWeapon != null ? currentWeapon.damage : 1f;
            float attackSpeed = currentWeapon != null ? currentWeapon.attackSpeed : 1f;

            // Melee
            Ray attackRay = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit hitInfo;

            Debug.DrawRay(attackRay.origin, attackRay.direction * attackRange, Color.red, 1f);

            if (Physics.Raycast(attackRay, out hitInfo, attackRange, enemyLayer))
            {

                IDanable damageableTarget = hitInfo.transform.GetComponentInParent<IDanable>();

                if (damageableTarget != null)
                {
                    damageableTarget.TakeDamage(Mathf.RoundToInt(damageToDeal));
                }

            }
            else
            {
                Debug.Log("No se pego nada");
            }

            attackTimer = attackSpeed;
        }
    }

    private void Interactable()
    {
        IInteractable closestInteractable = null;
        List<Collider> interactableColliders = Physics.OverlapSphere(transform.position + transform.forward, interactableRadius).ToList();
        float closestDistance = 1000f;

        if (interactableColliders.Count > 0)
        {
            foreach (Collider collider in interactableColliders)
            {
                if (collider.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    float distance = Vector3.Distance(collider.transform.position, transform.position);
                    if (distance < closestDistance)
                    {
                        closestInteractable = interactable;
                        closestDistance = distance;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            closestInteractable?.Interact();
        }
    }

    private void Looking()
    {
        // Transformar posición de pantalla a posición en el mundo
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Vector3 position = hitInfo.point;
            position.y = transform.position.y;
            transform.LookAt(position);
        }
    }

    private void Movement()
    {
        Vector3 movementVector = Vector3.zero;
        movementVector.x = Input.GetAxisRaw("Horizontal"); // -1 y 1
        movementVector.z = Input.GetAxisRaw("Vertical"); // -1 y 1

        characterController.Move(movementVector.normalized * Time.deltaTime * speed);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.forward, interactableRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * attackRange);
    }
}
