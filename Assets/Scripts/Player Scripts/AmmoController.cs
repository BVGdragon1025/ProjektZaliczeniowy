using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{

    public void ChangeAmmo(AmmoHolder ammoHolder, int amount)
    {
        if (ammoHolder.ammoCount < ammoHolder.maxAmmoCount)
        {
            ammoHolder.ammoCount = Mathf.Clamp(ammoHolder.ammoCount + amount, 0, ammoHolder.maxAmmoCount);
        }

    }
}
