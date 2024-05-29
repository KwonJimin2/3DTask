using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;



    private void Update()
    {
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        stamina.Add(stamina.regenRate * Time.deltaTime);

        if (hunger.curValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Boost(float amount)
    {
        StartCoroutine(BoostTime(amount));
    }

    public IEnumerator BoostTime(float amount)
    {
            GameObject player = GameObject.Find("Player");
            PlayerController playercontroller = player.GetComponent<PlayerController>();
            playercontroller.moveSpeed += amount;

            yield return new WaitForSecondsRealtime(10);
            playercontroller.moveSpeed -= amount;
    }

    public void HP_Up(float amount)
    {
        GameObject health = GameObject.Find("Health");
        Condition condition = health.GetComponent<Condition>();
        condition.maxValue += amount;
        condition.curValue += amount;
    }

    public void HP_Down(float amount)
    {
        GameObject health = GameObject.Find("Health");
        Condition condition = health.GetComponent<Condition>();
        condition.maxValue -= amount;
        condition.curValue = Mathf.Min(condition.curValue , condition.maxValue);
    }

    public void JumpPower_Up(float amount) 
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playercontroller = player.GetComponent<PlayerController>();
        playercontroller.jumpPower += amount;
    }

    public void JumpPower_Down(float amount)
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playercontroller = player.GetComponent<PlayerController>();
        playercontroller.jumpPower -= amount;
    }

    public void Speed_Up(float amount)
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playercontroller = player.GetComponent<PlayerController>();
        playercontroller.moveSpeed += amount;
    }
    public void Speed_Down(float amount)
    {
        GameObject player = GameObject.Find("Player");
        PlayerController playercontroller = player.GetComponent<PlayerController>();
        playercontroller.moveSpeed -= amount;
    }


    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }

}