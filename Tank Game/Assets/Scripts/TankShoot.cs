using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TankShoot : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject bullet1Icon;
    public GameObject bullet2Icon;
    public GameObject bullet3Icon;

    public float bulletSpeed = 10f;
    public int numberOfBullets = 3;
    public float reloadTime = 3f;

    private List<float> reloadTimes = new List<float>();
    private List<GameObject> bulletIcons = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < numberOfBullets; i++)
        {
            reloadTimes.Add(0f);
        }

        bulletIcons.Add(bullet1Icon);
        bulletIcons.Add(bullet2Icon);
        bulletIcons.Add(bullet3Icon);
    }

    void Update()
    {
        UpdateReloadTimes(Time.deltaTime);
        UpdateBulletIcons(); 
    }

    private void UpdateBulletIcons()
    {
        for(int i = 0; i < numberOfBullets; i++)
        {
            if (reloadTimes[i] <= 0f)
                bulletIcons[i].SetActive(true);
            else
                bulletIcons[i].SetActive(false);
        }
    }

    private void StartReload()
    {
        for(int i = 0; i < numberOfBullets; i++)
        {
            if (reloadTimes[i] <= 0f)
            {
                reloadTimes[i] = reloadTime;
                return;
            }
        }
    }

    public bool CanShoot()
    {
        foreach(float rt in reloadTimes)
        {
            if (rt <= 0f)
                return true;
        }
        return false;
    }

    private void UpdateReloadTimes(float deltaTime)
    {
        for(int i = 0; i < numberOfBullets; i++)
        {
            if(reloadTimes[i] > 0f)
            {
                reloadTimes[i] -= deltaTime;
                if (reloadTimes[i] < 0f)
                    reloadTimes[i] = 0f;
            }
        }
    }

    public void Shoot()
    {
        Transform shootingPoint = transform.FindChild("Graphics").FindChild("Tower").FindChild("ShootingPoint");
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);

        Vector3 bulletVelocity = bullet.transform.forward * bulletSpeed;
        bullet.GetComponent<Rigidbody>().velocity = bulletVelocity;

        StartReload();
    }
}
