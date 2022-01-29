using System.Linq;
using UnityEngine;

public class BonusGetter : MonoBehaviour
{
    [SerializeField] 
    private BonusDatasTemplate _template;
    [SerializeField] 
    private BonusDatas _datas;

    public BaseBonus GetBonus()
    {
        var bonusDatas = _template != null ? _template.datas : _datas;
        
        var randomValue = Random.Range(0, bonusDatas.datas.Sum(x => x.weight));
        var weightSum = 0f;
        foreach (var data in bonusDatas.datas)
        {
            weightSum += data.weight;

            if (weightSum >= randomValue)
            {
                return data.BaseBonus;
            }
        }

        return null;
    }
}
