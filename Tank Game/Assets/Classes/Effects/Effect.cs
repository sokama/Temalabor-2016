using UnityEngine;
using System.Collections;

namespace Assets.Classes.Effects
{
    public abstract class Effect : MonoBehaviour
    {
        public abstract void Affect(GameObject target);
    }
}