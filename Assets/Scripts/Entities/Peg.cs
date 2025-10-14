using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Peg : MonoBehaviour
{
    public SpriteRenderer sr;
    public float currencyValue = 1;

    private int level = 1;
    private float currentXP = 0;
    private float xpNextLevel = 10;

    private float xpGainMultiplier = 1f;
    private float levelScaler = 1.25f;
    private float currentXPValue = 1;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void GainXP()
    {
        currentXP += xpGainMultiplier * currentXPValue;
        CheckLevelUp();
    }

    public void UpgradeXPMultiplier()
    {
        xpGainMultiplier += 0.05f;
    }

    public void ResetPeg()
    {
        currencyValue = 1;
        currentXP = 0;
        xpNextLevel = 10;
        xpGainMultiplier = 1f;
        currentXPValue = 1;
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        xpNextLevel *= levelScaler;
    }
}
