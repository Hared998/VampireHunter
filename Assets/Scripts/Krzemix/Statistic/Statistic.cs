using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatName
{
    Helth=0,
    Stanima,
    Armor,
    Speed,
    Damage,
    RangeDamage,
    RangeHear,
    RangeDoST,
    Cooldown
}

public enum OperationName
{
    Add=0,
    Sub,
    Replace,
    Multiplier
}

[System.Serializable]
public class Statistic
{
    [SerializeField]
    public string statisticName { get; private set; }
    public StatName statisticId;
    public double actualPoint { get; private set; }
    public double basePoint = 0;
    public double bonusPoint { get; private set; }
    public double minPoint = 0;
    public double maxPoint { get; private set; }

    public Statistic(StatName statName, double basePoint, double minPoint)
    {
        this.statisticId = statName;
        this.basePoint = basePoint;
        this.minPoint = minPoint;
        setFirstStartSetting();
    }

    public void setMaxPoint()
    {
        this.maxPoint = this.basePoint + this.bonusPoint;
    }
    public void setStatisticName()
    {
        this.statisticName = statisticId.ToString();
    }
    public void setBonusPoint(double point)
    {
        bonusPoint = point;
        if(statisticId == StatName.Damage)
        {
            setActualPoint(maxPoint);
        }
    }

    public void setActualPoint(double point)
    {
        changeActualPoint(OperationName.Replace, point);
    }
    public void changeActualPoint(OperationName operationName, double point)
    {
        if (operationName == OperationName.Add)
        {
            if (actualPoint + point > maxPoint)
                actualPoint = maxPoint;
            else if (actualPoint + point <= minPoint)
                actualPoint = minPoint;
            else
                actualPoint += point;
        }
        else if (operationName == OperationName.Sub)
        {
            if (actualPoint - point < minPoint)
            {
                actualPoint = minPoint;
            }
            else
            {
                actualPoint -= point;
            }
        }
        else if (operationName == OperationName.Replace && point <= maxPoint && point >= minPoint)
            actualPoint = point;
        else if (operationName == OperationName.Multiplier)
        {
            if (actualPoint * point > maxPoint)
                actualPoint = maxPoint;
            else if (actualPoint * point < minPoint)
                actualPoint = minPoint;
            else
                actualPoint = actualPoint * point;
        }
    }
    public void setFirstStartSetting()
    {
        setStatisticName();
        setMaxPoint();
        setActualPoint(basePoint);
    }
}
