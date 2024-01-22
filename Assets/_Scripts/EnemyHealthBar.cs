using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    #region
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
    #endregion

    #region 
    public Animator animator;
    bool isDead = false;

    private static readonly int Dying = Animator.StringToHash("isDead");

   
    #endregion
    

    void Start()
    {
        fullWidth = topBar.rect.width;

    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("isDead", true);

        //GetComponent<AIPath (2D, 3D)>().SetBool(canMove, false);
        float animationDuration = GetAnimationDuration("Death");
        Invoke("DestroyGameObject", animationDuration);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    float GetAnimationDuration(string Death)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(Death))
            {
                return clip.length;
            }
        }
        return 0f;
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
        if(Health <= 0)
        {
            PlayDeathAnimation();
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
