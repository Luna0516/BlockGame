using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : PoolObject
{
    public bool isMerge = false;
    public int level = 0;
    public Shape shape = Shape.None;

    Animator anim;
    Rigidbody2D rigid;
    Collider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Block otherBlock = collision.gameObject.GetComponent<Block>();

            if(otherBlock.shape != Shape.None && otherBlock.shape == shape && !isMerge && !otherBlock.isMerge && level == otherBlock.level && level < 4)
            {
                float thisX = transform.position.x;
                float thisY = transform.position.y;
                float otherX = otherBlock.transform.position.x;
                float otherY = otherBlock.transform.position.y;

                if (thisY < otherY || (thisY == otherY && thisX > otherX))
                {
                    otherBlock.Hide(transform.position);
                    LevelUp();
                }
            }
        }
    }

    void Init()
    {
        isMerge = false;
        rigid.simulated = true;
        coll.enabled = true;
        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;
        anim.SetInteger("Level", level);
    }

    void LevelUp()
    {
        isMerge = true;

        rigid.velocity = Vector2.zero;
        rigid.angularVelocity = 0;

        StartCoroutine(LevelUpRoutine());
    }

    void Hide(Vector3 targetPos)
    {
        isMerge = true;

        rigid.simulated = false;
        coll.enabled = false;

        StartCoroutine(HideRoutine(targetPos));
    }

    IEnumerator LevelUpRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        level++;
        // 레벨업으로 변경될 내용들 
        // ex) 점점커지는 애니메이션, 이펙트 실행, 사운드 실행 등
        anim.SetInteger("Level", level);

        yield return new WaitForSeconds(0.2f);

        isMerge = false;
    }

    IEnumerator HideRoutine(Vector3 targetPos)
    {
        int frameCount = 0;

        while (frameCount < 20)
        {
            frameCount++;

            transform.position = Vector3.Lerp(transform.position, targetPos, 0.3f);

            yield return new WaitForEndOfFrame();
        }

        isMerge = false;
        gameObject.SetActive(false);
    }
}
