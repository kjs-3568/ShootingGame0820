using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Imovement
{
    void Move(Vector2 direction);

    void SetEnabled(bool newEnable);

}
