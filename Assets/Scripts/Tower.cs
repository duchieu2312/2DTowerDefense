using System;
using UnityEngine;

[Serializable]
public class Tower
{
    public string towername;
    public int cost;
    public GameObject prefab;

    public Tower (string _towername, int _cost, GameObject _prefab)
    {
        towername = _towername;
        cost = _cost;
        prefab = _prefab;
    }
}
