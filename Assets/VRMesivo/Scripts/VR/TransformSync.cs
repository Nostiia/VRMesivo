using Fusion;
using UnityEngine;

public class TransformSync : NetworkBehaviour
{
    [Networked] public Vector3 NetworkedPosition { get; set; }
    [Networked] public Quaternion NetworkedRotation { get; set; }

    private Vector3 _lastSentPosition;
    private Quaternion _lastSentRotation;

    private void Update()
    {
        if (Object.HasInputAuthority)
        {
            Vector3 currentPos = transform.position;
            Quaternion currentRot = transform.rotation;

            if (Vector3.Distance(_lastSentPosition, currentPos) > 0.01f ||
                Quaternion.Angle(_lastSentRotation, currentRot) > 0.5f)
            {
                NetworkedPosition = currentPos;
                NetworkedRotation = currentRot;

                _lastSentPosition = currentPos;
                _lastSentRotation = currentRot;
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
        if (Object.HasInputAuthority)
        {
            NetworkedPosition = transform.position;
            NetworkedRotation = transform.rotation;
        }
        else
        {
            transform.position = NetworkedPosition;
            transform.rotation = NetworkedRotation;
        }

        _lastSentPosition = transform.position;
        _lastSentRotation = transform.rotation;
    }
}
