using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Classes.Effects
{
    class DestroyEffect : InstantEffect
    {
        public override void Affect(GameObject target)
        {
            if (target.GetComponent<Destructible>() != null)
            {
                target.GetComponent<Destructible>().Destruct();
                Map.notifyWallDestroyed(target.transform.position);
            }
        }
    }
}
