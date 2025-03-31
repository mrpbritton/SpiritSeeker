using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator swordAnimator;
    public PlayerSwords[] swordArray;
    public PowerUpController powerUpController;

    private PlayerMove movementScript;
    private bool canAttack = true;
    private bool buffed = false;

    private void OnEnable()
    {
        movementScript = GetComponent<PlayerMove>();
        swordArray = GetComponentsInChildren<PlayerSwords>();
        foreach (PlayerSwords sword in swordArray)
        {
            sword.canDamage = false;
        }
    }

    void Update()
    {
        if (movementScript.controls.Player.Attack.triggered && canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackCD(1));
            swordAnimator.Play("SwordSpin");
            foreach (PlayerSwords sword in swordArray)
            {
                sword.canDamage = true;
            }
        }
        if (movementScript.controls.Player.SecondaryAttack.triggered && canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackCD(0.5f));
            swordAnimator.Play("SwordThrow");
            swordArray[0].canDamage = true;
            swordArray[1].canDamage = true;
            swordArray[2].canDamage = true;
        }
    }

    public void DamageBoost()
    {
        buffed = true;
        foreach (PlayerSwords sword in swordArray)
        {
            sword.damage = sword.damage * 5;
            Debug.Log(sword.damage);
        }
        StartCoroutine(nameof(BuffCD));
    }

    public IEnumerator AttackCD(float attackLength)
    {
        while (!canAttack)
        {
            yield return new WaitForSeconds(attackLength);
            canAttack = true;
            StopCoroutine(nameof(AttackCD));
            swordAnimator.Play("SwordIdle");
            foreach (PlayerSwords sword in swordArray)
            {
                sword.canDamage = false;
            }
        }
    }
    public IEnumerator BuffCD()
    {
        while (buffed)
        {
            yield return new WaitForSeconds(5);
            buffed = false;
            powerUpController.usedDamage();
            StopCoroutine(nameof(BuffCD));
            foreach (PlayerSwords sword in swordArray)
            {
                sword.damage = sword.damage / 5;
            }
        }
    }
}
