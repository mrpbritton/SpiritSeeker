using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator swordAnimator;
    public static List<PlayerSwords> swordArray = new List<PlayerSwords>();

    private PlayerMove movementScript;
    private bool canAttack = true;
    private bool buffed = false;

    private void OnEnable()
    {
        movementScript = GetComponent<PlayerMove>();
        PlayerSwords[] allSwords = GetComponentsInChildren<PlayerSwords>();
        foreach(PlayerSwords sword in allSwords)
        {
            swordArray.Add(sword);
        }
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
            StartCoroutine(nameof(AttackCD));
            swordAnimator.Play("SwordSpin");
            foreach (PlayerSwords sword in swordArray)
            {
                sword.canDamage = true;
            }
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

    public IEnumerator AttackCD()
    {
        while (!canAttack)
        {
            yield return new WaitForSeconds(1);
            canAttack = true;
            StopCoroutine(nameof(AttackCD));
            swordAnimator.Play("SwordIdle");
            foreach (PlayerSwords sword in swordArray)
            {
                sword.canDamage = false;
                Debug.Log(sword.canDamage);
            }
        }
    }
    public IEnumerator BuffCD()
    {
        while (buffed)
        {
            yield return new WaitForSeconds(5);
            buffed = false;
            StopCoroutine(nameof(BuffCD));
            foreach (PlayerSwords sword in swordArray)
            {
                sword.damage = sword.damage / 5;
            }
        }
    }
}
