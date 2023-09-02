using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip successClip;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        
        if(isTransitioning) { return; }

        switch(other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                break;
            case "Friendly":
                Debug.Log("Friendly something");
                break;
            default:
                StartCrushSequence();                
                break;
        }
    }

    void StartCrushSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(hitClip);
        hitParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayInSeconds);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayInSeconds);
    }


    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
