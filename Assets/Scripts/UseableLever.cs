using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableLever : Lever
{
    public void Use() // Jest tutaj wykonywany polimorfizm. Chc¹c skryptu Lever u¿yj tego nak³adaj¹c go nawybrany obiek by dzia³a³o.
    {
        Toogle();
    }
}
