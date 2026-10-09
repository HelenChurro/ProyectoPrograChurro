using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private float interactableRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int melee;

    [Header("Weapon Information")]
    //[SerializeField] private WeaponSO currentWeapon;
    //[SerializeField] private WeaponSO primaryWeapon;
    //[SerializeField] private WeaponSO secondaryWeapon;

    [SerializeField] private int primaryBullet;
    [SerializeField] private float shootTimer;

    private void Update()
    {
        Movement();

        Looking();

        Interactable();
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
                    if (Vector3.Distance(collider.transform.position, transform.position) < closestDistance)
                    {
                        closestInteractable = interactable;
                        closestDistance = Vector3.Distance(collider.transform.position, transform.position);
                    }
                }
            }
        }
        else
        {
            closestInteractable = null;
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            // "?" checa si es null
            closestInteractable?.Interact();
        }
    }

    private void Looking()
    {
        //Transformar posicion de pantalla a posicion en el mundo
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

        //Direccion
        movementVector.x = Input.GetAxisRaw("Horizontal"); // -1 y 1
        movementVector.y = 0;
        movementVector.z = Input.GetAxisRaw("Vertical"); // -1 y 1

        characterController.Move(movementVector * Time.deltaTime * speed);
    }


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward, interactableRadius);
    }

    
}
