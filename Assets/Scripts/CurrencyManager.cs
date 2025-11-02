using UnityEngine;

public class CurrencyManager : Manager<CurrencyManager>
{
    [SerializeField] private float money;
    [SerializeField] private float singularityPoints;
    [SerializeField] private float ascensionTokens;
    [SerializeField] private float evolutionEssence;

    public enum CurrencyType
    {
        M,
        SP,
        AT,
        EE
    }

    public void Initialize()
    {
        money = 0;
        singularityPoints = 0;
        ascensionTokens = 0;
        evolutionEssence = 0;
    }

    public void AddCurrency(float amount, CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.SP:
                singularityPoints += amount;
                break;
            case CurrencyType.AT:
                ascensionTokens += amount;
                break;
            case CurrencyType.EE:
                evolutionEssence += amount;
                break;
            case CurrencyType.M:
            default:
                money += amount;
                break;
        }
    }
}
