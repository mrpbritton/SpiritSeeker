using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator swordAnimator;
    public GameObject swordHolder;
    public PlayerSwords[] swordArray;

    // For Power Ups
    public PowerUpController powerUpController;
    public float damageBuffMultiplier = 2;
    public bool buffed = false;

    private PlayerMove movementScript;
    private bool canAttack = true;

    private void OnEnable()
    {
        movementScript = GetComponent<PlayerMove>();
        swordArray = swordHolder.GetComponentsInChildren<PlayerSwords>();
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
            StartCoroutine(AttackCD(1));
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
            sword.damage = sword.damage * damageBuffMultiplier;
        }
        powerUpController.beginDamage();
    }

    public void endDamageBoost()
    {
        foreach (PlayerSwords sword in swordArray)
        {
            sword.damage = sword.damage / damageBuffMultiplier;
        }
    }

    public void EnableMazeDestruction()
    {
        foreach (PlayerSwords sword in swordArray)
        {
            sword.canDestroyMazeCells = true;
        }
    }

    public void DisableMazeDestruction()
    {
        foreach (PlayerSwords sword in swordArray)
        {
            sword.canDestroyMazeCells = false;
        }
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
}
