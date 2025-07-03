// using System;
// using Shooter.Runtime;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
// namespace Movement.Runtime
// {
//     public class ShootPlayer : MonoBehaviour
//     {
//         #region Publics
//         
//                 
//         
//         #endregion
//         
//         
//         #region Unity Api
//
//         private void Awake()
//         {
//             _healthPlayer = GetComponent<PlayerStat>();
//         }
//
//         private void Update()
//         {
//             NumberAmmo();
//         }
//
//         #endregion
//         
//         
//         #region Utils
//
//         public void Tir(InputAction.CallbackContext context)
//         {
//             if (context.performed)
//                 Bubbleshoot();
//         }
//                 
//         private void Bubbleshoot()
//         {
//             float angleStep = spreadAngle / (missileCount); 
//             float startAngle = -spreadAngle / 2;
//                 
//             if (missileCount > 1)
//             {
//                 for (int i = 0; i < missileCount; i++)
//                 {
//                     GameObject bubble = poolMunition.GetBubble(); // Récupère une nouvelle bulle pour chaque tir
//                     Rigidbody rb = bubble.GetComponent<Rigidbody>();
//                     if (bubble != null)
//                     {
//                         bubble.transform.position = shootPoint.position;
//                         
//                         if (rb != null)
//                         {
//                             rb.linearVelocity = Vector3.zero; // Réinitialise la vitesse
//                             float angle = startAngle + angleStep * i;
//                             Vector3 dir = Quaternion.Euler(0, angle, 0) * shootPoint.forward;
//                             rb.AddForce(dir * shootForce, ForceMode.Impulse);
//                         }
//                     }
//                 }
//             }
//             else
//             {
//                 GameObject bubble = poolMunition.GetBubble();
//                 Rigidbody rb = bubble.GetComponent<Rigidbody>();
//                 bubble.transform.position = shootPoint.position;
//                 rb.linearVelocity = Vector3.zero;
//                 Vector3 dir = shootPoint.forward; 
//                 rb.AddForce(dir * shootForce, ForceMode.Impulse); 
//             }
//         }
//
//         private void NumberAmmo()
//         {
//             if (_healthPlayer.m_currentHealth <= _lifeModification[0]) missileCount = 1;
//             if (_healthPlayer.m_currentHealth >= _lifeModification[0] && _healthPlayer.m_currentHealth <= _lifeModification[1]) missileCount = 2;
//             if (_healthPlayer.m_currentHealth >= _lifeModification[1]) missileCount = 3;
//         }
//         
//         #endregion
//         
//         
//         #region Main Methode
//         
//                 
//         
//         #endregion
//                 
//                 
//         #region Privates
//                 
//         [SerializeField] private int spreadAngle;
//         [SerializeField] private int[] _lifeModification;
//         [SerializeField] private PoolMunition poolMunition; 
//         [SerializeField] private Transform shootPoint;
//         [SerializeField] private float shootForce;
//
//         private PlayerStat _healthPlayer;
//         private int missileCount;
//
//         #endregion
//     }
// }
