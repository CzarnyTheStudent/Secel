using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableLever : Lever
{
    public void Use() // Jest tutaj wykonywany polimorfizm. Chc�c skryptu Lever u�yj tego nak�adaj�c go nawybrany obiek by dzia�a�o.
    {
        Toogle();
    }
}
