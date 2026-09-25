using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask enemyLayer;

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

        //Disparar
        //
        if (Input.GetMouseButtonDown(0))
        {
            Ray fireRay = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit enemyInfo;

            Debug.DrawRay(fireRay.origin, fireRay.direction * 10, Color.green, 10f);

            if (Physics.Raycast(fireRay.origin, fireRay.direction, out enemyInfo, 100f, enemyLayer))
            {
                Debug.Log(enemyInfo.transform.gameObject.name);
            }
            else
            {
                Debug.Log("No rec");
            }
        }
    }
}
