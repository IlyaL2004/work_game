using UnityEngine;
using TMPro;

public class GroundHealth : MonoBehaviour
{
    public int hp = 10;
    public TextMeshProUGUI hpText;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpText.text = "Площадка: " + hp + " HP";

        if (hp <= 0)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    public void HealHp(int amount)
    {
        hp += amount;
        hpText.text = "Площадка: " + hp + " HP";
    }

        void OnCollisionEnter(Collision collision)
    {
        MeteorMovement meteor = collision.gameObject.GetComponent<MeteorMovement>();
        
        if (meteor != null && !meteor.hasHitGround)
        {
            meteor.hasHitGround = true; 

            Rigidbody meteorRb = meteor.GetComponent<Rigidbody>();
            if (meteorRb != null)
            {
                meteorRb.useGravity = true; 
            }

            int damage = 0;
            
            if (meteor.size == MeteorMovement.MeteorSize.Small) damage = 1;
            if (meteor.size == MeteorMovement.MeteorSize.Medium) damage = 3;
            if (meteor.size == MeteorMovement.MeteorSize.Large) damage = 4;

            // Узнаем точку, куда метеорит ударился об землю
            Vector3 hitPoint = collision.contacts[0].point;
            // Считаем расстояние от точки удара до центра карты (0,0,0)
            float distanceFromCenter = hitPoint.magnitude;

            if (distanceFromCenter < 3f)
            {
                damage = 100; // Мгновенная смерть
            }

            TakeDamage(damage);
            meteor.StartGroundTimer(); 
        }
    }

}