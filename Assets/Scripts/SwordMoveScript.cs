using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMoveScript : MonoBehaviour
{
    public GameObject p;

    Vector3 pPos;

    Quaternion pRot;

    public GameObject gc;

    GameControlScript gcScript;

    Vector3 position;

    Vector3 dif;

    private Vector3 screenToWorldPointPosition;

    float fls;

    Vector3 attackStartPos;

    public float angle;

    int hankei = 60;

    float sAngle;

    public AnimationCurve animationCurve;

    private float _curveRate = 0;

    public float attackTime = 1;

    public int attackDegree = 60;

    public int attacking = 0;

    public int attackValue = 1;

    float sTime;

    bool attacked = false;

    //フェードする速度
    private float _fadingSpeed = 0.05f;

    void Start()
    {
        fls = gameObject.transform.localScale.x;
        gcScript = gc.GetComponent<GameControlScript>();

        attackValue = GameObject.Find("Player").GetComponent<Character>().atk;
    }

    // Update is called once per frame
    void Update()
    {
        _curveRate = Mathf.Clamp(_curveRate + _fadingSpeed, 0f, 1f);

        pPos = p.transform.position;
        pPos.y -= 0.4f;
        pPos.x -= 0.4f;

        // Vector3でマウス位置座標を取得する
        position = Input.mousePosition;

        // Z軸修正
        position.z = 10f;

        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        dif = screenToWorldPointPosition - pPos;
        angle = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(0))
        {
            if (attacking == 0)
            {
                if (Mathf.Abs(angle) < 90)
                {
                    attacking = 1;
                }
                else
                {
                    attacking = 2;
                }
                sTime = Time.time;
                sAngle = angle;
                attackStartPos = transform.position;
            }
        }
        if (Time.time - sTime > attackTime)
        {
            attacking = 0;
            attacked = false;
        }

        if (attacking == 1)
        {
            attack();
        }
        else if (attacking == 2)
        {
            attack2();
        }

        var radian = angle * (Mathf.PI / 180);
        this.transform.position =
            new Vector3(Mathf.Cos(radian) * hankei + 10,
                Mathf.Sin(radian) * hankei - 25,
                0).normalized +
            pPos;
        transform.eulerAngles = new Vector3(0f, 0f, angle - 45);
    }

    void attack()
    {
        angle -=
            animationCurve.Evaluate((Time.time - sTime) / attackTime) *
            attackDegree;
        return;
    }

    void attack2()
    {
        angle +=
            animationCurve.Evaluate((Time.time - sTime) / attackTime) *
            attackDegree;
        return;
    }

    void OnTriggerStay2D(Collider2D other) //敵に武器が当たったとき
    {
        Character target = null;
        if (other.gameObject.GetComponent<Character>() != null)
        {
            target = other.gameObject.GetComponent<Character>();
        }
        else
        {
            return;
        }

        if (target.GetType().IsSubclassOf(typeof (Enemy)))
        {
            if (attacking != 0 && !attacked)
            {
                target.hp -= attackValue;
                Debug.Log(target.name + "を攻撃した");

                if (target.hp <= 0)
                {
                    gcScript.score += target.killScore;
                }
                attacked = true;
            }
        }
    }
}
