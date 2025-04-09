using Fusion;
using UnityEngine;

public class VRPlayerSetup : NetworkBehaviour
{
    [SerializeField] private MonoBehaviour[] scriptsToDisable;

    [SerializeField] private GameObject[] objectsToDisable; 

    public override void Spawned()
    {
        if (!HasStateAuthority)
        {
            foreach (var script in scriptsToDisable)
                script.enabled = false;

            foreach (var obj in objectsToDisable)
                obj.SetActive(false);
        }
    }

}
