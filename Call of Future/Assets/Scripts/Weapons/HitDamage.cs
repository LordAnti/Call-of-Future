using UnityEngine;

public class HitDamage : MonoBehaviour
{
    public string Area;
    public PHealth ph;
    public void AddDamage(float damage, string typeDamage, string typeFall)
    {
        switch (Area) //области получаемые урон
        {
            case "head": //голова
                ph.AddDamage(damage * 10f, typeDamage, typeFall);
                break;
            case "waist": //талия
                ph.AddDamage(damage * 1.5f, typeDamage, typeFall);
                break;
            case "stomach": //живот
                ph.AddDamage(damage * 1.8f, typeDamage, typeFall);
                break;
            case "chestdown": //нижняя часть груди
                ph.AddDamage(damage * 1.8f, typeDamage, typeFall);
                break;
            case "chestup": //вверхня часть груди
                ph.AddDamage(damage * 2f, typeDamage, typeFall);
                break;
            case "upperarm": //руки/ноги
            case "arm":
            case "upperleg":
            case "leg":
                ph.AddDamage(damage * 1f, typeDamage, typeFall);
                break;
            case "hand": //кисти/лодыжки
            case "foot":
                ph.AddDamage(damage * 0.35f, typeDamage, typeFall);
                break;
        }
    }
}
