using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdaptiveAttack
{
    [RequireComponent(typeof(AttackEditor))]
    public class Player : MonoBehaviour
    {
        AttackEditor attackEditor;
        
        void Start() 
        {
            attackEditor = GetComponent<AttackEditor>();
        }

        void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                OnAttack();
            }    
        }

        void OnAttack()
        {
           attackEditor.Attack();
        }
    }
}
