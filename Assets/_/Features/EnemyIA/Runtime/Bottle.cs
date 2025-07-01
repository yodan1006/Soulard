using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace EnemyIa.Runtime
{
    public class Bottle : MonoBehaviour
    {
        public GameObject launcher;

        private void Update()
        {
            RotationObject();
        }

        private void RotationObject()
        {
            Quaternion.Euler(0,0,180 * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            
            IAEnemy enemy = other.gameObject.GetComponent<IAEnemy>();
            if (enemy != null)
            {
                enemy.SetTarget(launcher);
            }
        }
    }
}
