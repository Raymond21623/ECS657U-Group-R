using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;
 
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    private float damageAfterDifficulty;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {

            RaycastHit hit;
 
            int layerMask = 1 << 8;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (hit.transform.TryGetComponent(out HealthSystem health))
                {
                    health.TakeDamage(damageAfterDifficulty);
                    health.HitVFX(hit.point);
                    print("enemy has dealt damage!");
                    hasDealtDamage = true;
                }
            }
        }
    }
    public void StartDealDamage()
    {
        damageAfterDifficulty = weaponDamage * DifficultyManager.DifficultyMultiplier; 
        Debug.Log($"weaponDamage: {weaponDamage}, Multiplier: {DifficultyManager.DifficultyMultiplier}, Final Damage: {damageAfterDifficulty}");
        canDealDamage = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}