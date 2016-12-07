using UnityEngine;
using System.Collections;

public class HUDRotation : MonoBehaviour {

    public Transform mainCamera;

    public void Update()
    {
        transform.rotation = mainCamera.rotation;
    }
}
