using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 3.5f;
    [SerializeField] GameObject _leaser, _multiLeaser, _shield;
    [SerializeField] GameObject _rightEngine, _leftEngine;
    [SerializeField] float _fireRate = .15f;
    [SerializeField] float _canFire = -1f;
    [SerializeField] int _health = 3;
    [SerializeField] float _speedMultiplier = 2f;

    [SerializeField] AudioClip _laserClip;
    AudioSource _audioSource;

    bool _isTrippleShotEnabled;
    int _score;
    SpawnManager _spawnManager;
    UiManager _uiManager;

    void Start()
    {
        transform.position = Vector3.zero;
        _spawnManager = FindObjectOfType<SpawnManager>();
        _uiManager = FindObjectOfType<UiManager>();
        _audioSource = AddAudioSourceComponent();
    }

    AudioSource AddAudioSourceComponent()
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.clip = _laserClip;
        return audioSource;
    }

    void Update()
    {
        CalculateMovment();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
            FireLeaser();
    }

    void CalculateMovment()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput);
        transform.Translate(_speed * Time.deltaTime * direction);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0));

        if (transform.position.x > 11)
            transform.position = new Vector3(-11, transform.position.y);
        else if (transform.position.x < -11)
            transform.position = new Vector3(11, transform.position.y);
    }

    void FireLeaser()
    {
        _canFire = Time.time + _fireRate;

        var pos = new Vector3(transform.position.x, transform.position.y + 1.2f);
        if (_isTrippleShotEnabled)
        {
            Instantiate(_multiLeaser, transform.position, Quaternion.identity);
            PlayLaserFiredAudio();
            return;
        }
        Instantiate(_leaser, pos, Quaternion.identity);
        PlayLaserFiredAudio();
    }

    void PlayLaserFiredAudio() => _audioSource.Play();

    public void DamageTaken()
    {
        if (_shield.activeInHierarchy)
        {
            _shield.SetActive(false);
            return;
        }

        _health -= 1;
        _uiManager.UpdateLivesSprive(_health);


        switch (_health)
        {
            case 2:
                _rightEngine.SetActive(true);
                break;
            case 1:
                _leftEngine.SetActive(true);
                break;
            case 0:
                _spawnManager.OnPlayerDeath();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public void AddScore()
    {
        _score += 10;
        _uiManager.UpdateScore(_score);
    }

    #region Powerups
    public void ActivateTrippleShotPowerup()
    {
        StartCoroutine(TrippleShotCoroutine());
    }

    public void ActivateSpeedPowerup()
    {
        StartCoroutine(SpeedBoostCoroutine());
    }

    public void ActivateShieldPowerup()
    {
        _shield.SetActive(true);
    }

    IEnumerator TrippleShotCoroutine()
    {
        _isTrippleShotEnabled = true;
        yield return new WaitForSeconds(5);
        _isTrippleShotEnabled = false;
    }

    IEnumerator SpeedBoostCoroutine()
    {
        _speed *= _speedMultiplier;
        yield return new WaitForSeconds(10);
        _speed /= _speedMultiplier;
    }
    #endregion
}
