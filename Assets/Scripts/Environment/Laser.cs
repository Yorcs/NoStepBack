using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Laser : MonoBehaviour
{
    public GameObject cam;
    private float startPos;
    private float damageDuration = 500f;
    [SerializeField] protected int damage;

    void Start()
    {
        startPos = transform.position.x;
    }
    void FixedUpdate()
    {
        float dist = cam.transform.position.x;
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
    public void LaserOn()
    {
        gameObject.SetActive(true);
    }

    public void LaserOff()
    {
        gameObject.SetActive(false);
    }

    public void PlayerPushedBack(PlayerStatus player) {
        player.PushBack(damage);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Enemy")){
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();

        }
        if(other.gameObject.tag.Equals("Player")){
            PlayerStatus player = other.gameObject.GetComponent<PlayerStatus>();
            Assert.IsNotNull(player);

            if(!player.IsDead()) {
                player.TakeDamage(damage);
                PlayerPushedBack(player);
            }
        }
    }

}
