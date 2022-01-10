using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class Unit1 : MonoBehaviour
{
    public Updates UnitUpdates;
    public int cost;
    [SerializeField] public int health = 1;
    private float Starthealth = 1;
    public int damage = 5;
    public bool isOurTeam = true;
    public float speed = 10.0f;
    public int reloadTime = 100;
    public Vector3 moveVector;
    private Rigidbody2D rb;

    private AudioSource source;
    public GameObject HP;

    private Random rnd = new Random();
    private int rand;
    public bool isMain = false;
    public bool canSeeInOtherLines = false;

    public AudioClip shootSound;
    public float maxDistance = 1.0f;
    public GameObject bulletPrefab;
    [SerializeField] public bool isMeele = false;

    protected void Start()
    {
        health += UnitUpdates.Add_Hp;
        damage += UnitUpdates.damage;
        speed += UnitUpdates.speed;
        reloadTime += UnitUpdates.reloadTime;
        maxDistance += UnitUpdates.maxDistance;
        foreach (Transform VARIABLE in GameObject.Find("Main Camera").transform)
        {
            if (VARIABLE.gameObject.name.StartsWith("baffer"))
            {
                //Debug.Log("buff");
                if (isOurTeam == VARIABLE.GetComponent<baffer>().ourTeam && !isMain)
                {
                    health += VARIABLE.GetComponent<baffer>().updates.Add_Hp;
                    damage += VARIABLE.GetComponent<baffer>().updates.damage;
                    speed += VARIABLE.GetComponent<baffer>().updates.speed;
                    //reloadTime += VARIABLE.GetComponent<baffer>().updates.reloadTime;
                    maxDistance += VARIABLE.GetComponent<baffer>().updates.maxDistance;
                }
            }
        }

        Starthealth = health;
//        rand = rnd.Next(-2,3);

        GetComponent<SpriteRenderer>().sortingOrder = 100;
        gameObject.layer = isOurTeam ? 8 : 9;
        if (GetComponent<Animator>())
            GetComponent<Animator>().SetInteger("HP", health);

        source = GetComponent<AudioSource>();
        moveVector = isOurTeam
            ? Vector3.left
            : Vector3.right;
        bool flip = GetComponent<SpriteRenderer>().flipX;
        GetComponent<SpriteRenderer>().flipX = !(flip ^ (moveVector == Vector3.right));

        moveVector = moveVector.normalized;
        rb = gameObject.GetComponents<Rigidbody2D>()[0];

        rb.velocity = moveVector * speed;
    }


    public void Damage(int dmg)
    {
        health -= dmg;
        if (GetComponent<Animator>())
            GetComponent<Animator>().SetInteger("HP", health);
        if (HP != null)
            HP.GetComponent<Image>().fillAmount = health / Starthealth;
    }

    public Vector3 GetMVector()
    {
        return moveVector;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Area"))
        {
            if (other.gameObject.transform.parent.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
            {
                moveVector = new Vector3(
                    other.gameObject.transform.parent.transform.position.x - gameObject.transform.position.x,
                    other.gameObject.transform.parent.transform.position.y - gameObject.transform.position.y);

                moveVector = moveVector.normalized;
            }
        }
    }

    Collider2D IsHit(float d)
    {
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(
            gameObject.transform
                .position, //+ moveVector * (gameObject.GetComponent<Transform>().localScale.x / 2 + 1.1f),
            moveVector + new Vector3(0, d, 0), maxDistance, gameObject.layer == 8 //LayerMask.GetMask("Team1")
                ? LayerMask.GetMask("Team2")
                : LayerMask.GetMask("Team1"));

        if (hit.collider != null)
        {
            if ((hit.collider.gameObject.transform.position.x - transform.position.x) *
                (hit.collider.gameObject.transform.position.x - transform.position.x) +
                (hit.collider.gameObject.transform.position.y - transform.position.y) *
                (hit.collider.gameObject.transform.position.y - transform.position.y)
                < maxDistance * maxDistance)
            {
                if (hit.collider.gameObject.CompareTag("Actor"))
                {
                    if (hit.collider.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
                    {
                        return hit.collider;
                    }
                }
            }
        }

        return null;
    }

    protected void Update()
    {
        if (GetComponent<Animator>())
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0)
                .IsName("End")) //health <= 0) ///Проверка на смерть
            {
                Destroy(gameObject);
                if (isMain)
                {
                    if (isOurTeam)
                    {
                        SceneManager.LoadScene("Lose");
                    }
                    else
                    {
                        gameObject.GetComponent<Level_button>().AddRequirementsToList();
                        SceneManager.LoadScene("Win");
                    }
                }

                return;
            }

        //GetComponent<SpriteRenderer>().sortingOrder = rand + (int) (-Mathf.Floor(transform.position.y * 10) + 100);
        if (GetComponent<Animator>())
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run")) //wait <= 0)
            {
                rb.velocity = moveVector * speed;
            }

        ///смотрим вперед
        Collider2D hit = IsHit(0);
        if (canSeeInOtherLines)
        {
            /// если перед нами никого нет смотрим вверх
            if (hit == null)
                hit = IsHit(0.5f);
            /// если перед нами и сверху никого нет смотрим вних
            if (hit == null)
                hit = IsHit(-0.5f);
            if (hit == null)
                hit = IsHit(1);
            /// если перед нами и сверху никого нет смотрим вних
            if (hit == null)
                hit = IsHit(-1);
        }

        ///если анимация атаки закончилась
//        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Finish Shoot"))
        if (GetComponent<Animator>())
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shoot") &&
                GetComponent<Animator>().IsInTransition(0))
            {
                ///при атаке играем звук
                source.PlayOneShot(shootSound);

                ///если это рукопашный юнит
                if (isMeele)
                {
                    if (hit != null)
                    {
                        ///не из нашей команды
                        if (isOurTeam ^ hit.gameObject.GetComponent<Unit1>().isOurTeam)
                        {
                            ///наносим ему урон
                            hit.gameObject.GetComponent<Unit1>().Damage(damage);
                        }
                    }
                }
                ///если стрелок
                else
                {
                    if (hit != null)
                    {
                        ///создаем патрон который летит туда где мы увидели врага
                        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, // + moveVector,
                            Quaternion.Euler(0, 0,
                                Mathf.Atan2(hit.gameObject.transform.position.y - transform.position.y,
                                    hit.gameObject.transform.position.x - transform.position.x) * -Mathf.Rad2Deg));
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                                hit.gameObject.transform
                                    .position
                                    .x - transform.position.x,
                                hit.gameObject.transform
                                    .position
                                    .y - transform.position.y, 0)
                            .normalized * bulletPrefab
                            .GetComponent<Bullet>().Speed;
                        bullet.GetComponent<Bullet>().isOurTeam = isOurTeam;
                    }
                }

                //            Debug.Log(Animator.GetNextAnimatorClipInfo(0)[0].clip.name);
                //            gameObject.GetComponent<Animator>().Play("AfterShoot");
                if (GetComponent<Animator>())
                    gameObject.GetComponent<Animator>().Play("Finish Shoot");
            }

        ///если нигде никого нет идем дальше
        if (hit == null) return;

        ///если мы видим что то и оно Актор
        if (hit.gameObject.CompareTag("Actor"))
        {
            ///если оно не из нашей команды
            if (hit.gameObject.GetComponent<Unit1>().isOurTeam ^ isOurTeam)
            {
                ///и в нашей зоне действия
                if (Mathf.Abs((gameObject.transform.position.x - hit.gameObject.transform.position.x)
                              * (gameObject.transform.position.x - hit.gameObject.transform.position.x)
                              -
                              (gameObject.transform.position.y - hit.gameObject.transform.position.y)
                              * (gameObject.transform.position.y - hit.gameObject.transform.position.y)
                ) < maxDistance * maxDistance)
                {
                    ///и если мы готовы атаковать
                    if (GetComponent<Animator>())
                        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run")
                            || gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        {
                            ///останавливаемся
                            rb.velocity = Vector3.zero;
                            ///говорим аниматору что нужно вызвать анимацию удара
                            if (GetComponent<Animator>())
                                gameObject.GetComponent<Animator>().SetTrigger("Fire");
                        }
                }
            }
        }
    }
}