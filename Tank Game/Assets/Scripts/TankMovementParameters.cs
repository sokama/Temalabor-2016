using UnityEngine;
using System.Collections;

public class TankMovementParameters : MonoBehaviour {
    public float tankMovementSpeed = 7;
    public float tankRotationSpeed = 100;

    public void TemporarilySetTankMovementSpeed(float tempValue, float time)
    {
        float oldValue = tankMovementSpeed;
        tankMovementSpeed = tempValue;
        StartCoroutine(SetTankMovementSpeedAfterDelay(oldValue, time));
    }

    private IEnumerator SetTankMovementSpeedAfterDelay(float newValue, float delay)
    {
        yield return new WaitForSeconds(delay);
        tankMovementSpeed = newValue;
    }

    public void TemporarilySetTankRotationSpeed(float tempValue, float time)
    {
        float oldValue = tankRotationSpeed;
        tankRotationSpeed = tempValue;
        StartCoroutine(SetTankRotationSpeedAfterDelay(oldValue, time));
    }

    private IEnumerator SetTankRotationSpeedAfterDelay(float newValue, float delay)
    {
        yield return new WaitForSeconds(delay);
        tankRotationSpeed = newValue;
    }
}
