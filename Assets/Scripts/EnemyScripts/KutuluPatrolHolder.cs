using System;
using UnityEngine;

namespace EnemyScripts
{
    public class KutuluPatrolHolder : MonoBehaviour
    {
        private void Start()
        {
            transform.parent = null;
        }
    }
}
