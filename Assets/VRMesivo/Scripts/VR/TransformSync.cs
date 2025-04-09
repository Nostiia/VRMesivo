using Fusion;
using UnityEngine;

public class TransformSync : NetworkBehaviour
{
    [Networked] private Vector3 NetworkedPosition { get; set; }
    [Networked] private Quaternion NetworkedRotation { get; set; }

    private Vector3 lastSentPosition;
    private Quaternion lastSentRotation;

    private void Update()
    {
        if (Object.HasInputAuthority)
        {
            Vector3 currentPos = transform.position;
            Quaternion currentRot = transform.rotation;

            if (Vector3.Distance(lastSentPosition, currentPos) > 0.01f ||
                Quaternion.Angle(lastSentRotation, currentRot) > 0.5f)
            {
                NetworkedPosition = currentPos;
                NetworkedRotation = currentRot;

                lastSentPosition = currentPos;
                lastSentRotation = currentRot;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, NetworkedPosition, Time.deltaTime * 15f);
            transform.rotation = Quaternion.Slerp(transform.rotation, NetworkedRotation, Time.deltaTime * 15f);
        }
    }

    public override void Spawned()
    {
        transform.position = NetworkedPosition;
        transform.rotation = NetworkedRotation;

        lastSentPosition = transform.position;
        lastSentRotation = transform.rotation;
    }
}
