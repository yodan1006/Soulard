using Movement.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Runtime
{
    public class Shoot : MonoBehaviour
    {

        #region Publics
        
                
        
                #endregion
        
        
                #region Unity Api
        
                private void Start()
                {
                    
                }
        
                private void Update()
                {
                    if (health.m_currentHealth >= bonus2)
                    {
                        missileCount = 3;
                    }
                    else if (health.m_currentHealth >= bonus1 && health.m_currentHealth < bonus2)
                    {
                        missileCount = 2;
                    }
                    else
                    {
                        missileCount = 1;
                    }
                }
        
                #endregion
        
        
                #region Utils

                public void Tir(InputAction.CallbackContext context)
                {
                    if (context.performed)
                    Bubbleshoot();
                }
                
                private void Bubbleshoot()
                    {
                         float angleStep = spreadAngle / (missileCount); 
                         float startAngle = -spreadAngle / 2;
                
                         if (missileCount > 1)
                         {
                             for (int i = 0; i < missileCount; i++)
                             {
                                 GameObject bubble = poolMunition.GetBubble(); // Récupère une nouvelle bulle pour chaque tir
                                 Rigidbody rb = bubble.GetComponent<Rigidbody>();
                                 if (bubble != null)
                                 {
                                     bubble.transform.position = shootPoint.position;
                        
                                     if (rb != null)
                                     {
                                         rb.linearVelocity = Vector3.zero; // Réinitialise la vitesse
                                         float angle = startAngle + angleStep * i;
                                         Vector3 dir = Quaternion.Euler(0, angle, 0) * shootPoint.forward;
                                         rb.AddForce(dir * shootForce, ForceMode.Impulse);
                                     }
                                 }
                             }
                        }
                        else
                        {
                            GameObject bubble = poolMunition.GetBubble();
                            Rigidbody rb = bubble.GetComponent<Rigidbody>();
                            bubble.transform.position = shootPoint.position;
                            rb.linearVelocity = Vector3.zero;
                            Vector3 dir = shootPoint.forward;
                            rb.AddForce(dir * shootForce, ForceMode.Impulse);
                        }
                    }
        
                #endregion
        
        
                #region Main Methode
        
                
        
                #endregion
                
                
                #region Privates
                
                [SerializeField] private int spreadAngle;
                [SerializeField] private int missileCount;
                [SerializeField] private PoolMunition poolMunition;
                [SerializeField] private Transform shootPoint;
                [SerializeField] private float shootForce;
                [SerializeField] private int bonus1;
                [SerializeField] private int bonus2;

                [SerializeField] private PlayerStat health;

                #endregion
    }
}
