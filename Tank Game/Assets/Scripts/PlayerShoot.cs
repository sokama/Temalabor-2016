using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

    public GameObject Bullet;
    public float BulletSpeed = 10f;
    private int wasShootIn = 0;
    void Update()
    {
        wasShootIn++;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (wasShootIn >= 30)
            {
                Shoot();
                wasShootIn = 0;
            }
        }
        
    }

    private void Shoot()
    {
        Transform shootingPoint = transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        GameObject bullet = (GameObject)Instantiate(Bullet, shootingPoint.position, shootingPoint.rotation);

        Vector3 bulletVelocity = bullet.transform.forward * BulletSpeed;
        bullet.GetComponent<Rigidbody>().velocity = bulletVelocity;
    }
}
