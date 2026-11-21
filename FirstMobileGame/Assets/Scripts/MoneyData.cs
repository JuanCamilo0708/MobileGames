using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoneyData", menuName = "Scriptable Objects/MoneyData")]
public class MoneyData : ScriptableObject
{
    [Serializable]
    public class MoneyPack
    {
        public string name;
        public string price;
        public long pointsGranted;
        public int sushiAmount;
    }

    public List<MoneyPack> packs = new();
}
