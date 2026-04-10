using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int lives = 3;
    
    private GameObject enemyPrefab;
    private float spawnTimer = 0f;
    public float spawnInterval = 2f;
    public float environmentTimeScale = 1f;

    public int abilityCharge = 0;
    public bool isAbilityActive = false;
    private float abilityTimer = 0f;
    public float abilityDuration = 5f;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            var go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
        }
        else if (scene.name == "MenuScene" || scene.name == "VictoryScene" || scene.name == "DefeatScene")
        {
            var go = new GameObject("MenuController");
            go.AddComponent<MenuController>();
        }
    }

    void Awake()
    {
        if (instance == null) instance = this;
        enemyPrefab = Resources.Load<GameObject>("Enemy");
    }

    void Update()
    {
        // Ability logic
        if (isAbilityActive)
        {
            abilityTimer -= Time.deltaTime;
            if (abilityTimer <= 0f)
            {
                isAbilityActive = false;
                environmentTimeScale = 1f;
                abilityCharge = 0;
            }
        }
        else
        {
            if (abilityCharge >= 5 && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
            {
                isAbilityActive = true;
                abilityTimer = abilityDuration;
                environmentTimeScale = 0.3f;
            }
        }

        // Spawning logic
        spawnTimer += Time.deltaTime * environmentTimeScale;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            float randomX = Random.Range(4.5f, 5.5f);
            float randomY = Random.Range(-2.5f, 2.5f);
            var enemy = Instantiate(enemyPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
            enemy.AddComponent<EnemyBehaviour>();
        }
    }

    public void OnEnemyKilled()
    {
        score += 10;
        
        if (!isAbilityActive && abilityCharge < 5)
        {
            abilityCharge++;
        }

        spawnInterval -= 0.05f;
        if (spawnInterval < 0.5f) spawnInterval = 0.5f;

        if (score >= 400)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

    public void TakeDamage()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene("DefeatScene");
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;

        GUI.Label(new Rect(20, 20, 200, 30), "Score: " + score, style);
        GUI.Label(new Rect(20, 50, 200, 30), "Vidas: " + lives, style);
        
        string abilityStatus = isAbilityActive ? "Ativo (" + abilityTimer.ToString("F1") + "s)" : (abilityCharge >= 5 ? "Pronto (Shift)" : "Carga: " + abilityCharge + "/5");
        GUI.Label(new Rect(20, 80, 400, 30), "Especial: " + abilityStatus, style);
    }
}
