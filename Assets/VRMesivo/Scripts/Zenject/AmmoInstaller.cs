using TMPro;
using UnityEngine;
using Zenject;

public class AmmoInstaller : MonoInstaller
{
    [SerializeField] private TMP_Text ammoText; // Assign in the Inspector
    [SerializeField] private PlayerAmmo playerAmmo; // Assign in the Inspector

    public override void InstallBindings()
    {
        Container.Bind<TMP_Text>().FromInstance(ammoText).AsSingle();
        Container.Inject(playerAmmo); // Manually inject dependencies into PlayerAmmo
    }
}
