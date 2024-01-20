using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [field: SerializeField]
    public int maxHealth {get; private set;}
    [field: SerializeField]
    public int Health {get; private set;}

    [SerializeField]

    private RectTransform topBar;
    [SerializeField]
    private RectTransform bottomBar;

    private float fullWidth;

    private float targetWidth => Health * fullWidth / maxHealth;

    public float animationSpeed = 10f;

    private Coroutine adjustBarWidthWidthCoroutine;

    private void Start()
    {
        fullWidth = topBar.rect.width;
    }
    public void Change(int amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, maxHealth);

        if (adjustBarWidthWidthCoroutine != null)
        {
            StopCoroutine(adjustBarWidthWidthCoroutine);
        }

        adjustBarWidthWidthCoroutine = StartCoroutine(AdjustBarWidth(amount));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Change(20);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            Change(-20);
        }
    }

    private IEnumerator AdjustBarWidth(int amount)
    {
        var suddenChangeBar = amount >= 0 ? bottomBar : topBar;
        var slowChangeBar = amount >= 0 ? topBar : bottomBar;

        suddenChangeBar.SetWidth(targetWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 1f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, targetWidth, Time.deltaTime * animationSpeed));

            yield return null;
        }

        slowChangeBar.SetWidth(targetWidth);
    }
}
