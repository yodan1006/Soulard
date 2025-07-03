using UnityEngine;

public class TropBu : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 2f;
    
    

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position =  transform.right * offset;
    }
}
