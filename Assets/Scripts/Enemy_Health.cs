using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy_Health : MonoBehaviour
{
    public float maxHealth = 1000;
    public float currentHealth;
    public int life = 3;
    private int currentLife; 
    public string damageTag = "P_Attack";
    public GameObject hitEffect;
    public float abilityTime = 4.0f;
    //public float healthRecoveryRate = 30.0f; //자기재생 어차피 작동 안해서 주석처리
    //public float recoveryDelay = 3.0f;

    private bool isInvulnerable = false; //무적상태 여부
    //private float recoveryTimer;
    //private bool isRecovering = false;

    //최종 페이즈 관련
    public GameObject warningEffect;
    public GameObject destroyEffect;
    public GameObject ExplodeEffect;
    //탄 제거 작을 위함
    public BulletReset bulletReset;
    //각 페이즈 별 공격 패턴 스크립트 호출
    public GameObject phase1;
    public GameObject phase2;
    public GameObject phase3;
    //사운드 관련
    public AudioSource audioSource;
    public AudioClip normalPhase;
    public AudioClip finalPhase;
    public AudioClip explosionSound;
    
    //public GameObject cameraObject; 최종페이즈 진동 연출용(현재 안씀)
    public GameObject Item_1up; //목숨 늘려주는 아이템 생성

    //public FlyingObject2 flyingObject2;
    public AimConstraint aimConstraint; //Aim Constraint 컴포넌트 호출

    /* 나중에 작업할거임. 페이즈마다 얼굴 바뀌게
    public Material MaterialP3;
    public Material MaterialP2;
    public Material MaterialP1;
    */

    void Start() //시작 상태
    {
        maxHealth = 1000f;
        currentHealth = maxHealth;
        currentLife = life;
        StartCoroutine(PreparingNewPhase());
        UpdatePhase();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = normalPhase;
        audioSource.Play();

        //flyingObject2 = GetComponent<FlyingObject2>();
        aimConstraint = GetComponent<AimConstraint>();  
        
    }

    void OnTriggerEnter(Collider other) //P_Attack 태그가 달린 오브젝트 피격 시
    {
        if (other.tag == damageTag && !isInvulnerable)
        {
            Vector3 spawnPosition = other.transform.position;
            Quaternion spawnRotation = Quaternion.LookRotation(-other.transform.forward);
            Instantiate(hitEffect, spawnPosition, spawnRotation);
            damaged(5.0f);
            Destroy(other.gameObject);
        }
    }

    public void damaged(float damage) //피격 처리
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;

            /*
            if (life == 1)
            {
                recoveryTimer = recoveryDelay;
                isRecovering = false;
            }
            */

            if (currentHealth <= 0)
            {
                audioSource.PlayOneShot(explosionSound);
                life -= 1;
                UpdatePhase();
                if (life > 0)
                {
                    if (life == 2)
                    {
                        maxHealth = 1200;
                    }
                    else if (life == 1)
                    {
                        aimConstraint.enabled = true; //마지막 페이즈는 항상 플레이어를 바라보도록
                        maxHealth = 2000;
                    }
                    currentHealth = maxHealth; //체력을 최대체력으로
                    StartCoroutine(PreparingNewPhase()); //채력 차는 연출
                    StartCoroutine(ability()); //체력이 전부 찰 때까지 무적시간 부여

                    if (bulletReset != null)
                    {
                        bulletReset.resetBullet(); //다음 페이즈로 넘어갈 때 탄 제거
                    }
                }
                else
                {
                    StartCoroutine(Defeated()); //폭파 이펙트 연출용
                }
            }
        }
    }

    private IEnumerator Defeated() //폭파 이펙트
    {
        FlyObject1 flyObject1 = GetComponent<FlyObject1>(); //움직이는걸 멈춰야 하기 때문에 불러옴
        if (flyObject1 != null) flyObject1.enabled = false;

        //모든 공격을 멈춤
        if (phase1 != null) phase1.SetActive(false);
        if (phase2 != null) phase2.SetActive(false);
        if (phase3 != null) phase3.SetActive(false);
        
        //연출 시작
        isInvulnerable = true; //무적 상태로 전환
        destroyEffect.SetActive(true); //터지는 이펙트
        StartCoroutine(Vibrate()); //진동 시작

        //audioSource.PlayOneShot(explosionSound); //소리

        //yield return new WaitForSeconds(3); //3초 후에
        for (int i = 0;i < 3; i++) {
            audioSource.PlayOneShot(explosionSound);
            yield return new WaitForSeconds(1);

        }

        Instantiate(ExplodeEffect, transform.position, transform.rotation); //최종 폭발 (현재로선 더미데이터)
        /*
        ThirdPersonCamera thirdPersonCamera = cameraObject.GetComponent<ThirdPersonCamera>();
        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.Vibrate();
        }
        */

        Destroy(gameObject); //현재로선 더미데이터
    }

    
    private IEnumerator Vibrate() //진동
    {
        Vector3 currentPosition = transform.position;
        //0.1초 간격으로 무작위 방향으로 이동
        while (true)
        {
            float x = Random.Range(-0.2f, 0.2f);
            float y = Random.Range(-0.2f, 0.2f);
            float z = Random.Range(-0.2f, 0.2f);
            transform.position = currentPosition + new Vector3(x, y, z);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ability()
    {
        //FlyingObject2 flyingObject2 = GetComponent<FlyingObject2>();
        //flyingObject2.MoveToCenter();
        isInvulnerable = true;
        yield return new WaitForSeconds(abilityTime);
        isInvulnerable = false;
        //flyingObject2.ResumeRandomMove();
    }

    void Update()
    {
        /*
        if (life == 1)
        {
            if (!isRecovering)
            {
                recoveryTimer -= Time.deltaTime;
                if (recoveryTimer <= 0)
                {
                    RecoverHealth(healthRecoveryRate * Time.deltaTime);
                }
            }
        }
        */
    }

    /*
    void RecoverHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
    */

    public IEnumerator PreparingNewPhase() //무적시간 동안 체력이 차는 연출
    {
        float duration = abilityTime - 1f;
        float elapsed = 0.0f; //0부터 시작함

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentHealth = Mathf.Lerp(1, maxHealth, elapsed / duration);
            yield return null;
        }

        currentHealth = maxHealth; //최대체력을 넘길수도 있으니까 다시 조정
    }

    private void UpdatePhase() //페이즈 전환용
    {
        switch (life)
        {
            case 3:
                if (phase1 != null) phase1.SetActive(true);
                if (phase2 != null) phase2.SetActive(false);
                if (phase3 != null) phase3.SetActive(false);
                break;
            case 2:
                dropOneUp();
                if (phase1 != null) phase1.SetActive(false);
                if (phase2 != null) phase2.SetActive(true);
                if (phase3 != null) phase3.SetActive(false);
                break;
            case 1:
                if (phase1 != null) phase1.SetActive(false);
                if (phase2 != null) phase2.SetActive(false);
                if (phase3 != null) phase3.SetActive(true);
                warningEffect.SetActive(true);
                audioSource.Stop();
                audioSource.clip = finalPhase;
                audioSource.Play();
                break;
        }
    }

    public void dropOneUp() //1업 아이템 떨굼
    {
        Instantiate(Item_1up, transform.position, Quaternion.identity);
    }
}