using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_hit : MonoBehaviour
{
    public GameObject PlayerVer_;
    public BulletReset bulletReset;
    public AudioClip hitSound;
    private GameObject Player;
    private int life;
    private int currentLife;
    public GameObject SupporterDrone_R;
    public GameObject SupporterDrone_L;
    public AnimationController aniCon;

    private AudioSource audioSource;

    private void Awake()
    {
        Player = PlayerVer_.transform.GetChild(0).gameObject;
        currentLife = Player.GetComponent<CharacterLife>().currentLife;
        life = currentLife;
        audioSource = Player.AddComponent<AudioSource>();
        audioSource.clip = hitSound;
    }

    private void FixedUpdate()
    {
        currentLife = Player.GetComponent<CharacterLife>().currentLife;
        if (currentLife != life)
        {
            if (currentLife < life)
            {
                life = currentLife;
                SupporterDrone_R.GetComponent<PlayerShooter>().canshoot = false;
                SupporterDrone_L.GetComponent<PlayerShooter>().canshoot = false;
                aniCon.startHitMotion();
                audioSource.Play();
                StartCoroutine(respawn());
                StartCoroutine(N_move());
            }
            else
            {
                life = currentLife;
            }
        }
    }

    IEnumerator N_move()
    {
        Player.GetComponent<CameraControlledMovement>().enabled = false;
        yield return new WaitForSeconds(2.2f);
        Player.GetComponent<CameraControlledMovement>().enabled = true;
    }

    IEnumerator respawn()
    {
        Player.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(4f);
        Player.GetComponent<Collider>().enabled = true;
        SupporterDrone_R.GetComponent<PlayerShooter>().canshoot = true;
        SupporterDrone_L.GetComponent<PlayerShooter>().canshoot = true;

        if (currentLife != 0)
        {
            if (bulletReset != null)
            {
                //bulletReset.resetBullet();
            }
        }
    }
}