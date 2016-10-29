using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    public float MovementSpeed;
    public float RotationSpeed;

    Rigidbody rb;

    int raycastFloorMask;
    float raycastLength = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        raycastFloorMask = LayerMask.GetMask("RaycastFloor");
    }

    void Update()
    {
        MovePlayer();
        RotateTower();
    }

    private void MovePlayer()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Input.GetAxisRaw("Horizontal") * Input.GetAxisRaw("Vertical") * Time.deltaTime);
        rb.velocity = transform.forward * MovementSpeed * Input.GetAxisRaw("Vertical");
    }

    private void RotateTower()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastFloorHit;
        if(Physics.Raycast(camRay, out raycastFloorHit, raycastLength, raycastFloorMask))
        {
            Vector3 fromPlayerToMouse = raycastFloorHit.point - transform.position;
            fromPlayerToMouse.y = 0f;

            Quaternion rotation = Quaternion.LookRotation(fromPlayerToMouse);

            Transform tower = transform.FindChild("Graphics").FindChild("Tower");

            tower.rotation = rotation;
        }
    }
}
