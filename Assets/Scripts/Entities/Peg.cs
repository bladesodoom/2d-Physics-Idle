using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Peg : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer sr;
    public float currencyValue = 1;

    public int pegID;
    public int pegLevel = 1;
    public float pegCurrentXP = 0;
    public float pegXPNextLevel = 10;

    public float pegXPGainMultiplier = 1f;
    public float pegLevelScaler = 1.25f;
    public float pegCurrentXPValue = 1;

    public float pegUpgradeCost = 10;
    public float pegProductionBoost = 1;

    public PegData ToData()
    {
        return new PegData
        {
            id = pegID,
            pegPosition = transform.position,
            level = pegLevel,
            upgradeCost = pegUpgradeCost,
            value = currencyValue,
            currentXP = pegCurrentXP,
            xpNextLevel = pegXPNextLevel,
            xpGainMultiplier = pegXPGainMultiplier,
            levelScaler = pegLevelScaler,
            currentXPValue = pegCurrentXPValue,
            productionBoost = pegProductionBoost,
        };
    }

    public void FromData(PegData data)
    {
        pegID = data.id;
        transform.position = data.pegPosition;
        pegLevel = data.level;
        pegUpgradeCost = data.upgradeCost;
        currencyValue = data.value;
        pegCurrentXP = data.currentXP;
        pegXPNextLevel = data.xpNextLevel;
        pegXPGainMultiplier = data.xpGainMultiplier;
        pegLevelScaler = data.levelScaler;
        pegCurrentXPValue = data.currentXPValue;
        pegProductionBoost = data.productionBoost;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        PegManager.Instance.SelectPeg(this);
    }


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void GainXP()
    {
        pegCurrentXP += pegXPGainMultiplier * pegCurrentXPValue;
        CheckLevelUp();
    }

    public bool TryUpgrade()
    {
        if (CurrencyManager.Instance.TrySpend(pegUpgradeCost))
        {
            pegLevel++;
            pegUpgradeCost *= 1.5f;
            pegProductionBoost += 0.1f;
            pegXPGainMultiplier += 0.05f;
            currencyValue += 5;
            return true;
        }
        return false;
    }

    public void ResetPeg()
    {
        currencyValue = 1;
        pegCurrentXP = 0;
        pegXPNextLevel = 10;
        pegXPGainMultiplier = 1f;
        pegCurrentXPValue = 1;
    }

    private void CheckLevelUp()
    {
        if (pegCurrentXP >= pegXPNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        pegLevel++;
        pegXPNextLevel *= pegLevelScaler;
    }
}
