using System.Collections;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public Light directionalLight;
    public EnemySpawn spawner;
    public GameObject faeLight;

    public float normalDuration = 25f;
    public float faeLightsDuration = 10f;

    public Color normalColor = Color.white;
    public Color darkColor = new Color(0.1f, 0.1f, 0.1f);
    public float lightIntensityNormal = 1f;
    public float lightIntensityDark = 0.2f;
    public float spawnTimer = 3f;
    public int faeLightCount = 20;

    private Color targetColor;
    private float targetIntensity;
    private float targetAmbient;

    public bool isEventActive = false;
    public float timer;

    // Sets timer stats
    void Start()
    {
        timer = normalDuration;
        targetColor = normalColor;
        targetIntensity = lightIntensityNormal;
        targetAmbient = lightIntensityNormal;
    }

    // changes between normal and event time, light change happens slowly
    void Update()
    {
        timer -= Time.deltaTime;

        directionalLight.color = Color.Lerp(directionalLight.color, targetColor, Time.deltaTime);
        directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, targetIntensity, Time.deltaTime);
        RenderSettings.ambientIntensity = Mathf.Lerp(RenderSettings.ambientIntensity, targetAmbient, Time.deltaTime);

        if (timer <= 0)
        {
            if (!isEventActive)
            {
                startFaeLights();
            }
            else
            {
                endFaeLights();
            }
        }
    }

    //event start logic
    void startFaeLights()
    {
        isEventActive = true;
        timer = faeLightsDuration;

        targetColor = darkColor;
        targetIntensity= lightIntensityDark;
        targetAmbient = 0.5f;

        StartCoroutine(spawnFaeLightsActive());
    }

    //fae light spawning logic
    IEnumerator spawnFaeLightsActive()
    {
        float delay = faeLightsDuration / faeLightCount;

        for (int i = 0; i < faeLightCount; i++)
        {
            spawnFaeLight();

            yield return new WaitForSeconds(delay);
        }
    }

    //individual fae light spawning logic
    void spawnFaeLight()
    {
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(15f, 30f);
        Vector3 spawnPosition = spawner.playerLocation.position + new Vector3(spawnDirection.x, 0f, spawnDirection.y) * distance;

        RaycastHit checkSpawn;
        //only spawn fae light if ground is available
        if (Physics.Raycast(spawnPosition + Vector3.up * 10f, Vector3.down, out checkSpawn, 20f))
        {
            GameObject enemy = Instantiate(faeLight, checkSpawn.point + Vector3.up * 2f, Quaternion.identity);
            FaeLights fae = enemy.GetComponent<FaeLights>();
            fae.manager = this;
        }
    }

    //event end logic
    void endFaeLights()
    {
        isEventActive = false;
        timer = normalDuration;

        targetColor = normalColor;
        targetIntensity = lightIntensityNormal;
        targetAmbient = 1f;
    }
}
