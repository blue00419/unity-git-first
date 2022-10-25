using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            collision.GetComponent<PlayerHP>().TakeDamage(damage);

            OnDie();
        }
    }

    public void OnDie(){
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
    }
}
