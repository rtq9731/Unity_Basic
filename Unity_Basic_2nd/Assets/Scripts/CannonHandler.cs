using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CannonHandler : MonoBehaviour
{

    public static int boxCount = 0;

    [SerializeField] GameObject ballPrefab = null;
    [SerializeField] GameObject firePosition = null;
    [SerializeField] GameObject ClearPanel = null;
    [SerializeField] Slider gauge = null;
    [SerializeField] Button restartBtn = null;
    [SerializeField] float angleSpeed = 60f;
    [SerializeField] float power = 0f;
    [SerializeField] float maxPower = 0f;
    [SerializeField] float startPower = 0f;
    [SerializeField] Text powerText;
    [SerializeField] Text angleText;
    [SerializeField] Text ammoText;
    [SerializeField] Text boxCountText; 
    [SerializeField] CinemachineVirtualCamera CannonCam = null;
    [SerializeField] CinemachineBrain mainCam = null;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] int ammo;

    private int ammoCurrent;
    public bool isFire;
    bool isCharging;

    void Awake()
    {
        ammoCurrent = ammo;
        boxCount = 0;
        restartBtn.onClick.AddListener(() => SceneManager.LoadScene("SampleScene"));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime));

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(0, 0, angleSpeed * Time.deltaTime * (-1)));
        }

        if (ammoCurrent > 0 && !isFire)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                isCharging = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Fire();
                isCharging = false;
            }

            if (isCharging && power < maxPower)
            {
                power += 500 * Time.deltaTime;
            }
        }

        if (boxCount < 1)
            ClearPanel.SetActive(true);


        float z = Mathf.Clamp(transform.rotation.eulerAngles.z, 1, 88);
        power = Mathf.Clamp(power, 0, maxPower);
        gauge.value = power / maxPower;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, z));

        powerText.text = $"Power : {power.ToString("0.0")}";
        angleText.text = $"Angle : {z.ToString("0.0")}¡Æ";
        ammoText.text = $"Ammo : {ammoCurrent}";
        boxCountText.text = $"Box : {boxCount}";
    }

    void Fire()
    {
        isFire = true;
        StartCoroutine(DelayFire());
        ammoCurrent--;
    }

    IEnumerator DelayFire()
    { 
        mainCam.m_DefaultBlend.m_Time = 1f;
        CannonCam.transform.position = new Vector3(firePosition.transform.position.x, firePosition.transform.position.y, CannonCam.transform.position.z);

        CannonCam.gameObject.SetActive(true);


        yield return new WaitForSeconds(1f);
        GameObject ball = Instantiate(ballPrefab, firePosition.transform.position, Quaternion.identity);
        BallScript bs = ball.GetComponent<BallScript>();
        bs.Shoot(firePosition.transform.right, power, CannonCam, this);
        muzzleFlash.Play();
        power = 0;
        CannonCam.Follow = ball.transform;

    }
}
