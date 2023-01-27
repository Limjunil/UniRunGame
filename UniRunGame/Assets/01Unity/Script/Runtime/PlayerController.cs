using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float PLAYER_STEP_ON_Y_ANGLE_MIN = 0.7F; //!< 45도 각도

    public AudioClip deathSound = default;
    public float jumpForce = default;

    private int jumpCount = default;
    private bool isGrounded = false;
    private bool isDead = false;


    #region Player's componet
    private Rigidbody2D playerRigid = default;
    private Animator playerAni = default;
    private AudioSource playerAudio = default;
    #endregion // Player's componet

    // Start is called before the first frame update
    void Start()
    {
        // set player's components
        playerRigid = gameObject.GetComponentMust<Rigidbody2D>();
        playerAni = gameObject.GetComponentMust<Animator>();
        playerAudio = gameObject.GetComponentMust<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // Return/ If player dead
        if(isDead == true)
        {
            // 사망 시 처리를 더이상 진행하지 않고 종료
            return;
        }


        // { 플레이어 점프 관련 로직

        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            // 점프 키를 누르는 순간 움직임을 완전히 멈춤
            playerRigid.velocity = Vector2.zero;

            playerRigid.AddForce(new Vector2(0, jumpForce));

            playerAudio.Play();
        }   // if : 플레이어가 점프할 때
        else if (Input.GetMouseButtonUp(0) && 0 < playerRigid.velocity.y)
        {
            // 나눗셈보다 곱셈이 빠르기 때문에 곱셈을 사용한다.
            playerRigid.velocity = playerRigid.velocity * 0.5f;

        }   // if : 플레이어가 공중에 떠 있을 때

        playerAni.SetBool("Grounded", isGrounded);
        // } 플레이어 점프 관련 로직


        // 점프 중이 아닐 때 그라운드에서 사용하는 로직

    }   // Update()


    //! Player die
    private void Die()
    {
        playerAni.SetTrigger("Die");

        playerAudio.clip = deathSound;
        playerAudio.Play();

        playerRigid.velocity = Vector2.zero;
        isDead = true;

        // 게임 매니저로 플레이어가 죽었을 때의 UI 처리를 한다
        GameManager.instance.OnPlayerDead();
    }   // Die()

    //! 트리거 충돌 감지 처리를 위한 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("DeadZone") && isDead == false)
        {
            Die();
        }
    }

    //! 바닥에 닿았는지 체크하는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PLAYER_STEP_ON_Y_ANGLE_MIN < collision.contacts[0].normal.y)
        {
            isGrounded = true;
            jumpCount = 0;
        }   // if : 45도 보다 완만한 땅을 밟은 경우

    }   // OnCollisionEnter2D()

    //! 바닥에서 벗어났는지 체크하는 함수
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }   // OnCollisionExit2D()
}
