using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public enum MeteorSize { Small, Medium, Large } 
    public MeteorSize size; 

    public float speed = 10f; 
    public GameObject smallPrefab;  
    public GameObject mediumPrefab; 
    public GameObject explosionParticle; 
    public AudioClip splitSound; 

    public bool isFragment = false; 
    public bool hasHitGround = false; 

    private Rigidbody rb;

         void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (!isFragment)
        {


            Vector3 randomTarget = new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f));
            Vector3 directionToBase = (randomTarget - transform.position).normalized;
            
            rb.linearVelocity = directionToBase * speed;
        }
    }

    void Update()
    {
        if (transform.position.magnitude > 150f) 
        {
            GameManager.Instance.AddScore();
            Destroy(gameObject);
        }
    }

    public void HitByGravityGun(int gunMode)
    {
        int meteorPower = (int)size;

        if (gunMode < meteorPower)
        {
            rb.AddForce(-transform.forward * 0.1f, ForceMode.Impulse);
            return;
        }

        if (gunMode == meteorPower)
        {
            Vector3 pushDirection = (transform.position - Vector3.zero).normalized;
            rb.AddForce(pushDirection * 1.5f, ForceMode.Impulse);
            return;
        }

        if (gunMode > meteorPower)
        {
            if (size == MeteorSize.Small)
            {
                Vector3 pushDirection = (transform.position - Vector3.zero).normalized;
                rb.AddForce(pushDirection * 2f, ForceMode.Impulse);
            }
            else if (size == MeteorSize.Medium)
            {
                int fragmentsCount = Random.Range(2, 4); 
                SpawnFragments(smallPrefab, fragmentsCount);
                Destroy(gameObject);
            }
        }
    }

    void SpawnFragments(GameObject prefab, int count)
    {
        if (splitSound != null) AudioSource.PlayClipAtPoint(splitSound, transform.position, 2.5f);

        if (explosionParticle != null)
        {
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject frag = Instantiate(prefab, transform.position, Quaternion.identity);
            frag.GetComponent<MeteorMovement>().isFragment = true;
            Rigidbody fragRb = frag.GetComponent<Rigidbody>();
            fragRb.linearVelocity = Random.onUnitSphere * 3f; 
        }
    }

    public void StartGroundTimer()
    {
        Destroy(gameObject, 15f); 
    }
}