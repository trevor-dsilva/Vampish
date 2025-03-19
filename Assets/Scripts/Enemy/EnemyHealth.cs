using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    public GameObject xpOrb; //orb to drop

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   public void TakeLightningDamage(int damage)
    {
        health -= damage;

    }

    public void TakeBulletDamage()
    {
        health -= GameManager.Instance.playerPower;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeBulletDamage();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Vector3 aboveEnemy = transform.position;
            aboveEnemy.y += .5f;
            GameObject newXPorb = Instantiate(xpOrb, aboveEnemy, Quaternion.identity);
            GameManager.Instance.EnemyDestroyed();
            Destroy(gameObject);
        }
        
    }
}
