using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class Peg : MonoBehaviour, IPointerClickHandler
{
    public SpriteRenderer sr;
    public float damageFlashTime = 0.05f;

    public int pegID;

    public float basePegValue = 1;
    public float pegValue = 1;

    public float baseHP = 50;
    public float maxHP = 50;
    public float currentHP;

    public float respawnDelay = 10f;
    public bool isRespawning = false;

    public Color defaultColor;
    private Coroutine flashRoutine;

    public event Action OnStatsChanged;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
        currentHP = maxHP;
    }

    public PegData ToData()
    {
        return new PegData
        {
            id = pegID,
            pegPosition = transform.position,
            value = pegValue
        };
    }

    public void FromData(PegData data)
    {
        pegID = data.id;
        transform.position = data.pegPosition;
        pegValue = data.value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) return;
        PegManager.Instance.SelectPeg(this);
    }

    public void TakeDamage(float amount, Matter matter)
    {
        if (isRespawning) return;

        FloatingTextManager.Instance.ShowFloatingText(
            transform.position,
            $"+${pegValue}",
            Color.gold
        );
        FloatingTextManager.Instance.ShowFloatingText(
            transform.position,
            $"-{matter.damage:F2}",
            Color.red,
            false
        );

        currentHP -= amount;
        OnStatsChanged?.Invoke();

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            if (flashRoutine != null)
                StopCoroutine(flashRoutine);
            flashRoutine = StartCoroutine(FlashDamage());
        }
    }

    private IEnumerator FlashDamage()
    {
        if (sr == null) yield break;
        sr.color = new Color(120, 0, 0);
        yield return new WaitForSeconds(damageFlashTime);

        if (!isRespawning && sr != null)
            sr.color = defaultColor;
        flashRoutine = null;
    }

    private void Die()
    {
        if (isRespawning) return;

        isRespawning = true;

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
            flashRoutine = null;
        }

        if (sr != null)
        {
            sr.color = defaultColor;
            sr.enabled = false;
        }

        PegManager.Instance.ScheduleRespawn(this, respawnDelay);
        gameObject.SetActive(false);
    }

    public void ResetPeg()
    {
        currentHP = baseHP;
        pegValue = basePegValue;

        if (sr != null)
        {
            sr.color = defaultColor;
            sr.enabled = true;
        }
    }
}
