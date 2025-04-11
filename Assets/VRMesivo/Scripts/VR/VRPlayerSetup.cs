using Fusion;
using UnityEngine;

public class VRPlayerSetup : NetworkBehaviour
{
    [SerializeField] private MonoBehaviour[] _scriptsToDisable;

    [SerializeField] private GameObject[] _objectsToDisable; 

    public override void Spawned()
    {
        if (!HasStateAuthority)
        {
            foreach (var script in _scriptsToDisable)
                script.enabled = false;

            foreach (var obj in _objectsToDisable)
                obj.SetActive(false);
        }
    }

}
