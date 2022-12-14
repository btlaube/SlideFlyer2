using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoBehavior : MonoBehaviour {
    
    public CargoObject cargoObject;

    [SerializeField] private GameObject pickUpPrefab;
    
    [SerializeField] private PlayerScoreInt playerScore;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int currentHealth;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Start() {
        sr.sprite = cargoObject.sprites[0];
        rb.velocity = transform.up * -cargoObject.speed.value;
        currentHealth = cargoObject.health;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
        else {
            sr.sprite = cargoObject.sprites[currentHealth-1];
        }
        
    }

    void Die() {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        GameObject newPickUp = Instantiate(pickUpPrefab, transform.position, Quaternion.identity, transform.parent);
        newPickUp.GetComponent<PickUpBehavior>().pickUpObject = cargoObject.drop;
        playerScore.value++;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        PlayerHealth playerHealth = hitInfo.GetComponent<PlayerHealth>();
        if (playerHealth != null) {
            playerHealth.TakeDamage(cargoObject.damage.value);
            Destroy(gameObject);
        }
    }

}
