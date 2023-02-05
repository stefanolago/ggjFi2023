using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float timeBetweenShot;
    public GameObject bulletPrefab;
    public float bulletStrenght;
    public Transform shootPosition;
    private Animator animator;
    private float timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!animatorInfo.IsName("ShootAnimation") && !animatorInfo.IsName("DeathAnimation"))
        {
            timer += Time.deltaTime;

            if (timer >= timeBetweenShot)
            {
                timer = 0.0f;
                StartCoroutine(PlayAnimation());
            }
        }
    }

    private IEnumerator PlayAnimation()
    {

        animator.Play("ShootAnimation");
        yield return new WaitForSeconds(timeBetweenShot);
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,shootPosition.position,Quaternion.identity);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(-transform.right * bulletStrenght * transform.localScale.x, ForceMode2D.Impulse);
        animator.Play("Idle");
        SoundManager.Instance.PlayBulletSound();

    }

    public void Death()
    {
        animator.Play("DeathAnimation");
        SoundManager.Instance.PlayDeathPlayerSound();
    }

    public void EndDeathAnimation()
    {
        Destroy(gameObject);
    }

}
