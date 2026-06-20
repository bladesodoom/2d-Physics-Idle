using UnityEngine;

public class BallManager : UpgradeManager<BallManager>
{
    public double ballValue { get; private set; }
    public float ballSize { get; private set; }
    public int ballMaxQuantity { get; private set; }

    public int ActiveBallCount { get; private set; }
    public bool CanSpawnBall => ActiveBallCount < ballMaxQuantity;

    protected override void InitializeStats()
    {
        ballValue = base.SaveData.ballValue;
        ballSize = base.SaveData.ballSize;
        ballMaxQuantity = base.SaveData.ballMaxQuantity;
    }

    public void RegisterBall()
    {
        ActiveBallCount++;
    }

    public void UnregisterBall()
    {
        ActiveBallCount = Mathf.Max(0, ActiveBallCount - 1);
    }

    public void UpgradeValue()
    {
        ballValue *= 1.1f;
        WriteToSave();
    }

    public void UpgradeSize()
    {
        ballSize *= 0.9f;
        WriteToSave();
    }

    public void UpgradeMaxQuantity()
    {
        ballMaxQuantity += 5;
        WriteToSave();
    }

    protected override void WriteToSave()
    {
        base.SaveData.ballValue = ballValue;
        base.SaveData.ballSize = ballSize;
        base.SaveData.ballMaxQuantity = ballMaxQuantity;
    }
}
