using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketButtons : MonoBehaviour
{
    public void FadeOutFadeIn()
    {

    }

    public IEnumerator WaitToFadeIn()
    {
        GetComponent<Button>();
        yield return new WaitForSeconds(2f);
    }
}
