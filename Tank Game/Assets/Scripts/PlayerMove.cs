using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{

    //public float MovementSpeed;
    //public float RotationSpeed;

    Rigidbody rb;

    int raycastFloorMask;
    float raycastLength = 100f;

    public bool isControllable = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        raycastFloorMask = LayerMask.GetMask("RaycastFloor");
    }

    void FixedUpdate()
    {
        if (isControllable)
        {
            MovePlayer();
            RotateTower();
        }
    }

    void Update()
    {
        if (isControllable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (GetComponent<TankShoot>().CanShoot())
                {
                    GetComponent<TankShoot>().Shoot();
                }
            }
        }

    }

    private void MovePlayer()
    {
        transform.Rotate(Vector3.up * GetComponent<TankMovementParameters>().TankRotationSpeed * Input.GetAxisRaw("Horizontal") * Input.GetAxisRaw("Vertical") * Time.deltaTime);
        Vector3 newVelocity = transform.forward * GetComponent<TankMovementParameters>().TankMovementSpeed * Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, newVelocity.z);
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
