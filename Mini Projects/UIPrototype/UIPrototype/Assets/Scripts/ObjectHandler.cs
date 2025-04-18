using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(other.gameObject);
    }
}
