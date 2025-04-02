using Fusion;
using UnityEngine;
using UnityEngine.AI;

public class MagazineManager : NetworkBehaviour
{
    [SerializeField] private GameObject _magazinePrefab;
    [SerializeField] private Transform[] _spawnZones;
    [SerializeField] private int _boxCount = 5;

    [Networked] public Vector3 Pos {  get; private set; }   

    public override void Spawned()
    {
        if (!Object.HasStateAuthority)
            return;

        for (int i = 0; i < _boxCount; i++)
        {
            if (_spawnZones.Length == 0) return;

            Transform spawnZone = _spawnZones[Random.Range(0, _spawnZones.Length)];
            Pos = spawnZone.position + new Vector3(
                Random.Range(-1f, 1f), 
                0.5f,
                Random.Range(-1f, 1f)
            );

            Runner.Spawn(_magazinePrefab, Pos, Quaternion.identity);
        }
    }

    public void DespawnedAndSpawned()
    {
        if (!Object.HasStateAuthority)
            return;
        ExtraMagazine[] magazines = FindObjectsOfType<ExtraMagazine>();

        for (int i = 0; i < magazines.Length; i++)
        {
            NetworkObject magazine = magazines[i].transform.GetComponent<NetworkObject>();
            Runner.Despawn(magazine);
        }

        Spawned();
    }
}