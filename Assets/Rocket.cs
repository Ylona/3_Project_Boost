using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsTrust = 100f;
    [SerializeField] float mainTrust = 2000f;
    [SerializeField] float LevelLoadDelay = 2f;


    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip newLevel;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem newLevelParticles;

    Rigidbody rigidbody;
    AudioSource audio;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (state == State.Alive) {
            RespondToTrustInput();
            RespondToRotateInput();
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (state != State.Alive) { return; }
        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence() {
        state = State.Dying;
        audio.Stop();
        audio.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", LevelLoadDelay);
    }

    private void StartSuccessSequence() {
        state = State.Transcending;
        audio.Stop();
        audio.PlayOneShot(newLevel);
        newLevelParticles.Play();
        Invoke("LoadNextLevel", LevelLoadDelay);
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel() {
        SceneManager.LoadScene(1);
    }

    private void RespondToTrustInput() {
        if (Input.GetKey(KeyCode.Space)) {
            ApplyTrust();
        } else {
            audio.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyTrust() {

        if (!mainEngineParticles.isPlaying) {
            mainEngineParticles.Play();
        }


        if (!audio.isPlaying) {
            audio.PlayOneShot(mainEngine);
        }

        rigidbody.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);



    }

    private void RespondToRotateInput() {
        rigidbody.freezeRotation = true;
        float ratationThisFrame = rcsTrust * Time.deltaTime;


        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(-Vector3.forward * ratationThisFrame);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.forward * ratationThisFrame);
        } 

        rigidbody.freezeRotation = false;

    }


}
