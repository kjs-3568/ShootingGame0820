using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iweapon
{
    void SetOwner(GameObject newOwner);
    void Fire();

    void SetEnabled(bool enabled);
   
}
