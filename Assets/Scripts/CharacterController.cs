using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Collider melee;
    [SerializeField] private float interactableRadius;



    private void Update()
    {
        Vector3 movementVector = Vector3.zero;

        //Direccion
        movementVector.x = Input.GetAxisRaw("Horizontal"); // -1 y 1
        movementVector.y = 0;
        movementVector.z = Input.GetAxisRaw("Vertical"); // -1 y 1

        characterController.Move(movementVector * Time.deltaTime * speed);

        //Transformar posicion de pantalla a posicion en el mundo
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Vector3 position = hitInfo.point;
            position.y = transform.position.y;
            transform.LookAt(position);
        }
        //Melee
        if (Input.GetMouseButtonDown(0))
        {
            melee.enabled = true;
            Invoke("NoMelee", 0.2f);

            Debug.Log("Melee");
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
    private void OnTriggerEnter(Collider other)
    {

        if (((1 << other.gameObject.layer & enemyLayer) != 0))
        {
            Debug.Log("Auch");
        }
    }
    void NoMelee()
    {
        melee.enabled = false;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward, interactableRadius);
    }

}
    