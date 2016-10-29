using UnityEngine;
using System.Collections;
using System;

public class BulletExplode : MonoBehaviour {

    public float Damage = 10f;
    public string PlayerTag = "Player";
    public string EnemyTag = "Enemy";

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

        Explode();
    }

    //private void Hit(GameObject hitTarget)
    //{
    //    Debug.Log("HIT");
    //}

    private void Explode()
    {
        Destroy(gameObject);
        return;
    }
}
