using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attacks : MonoBehaviour
{
    public BossAction[] actions;
    public GameObject wavePrefab;
    public GameObject laserPrefab;

    Animator animator;
    Rigidbody2D body;
    int hitBulletCount = 0;
    float time = 0;
    bool isDead = false;
    int currentAction = -1;
    float currentActionStartTime;
    BossAction previousAction;
    bool landedOnPlatform = true;
    bool actionJustSwitched = false;
    bool orientedLeft = true;
    bool isDefending = false;
    float defenseTime = 0;
    float DEFENCE_TIMER = 2f;
    int health = 10;

    public enum ACTION { STAY, SPAWN_WAVE, SHOOT_LASER };

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

        NextAction();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (isDefending)
        {
            defenseTime += Time.deltaTime;

            if (defenseTime >= DEFENCE_TIMER)
                isDefending = false;
        }

        animator.SetBool("isDeflecting", isDefending);

        BossAction action = actions[currentAction];
        if ((action.action == ACTION.STAY || action.action == ACTION.SPAWN_WAVE || action.action == ACTION.SHOOT_LASER) &&
            time - currentActionStartTime >= action.time)
        {
            NextAction();
        }


        if (actions[currentAction].action == ACTION.STAY)
        {
            if (actionJustSwitched == true)
            {
                body.velocity = Vector2.zero;
            }
        }
        else if (actions[currentAction].action == ACTION.SPAWN_WAVE)
        {
            if (actionJustSwitched == true)
            {
                animator.SetTrigger("triggerWave");

                MiteWaveMovement wave = Instantiate(wavePrefab, transform.position, Quaternion.identity).GetComponent<MiteWaveMovement>();
                if (orientedLeft == true)
                {
                    wave.movement.x = -wave.movement.x;
                    wave.transform.position = new Vector3(wave.transform.position.x + 10, wave.transform.position.y, 0);
                } else
                {
                    wave.transform.position = new Vector3(wave.transform.position.x - 10, wave.transform.position.y, 0);
                }

                Destroy(wave.gameObject, 10f);
            }

        }

        actionJustSwitched = false;
    }


    void NextAction()
    {
        currentActionStartTime = time;
        actionJustSwitched = true;

        currentAction++;
        if (currentAction >= actions.Length)
        {
            currentAction = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDefending == true || isDead == true)
            return;
        
        if (collision.gameObject.tag == "Bullet")
        {
            hitBulletCount++;

            health--;

            if(health == 0)
            {
                Die();
                return;
            }

            if(hitBulletCount >= 3)
            {
                isDefending = true;
                defenseTime = 0;
                hitBulletCount = 0;
            }
        }
    }

    void Die()
    {
        isDead = true;

        AnimationHelper.Instance.Scale(gameObject, Vector3.zero, 3f);

        Invoke("SceneLoad", 4f);
    }

    void SceneLoad()
    {
        SceneManager.LoadScene("end_screen_scene", LoadSceneMode.Single);
    }

    [Serializable]
    public class BossAction
    {
        public ACTION action;
        public bool isOnLeft;
        public float time;
    }
}
