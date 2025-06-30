using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shooter.Runtime
{
    public class PoolMunition : MonoBehaviour
    {
        [Header("prefab bubble")]
        public GameObject AmmoPrefab;
        public int PoolSize = 100;

        private List<GameObject> bubblesPool;

        private void Awake()
        {
            bubblesPool = new List<GameObject>();

            for (int i = 0; i < PoolSize; i++)
            {
                GameObject bubble = Instantiate(AmmoPrefab);
                bubble.SetActive(false);
                bubblesPool.Add(bubble);
            }
        }

        public GameObject GetBubble()
        {
            foreach (var bubble in bubblesPool)
            {
                if (!bubble.activeInHierarchy)
                {
                    bubble.SetActive(true);
                    return bubble;
                }
            }
            GameObject newBubble = Instantiate(AmmoPrefab);
            newBubble.SetActive(true);
            bubblesPool.Add(newBubble);
            return newBubble;
        }

        public List<GameObject> GetAllBubbles()
        {
            return bubblesPool;
        }

        public void ReturnBubble(GameObject bubble)
        {
            bubble.SetActive(false);
        }
    }
}