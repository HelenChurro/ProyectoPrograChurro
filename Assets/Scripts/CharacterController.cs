using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Collider melee;



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

}