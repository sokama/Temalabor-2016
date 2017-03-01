using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {

    public Collider wholeCollider;
    public GameObject whole;
    public GameObject pieces;

    public float disappearTime = 5f;

    public float explosionForce = 1;
    public float explosionRadius = 3;

    public void Destruct()
    {
        wholeCollider.enabled = false;
        whole.SetActive(false);
        pieces.SetActive(true);

        foreach (Transform child in pieces.transform)
        {
            child.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, whole.transform.position, explosionRadius, 0, ForceMode.Impulse);
        }

        Destroy(gameObject, disappearTime);
    }

}
