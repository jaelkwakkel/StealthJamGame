using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinClip;

    private void Start()
    {
        scoreText.text = score.ToString();
        score = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>() != null)
        {
            Debug.Log("Hit");
            SceneManager.LoadScene("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(coinClip);
            score++;
            scoreText.text = score.ToString();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Finish") && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
