using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float xp;
    private float health;
    private float lerpTimer;
    private float lerpXpTimer;
    private int userLevel;

    [Header("Health Bar")]
    public float maxHealth = 100;
    public float chipSpeed = 2;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("XP Bar")]
    public float maxXP = 100;
    public float XPchipSpeed = 2;
    public Image frontXPBar;
    public TextMeshProUGUI level;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;

    private void Start()
    {
        xp = 0;
        userLevel = 0;
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    private void Update()
    {
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateUI();
        if(overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (health < 30) return;
            if(durationTimer > duration) 
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateUI()
    {
        level.text = "Lv: " + userLevel;

        //健康值 Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }

        //获取经验
        float fillFXp = frontXPBar.fillAmount; 
        float hExpFraction = xp / maxXP;
        if(fillFXp < hExpFraction)
        {
            lerpXpTimer += Time.deltaTime;
            float percentComplete = lerpXpTimer / XPchipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontXPBar.fillAmount = Mathf.Lerp(fillFXp, hExpFraction, percentComplete);
        }
        else if(fillFXp > hExpFraction)
        {
            frontXPBar.fillAmount = hExpFraction;
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    public void TakeXP(float exp)
    {
        xp += exp;
        lerpXpTimer = 0f;

        if(xp >= maxXP)
        {
            xp = 0;
            userLevel++;
        }
    }
}
