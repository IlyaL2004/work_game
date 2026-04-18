using UnityEngine;

public class BonusZone : MonoBehaviour
{
    public float lifetime = 10f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("В зону что-то вошло: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("ЭТО ИГРОК! Выдаем энергию.");
            
            GameManager.Instance.AddMoney(5000);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Это не игрок. У него тег: " + other.tag);
        }
    }
}