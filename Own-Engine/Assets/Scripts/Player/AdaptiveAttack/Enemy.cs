using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdaptiveAttack
{
    public class Enemy : MonoBehaviour
    {
        public void OnHit()
        {
            Debug.Log($"{this.name} : Hit");
        }
    }
}
