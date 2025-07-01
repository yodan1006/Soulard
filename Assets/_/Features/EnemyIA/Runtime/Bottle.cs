using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace EnemyIa.Runtime
{
    public class Bottle : MonoBehaviour
    {
        public GameObject launcher;
        private Vector3 _moveDirection;

        public void InitDirection(Vector3 moveDirection)
        {
            _moveDirection = moveDirection.normalized;
        }

        private void Update()
        {
            RotationObject();
        }

        private void RotationObject()
        {
            _moveDirection.z -= 90f;
            transform.Rotate(_moveDirection, 720f * Time.deltaTime);
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
