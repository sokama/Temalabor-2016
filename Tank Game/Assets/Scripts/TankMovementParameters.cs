using UnityEngine;
using System.Collections;

public class TankMovementParameters : MonoBehaviour {
    public float defaultTankMovementSpeed = 7;
    public float defaultTankRotationSpeed = 100;

    public float TankMovementSpeed { get; set; }
    public float TankRotationSpeed { get; set; }

    //public void TemporarilySetTankMovementSpeed(float tempValue, float time)
    //{
    //    float oldValue = tankMovementSpeed;
    //    tankMovementSpeed = tempValue;
    //    StartCoroutine(SetTankMovementSpeedAfterDelay(oldValue, time));
    //}

    //private IEnumerator SetTankMovementSpeedAfterDelay(float newValue, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    tankMovementSpeed = newValue;
    //}

    //public void TemporarilySetTankRotationSpeed(float tempValue, float time)
    //{
    //    float oldValue = tankRotationSpeed;
    //    tankRotationSpeed = tempValue;
    //    StartCoroutine(SetTankRotationSpeedAfterDelay(oldValue, time));
    //}

    //private IEnumerator SetTankRotationSpeedAfterDelay(float newValue, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    tankRotationSpeed = newValue;
    //}

    public void ResetTankMovementSpeed()
    {
        TankMovementSpeed = defaultTankMovementSpeed;
    }

    public void ResetTankRotationSpeed()
    {
        TankRotationSpeed = defaultTankRotationSpeed;
    }
}
