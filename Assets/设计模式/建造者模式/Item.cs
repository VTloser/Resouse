using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface Item
{
    string name();
    Packing packing();
    float Price();
}
