using UnityEngine;
using UnityEngine.UI; 
using TMPro;         

public class GunController : MonoBehaviour
{
    [Header("UI Элементы")]
    public Image crosshairImage; 
    public TextMeshProUGUI modeText; 

    [Header("Настройки пушки")]
    public float rayDistance = 200f;
    public float pushForce = 15f;
    public LineRenderer lineRenderer; 

    [Header("Звуки пушки")]
    public AudioClip weakShootSound;   // Перетащи сюда звук 1
    public AudioClip mediumShootSound; // Перетащи сюда звук 2
    public AudioClip strongShootSound; // Перетащи сюда звук 3

    [Header("Цвета режимов")]
    public Color weakColor = Color.green;
    public Color mediumColor = Color.yellow;
    public Color strongColor = Color.red;

    private int currentMode = 0; 
    private Camera playerCamera;
    private AudioSource audioSource; // Компонент звука
    
    private string[] modeNames = { "Слабая", "Средняя", "Сильная" };
    private Color[] modeColors;
    private AudioClip[] shootSounds; // Массив звуков для удобства

    void Start()
    {
        playerCamera = GetComponentInParent<Camera>();
        modeColors = new Color[] { weakColor, mediumColor, strongColor };
        shootSounds = new AudioClip[] { weakShootSound, mediumShootSound, strongShootSound };
        
        // Получаем компонент звука
        audioSource = GetComponent<AudioSource>();
        lineRenderer.enabled = false; 
        UpdateUI();
    }

    void Update()
    {
        SwitchMode();
        ShootGravityRay();
    }

    void SwitchMode()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) currentMode = (currentMode + 1) % 3; 
        if (scroll < 0f) currentMode = (currentMode - 1 + 3) % 3; 

        if (Input.GetKeyDown(KeyCode.Alpha1)) currentMode = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentMode = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentMode = 2;

        UpdateUI();
    }

    void ShootGravityRay()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (shootSounds[currentMode] != null)
            {
                audioSource.PlayOneShot(shootSounds[currentMode]); // Воспроизводим звук текущего режима
            }
        }

        // Сам луч рисуется, пока кнопка УДЕРЖИВАЕТСЯ (GetMouseButton)
        if (Input.GetMouseButton(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                lineRenderer.SetPosition(1, hit.point);

                MeteorMovement meteor = hit.collider.GetComponent<MeteorMovement>();
                
                if (meteor != null)
                {
                    meteor.HitByGravityGun(currentMode);
                }
                else if (hit.rigidbody != null)
                {
                    Vector3 pushDirection = (hit.point - Vector3.zero).normalized;
                    hit.rigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, ray.origin + ray.direction * rayDistance);
            }

            lineRenderer.startColor = modeColors[currentMode];
            lineRenderer.endColor = modeColors[currentMode];
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void UpdateUI()
    {
        if (crosshairImage != null)
        {
            crosshairImage.color = modeColors[currentMode];
        }

        if (modeText != null)
        {
            modeText.text = "Режим: " + modeNames[currentMode];
            modeText.color = modeColors[currentMode]; 
        }
    }
}