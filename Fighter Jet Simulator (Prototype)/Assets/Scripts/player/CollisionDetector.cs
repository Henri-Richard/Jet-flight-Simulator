using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] GameObject f18;

    void OnTriggerEnter(Collider other)
    {
        f18.GetComponent<PlayerMovements>().destroy(f18);
    }
}
