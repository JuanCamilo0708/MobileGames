using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Upgrades", menuName = "Scriptable Objects/Upgrades")]
public class Upgrades : ScriptableObject
{
    [Serializable]
    public class Upgrade
    {
        public string name;
        public Sprite sprite;
        public long cost;
        public long amount;

        public UpgradeType type;
    }

    public List<Upgrade> upgrades = new();
}

public enum UpgradeType
{
    Click,
    Idle
}
