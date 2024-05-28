using UnityEngine;
using System.Collections;

public class ParticleEffectController : MonoBehaviour
{
    public Transform player; // Referencja do obiektu gracza
    public GameObject particleEffect; // Referencja do efektu cząsteczkowego

    private float timeToNextToggle; // Czas do następnej zmiany stanu (włącz/wyłącz)
    public bool isEffectActive = false; // Aktualny stan efektu

    private void Start()
    {
        if (player == null || particleEffect == null)
        {
            Debug.LogError("Player or Particle Effect is not assigned!");
            return;
        }

        // Ustaw początkowy stan efektu na wyłączony
        particleEffect.SetActive(false);

        // Ustaw początkowy czas do zmiany stanu
        SetNextToggleTime();
        // Ustaw początkową pozycję względem gracza
        UpdateEffectPosition();
    }

    private void Update()
    {
        UpdateEffectPosition();

        // Odliczanie do następnej zmiany stanu efektu
        timeToNextToggle -= Time.deltaTime;
        if (timeToNextToggle <= 0)
        {
            ToggleEffect();
            SetNextToggleTime();
        }
    }

    private void ToggleEffect()
    {
        isEffectActive = !isEffectActive;
        particleEffect.SetActive(isEffectActive);
    }

    private void SetNextToggleTime()
    {
        if (isEffectActive)
        {
            // Losowy czas aktywności efektu (10 min do 30 min)
            timeToNextToggle = Random.Range(10 * 60, 30 * 60);
        }
        else
        {
            // Losowy czas nieaktywności efektu (5 min do 60 min)
            timeToNextToggle = Random.Range(5 * 60, 60 * 60);
        }
    }

    private void UpdateEffectPosition()
    {
        // Utrzymuj efekt 15 jednostek nad graczem
        if (player != null)
            particleEffect.transform.position = new Vector3(player.position.x, player.position.y + 15, player.position.z);
    }
}
