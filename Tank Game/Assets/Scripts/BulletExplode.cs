using UnityEngine;
using System.Collections;
using System;

public class BulletExplode : MonoBehaviour {

    public float Damage = 10f;
    public string PlayerTag = "Player";
    public string EnemyTag = "Enemy";

    public GameObject ExplosionParticles;

    void OnTriggerEnter(Collider col)
    {
        //if (col.tag == PlayerTag || col.tag == EnemyTag)
        //{
        //    Hit(col.gameObject);
        //}

        if(col.GetComponent<Health>() != null)
        {
            col.GetComponent<Health>().DecreaseHealth(Damage);
        }

        if(col.GetComponent<Destructible>() != null)
        {
            Debug.Log("Destruct");
            col.GetComponent<Destructible>().Destruct();
        }

        Explode();
    }

    //private void Hit(GameObject hitTarget)
    //{
    //    Debug.Log("HIT");
    //}

    private void Explode()
    {
        Quaternion explosionRotation = new Quaternion();
        explosionRotation.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z);
        GameObject explosion = (GameObject) Instantiate(ExplosionParticles, transform.position, explosionRotation);
        Destroy(explosion, 0.5f);

        Destroy(gameObject);
        return;
    }
}
