using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    public float MovementSpeed;
    public float RotationSpeed;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Input.GetAxis("Horizontal") * Input.GetAxis("Vertical") * Time.deltaTime);
        rb.velocity = transform.forward * MovementSpeed * Input.GetAxis("Vertical");
    }
}
