using UnityEngine;
using TMPro;
using DG.Tweening;
public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;

    [SerializeField] private SpriteRenderer spriteRenderer;
    public int MAxHealth { get; private set; }

    public int CurrentHealth { get; private set; }
    protected void SetupBase(int health, Sprite image)
    {
        MAxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        UpdateHealthText();
        
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP: " + CurrentHealth;
    }

    public void Damage(int damageAmount)
    {
        CurrentHealth-=damageAmount;
        if(CurrentHealth < 0) CurrentHealth = 0;
        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText() ;
    }
}
