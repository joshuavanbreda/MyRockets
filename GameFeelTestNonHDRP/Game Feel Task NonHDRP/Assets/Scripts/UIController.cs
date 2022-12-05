using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class UIController : MonoBehaviour
{
    public RocketPool rocketPool;
    public GameObject menu;

    public GameObject settingsBtn;
    public GameObject closeSettingsBtn;
    public Toggle rocketTrailsToggle;
    public Slider rocketSpeedSlider;
    public Toggle infiniteRocketsToggle;
    public Toggle postProcessingToggle;

    public TextMeshProUGUI rocketSpeedText;

    private int speedModifier = 10;
    private bool isInfinite;

    private float rocketSpeedFloat;

    public float uiRocketSpeed;
    public float sizeModifier;

    public float rocketWaitModifier = 2f;

    public bool canSeeTrails = true;
    public Camera cam1;
    public Camera cam2;

    void Start()
    {
        menu.SetActive(false);
        menu.transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        rocketSpeedFloat = Mathf.Round((rocketSpeedSlider.value * speedModifier) / 3);

        if (rocketSpeedFloat == 0)
        {
            rocketSpeedText.text = "Rocket speed: \n" + "1";
            uiRocketSpeed = 10f;
        }

        if (rocketSpeedFloat == 1)
        {
            rocketSpeedText.text = "Rocket speed: \n" + "2";
            uiRocketSpeed = 20f;
        }

        if (rocketSpeedFloat == 2)
        {
            rocketSpeedText.text = "Rocket speed: \n" + "2";
            uiRocketSpeed = 20f;
        }

        if (rocketSpeedFloat == 3)
        {
            rocketSpeedText.text = "Rocket speed: \n" + "3";
            uiRocketSpeed = 30f;
        }

        if (isInfinite)
        {
            rocketPool.canPress1 = true;
            rocketPool.canPress2 = true;
            rocketPool.canPress3 = true;
        }
    }

    public void Settings()
    {
        menu.SetActive(true);
        menu.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(()=> menu.transform.DOShakeScale(0.5f, 0.1f));
    }

    public void CloseSettings()
    {
        menu.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(()=> menu.SetActive(false));
    }

    public void RocketTrailsToggle()
    {
        if (rocketTrailsToggle.isOn)
        {
            canSeeTrails = true;
        }
        else
        {
            canSeeTrails = false;
        }
    }

    public void InfiniteRocketsToggle()
    {
        if (infiniteRocketsToggle.isOn)
        {
            isInfinite = true;
            rocketWaitModifier = 0f;
        }
        else
        {
            isInfinite = false;
            rocketWaitModifier = 2f;
        }
    }

    public void PostProcessingToggle()
    {
        if (postProcessingToggle.isOn)
        {
            cam1.GetComponent<PostProcessLayer>().enabled = true;
            cam2.GetComponent<PostProcessLayer>().enabled = true;
        }
        else
        {
            cam1.GetComponent<PostProcessLayer>().enabled = false;
            cam2.GetComponent<PostProcessLayer>().enabled = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
