using UnityEngine;
using Fusion;

public class XRCameraFollower : MonoBehaviour
{
    private Transform playerToFollow;

    public void SetTarget(NetworkObject playerObject)
    {
        if (playerObject.HasInputAuthority)
        {
            // Parent XR Origin to the spawned player
            transform.SetParent(playerObject.transform);
            transform.localPosition = Vector3.zero; // Reset position relative to player
            transform.localRotation = Quaternion.identity;
        }
    }


    private void LateUpdate()
    {
        if (playerToFollow != null)
        {
            transform.position = new Vector3(playerToFollow.position.x, playerToFollow.position.y, playerToFollow.position.z);
        }
    }
}
