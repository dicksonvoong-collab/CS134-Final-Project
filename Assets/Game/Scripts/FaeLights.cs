using UnityEngine;

public class FaeLights : MonoBehaviour
{
    public int healAmount = 5;
    public int expValue = 10;
    public int speedUpTo = 15;
    public int speedUpDuration = 5;

    public float timer = 10f;
    public float tilBoom;

    public EventsManager manager;

    //sets boom timer
    public void Start()
    {
        tilBoom = Time.time + timer;
    }

    //fae light disappears after time or event ends
    public void Update()
    {
        if (Time.time > tilBoom || !manager.isEventActive)
        {
            Destroy(gameObject);
        }
    }

    //when touched by player, heal player, give exp, give speed buff
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            player.heal(healAmount);

            PlayerEXP exp = other.gameObject.GetComponent<PlayerEXP>();
            exp.addExperience(expValue);

            PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement>();
            movement.speedUp(speedUpTo, speedUpDuration);
        }
    }
}
