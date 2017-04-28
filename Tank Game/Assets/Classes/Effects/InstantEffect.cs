using UnityEngine;
using System.Collections;

namespace Assets.Classes.Effects
{
    public abstract class InstantEffect : MonoBehaviour
    {
        public abstract void Affect(GameObject target);
    }
}