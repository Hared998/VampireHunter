using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[CreateAssetMenu(fileName = "NewEfect", menuName = "Statistic/Efect")]
public class Efect : ScriptableObject
{
    //[SerializeField]
    public string efectName = "efect name";
    public StatName typeStatistic = StatName.Armor;
    public bool isMultipler = false;
    public double valueEfect = 0;

    public Efect()
    {

    }
    public Efect(string efectName, StatName typeStatistic, bool isMultipler, double valueEfect)
    {
        this.efectName = efectName;
        this.typeStatistic = typeStatistic;
        this.isMultipler = isMultipler;
        this.valueEfect = valueEfect;
    }
}
