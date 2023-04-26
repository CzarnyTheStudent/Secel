using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableLever : Lever
{
    public void Use() // Jest tutaj wykonywany dziedziczenie. Chcąc skryptu Lever użyj tego nakładając go nawybrany obiek by działało.
    {
        Toogle();
    }
}
