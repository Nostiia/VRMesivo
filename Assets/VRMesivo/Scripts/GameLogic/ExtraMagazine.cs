using Fusion;
using UnityEngine;

public class ExtraMagazine : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerCountMagazine magazineCount = other.gameObject.GetComponent<PlayerCountMagazine>();
        magazineCount.IncrementMagazine();

        Runner.Despawn(Object);
    }
}
