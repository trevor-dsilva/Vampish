using UnityEngine;

public class Lightning : MonoBehaviour
{
    public int damage = 10; // Damage dealt to the enemy
    public float detectionRadius; // Radius within which to detect enemies
    public LayerMask enemyLayer; // Layer mask to filter enemies
    public float detectionInterval;
    GameObject Enemy;
    public GameObject lightningPrefab;


    private void Start()
    {
        detectionRadius = GameManager.Instance.detectionRadius;
        detectionInterval = GameManager.Instance.detectionInterval;




        StartCoroutine(DetectAndDamageCoroutine());
    }

    private System.Collections.IEnumerator DetectAndDamageCoroutine()
    {
        while (true)
        {
            DetectAndDamageClosestEnemy();
            yield return new WaitForSeconds(detectionInterval);
        }
    }

    private void DetectAndDamageClosestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);
        if (hits.Length > 0)
        {
            Collider closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider hit in hits)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit;
                }
            }

            if (closestEnemy != null)
            {
                Quaternion rotation = Quaternion.Euler(90, 0, 0);
                Transform aboveEnemy = closestEnemy.transform;
                aboveEnemy.position = aboveEnemy.position + new Vector3(0,1,0);
                GameObject lightningInstance = Instantiate(lightningPrefab, aboveEnemy.position, rotation);
                ParticleSystem lightSys = lightningInstance.GetComponent<ParticleSystem>();

                if (lightSys != null)
                {
                    lightSys.Play();

                }

                DealDamage(closestEnemy.gameObject);
                Destroy(lightningInstance, lightSys.main.duration);
            }
        }
    }

    private void DealDamage(GameObject enemy)
    {
        // Assuming the enemy has a script with a TakeDamage method
        Enemy = enemy;
        Enemy.GetComponent<EnemyHealth>().TakeLightningDamage(damage);

    }
}
