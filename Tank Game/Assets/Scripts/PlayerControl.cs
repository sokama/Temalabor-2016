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

            GameObject tower = transform.FindChild("Graphics").FindChild("Tower").gameObject;

            if (tower == null) Debug.Log("tower is null");

            //Rigidbody rbTower = tower.GetComponent<Rigidbody>();

            //if (rbTower == null) Debug.Log("rbTower is null");

            //rbTower.MoveRotation(rotation);

            tower.transform.rotation = rotation;
            tower.transform.Rotate(new Vector3(-90f, 0f, 0f)); //ez azért kell, mert rossz a tank 3D modellje, és alapból -90 fokkal el kell forgatni az X tengely körül...
        }
    }
}
