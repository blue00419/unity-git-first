using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject      projectilePrefab ;
    [SerializeField]
    private float           attackRate = 0.1f;
    [SerializeField]
    private int             maxAttackLevel = 3;
    private int             attackLevel = 1;
    private AudioSource     audioSource;

    [SerializeField]
    private GameObject      boomPrefab;
    private int             boomCount = 3;

    public int BoomCount{
        set => boomCount = Mathf.Max(0, value);
        get => boomCount;
    }

    public int AttackLevel{
        set => attackLevel = Mathf.Clamp(value, 1, maxAttackLevel);
        get => attackLevel;
    }

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    public void StartFiring(){
        StartCoroutine("TryAttack");
    }

    public void StopFiring(){
        StopCoroutine("TryAttack");
    }

    public void StartBoom(){
        if(boomCount > 0){
            boomCount--;
            Instantiate(boomPrefab, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator TryAttack(){
        while(true){
            AttackByLevel();

            audioSource.Play();

            yield return new WaitForSeconds(attackRate);
        }
    }

    private void AttackByLevel(){
        GameObject cloneProjectile = null;

        switch(attackLevel){
            case 1:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(projectilePrefab, transform.position + Vector3.left *0.2f, Quaternion.identity);
                Instantiate(projectilePrefab, transform.position + Vector3.right * 0.2f, Quaternion.identity);
            break;
            case 3:
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                
                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f,1,0));

                cloneProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                cloneProjectile.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f,1,0));
                break;
        }
    }
}
