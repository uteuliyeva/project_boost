using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip successClip;

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
        // todo add particle effect upon crush
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayInSeconds);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        // todo add particle effect upon success
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
