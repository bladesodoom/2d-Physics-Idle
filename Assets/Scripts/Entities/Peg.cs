using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Peg : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer sr;
    public Color damagedColor = Color.red;
    public float damageFlashTime = 0.1f;

    public int pegID;

    public int pegLevel = 1;
    public float pegCurrentXP = 0;
    public float pegXPNextLevel = 10;
    public float pegXPGainMultiplier = 1f;
    public float pegLevelScaler = 1.25f;
    public float pegCurrentXPValue = 1;
    public float pegUpgradeCost = 10;

    public float basePegValue = 1;
    public float pegValue = 1;

    public float baseHP = 50;
    public float maxHP = 50;
    public float currentHP;

    public float respawnDelay = 10f;
    public bool isRespawning = false;


    public PegData ToData()
    {
        return new PegData
        {
            id = pegID,
            pegPosition = transform.position,
            level = pegLevel,
            upgradeCost = pegUpgradeCost,
            value = pegValue,
            currentXP = pegCurrentXP,
            xpNextLevel = pegXPNextLevel,
            xpGainMultiplier = pegXPGainMultiplier,
            levelScaler = pegLevelScaler,
            currentXPValue = pegCurrentXPValue,
        };
    }

    public void FromData(PegData data)
    {
        pegID = data.id;
        transform.position = data.pegPosition;
        pegLevel = data.level;
        pegUpgradeCost = data.upgradeCost;
        pegValue = data.value;
        pegCurrentXP = data.currentXP;
        pegXPNextLevel = data.xpNextLevel;
        pegXPGainMultiplier = data.xpGainMultiplier;
        pegLevelScaler = data.levelScaler;
        pegCurrentXPValue = data.currentXPValue;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        PegManager.Instance.SelectPeg(this);
    }


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        if (isRespawning) return;

        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashDamage());
        }
    }

    private IEnumerator FlashDamage()
    {
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = damagedColor;
            yield return new WaitForSeconds(damageFlashTime);
            sr.color = original;
        }
    }

    private void Die()
    {
        if (isRespawning) return;

        isRespawning = true;
        StopAllCoroutines();
        sr.enabled = false;

        PegManager.Instance.ScheduleRespawn(this, respawnDelay);
        gameObject.SetActive(false);
    }

    private void RespawnPeg()
    {
        ResetPeg();
        sr.color = Color.white;
        gameObject.SetActive(true);
        isRespawning = false;
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
            pegXPGainMultiplier += 0.05f;
            pegValue++;
            return true;
        }
        return false;
    }

    public void ResetPeg()
    {
        currentHP = baseHP;
        pegValue = basePegValue;
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
