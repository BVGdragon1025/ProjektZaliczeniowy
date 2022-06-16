using UnityEngine;

[CreateAssetMenu(fileName = "Ammo Holder", menuName = "Data Containers/Ammo Holder", order = 1)]

public class AmmoHolder : ScriptableObject
{
    public int maxAmmoCount;
    public int ammoCount;
}
