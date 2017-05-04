using UnityEngine;
using System.Collections;

namespace Assets.Classes.Effects
{
    public abstract class InstantEffect
    {
        public abstract void Affect(GameObject target);
    }
}