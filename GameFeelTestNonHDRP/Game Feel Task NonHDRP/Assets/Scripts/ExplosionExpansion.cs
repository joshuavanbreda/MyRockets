using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosionExpansion : MonoBehaviour
{
    public GameObject flashFx;
    public GameObject flashSphereFx;
    public GameObject shockwaveFx;
    private bool flash = true;

    void Start()
    {
         //Sequence explosionElastic = DOTween.Sequence();

         //explosionElastic.Append(transform.DOScale(new Vector3(0.105f, 0.105f, 0.105f), 1f))
         //    .Append(transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1f)).SetLoops(-1, LoopType.Yoyo);





        transform.DOScale(new Vector3(0.102f, 0.102f, 0.102f), 0.5f).SetLoops(-1, LoopType.Yoyo);
        Sequence scaleExplosion = DOTween.Sequence();

        //scaleExplosion.Append(gameObject.transform.parent.DOScale(new Vector3(20f, 20f, 20f), 1f))
        //    .Append(transform.parent.transform.DOScale(new Vector3(80f, 80f, 80f), 0.25f))
        //    .AppendInterval(2f)
        //    .Append(gameObject.transform.parent.DOScale(new Vector3(0f, 0f, 0f), 0.5f));


        scaleExplosion.Append(gameObject.transform.parent.DOScale(new Vector3(20f, 20f, 20f), 0.25f))
            .Append(transform.parent.transform.DOScale(new Vector3(0f, 0f, 0f), 0.25f))
            .AppendInterval(0.2f)
            .Append(gameObject.transform.parent.DOScale(new Vector3(80f, 80f, 80f), 0.1f))
            .AppendInterval(1f)
            .Append(gameObject.transform.parent.DOShakeScale(0.5f, strength: new Vector3(10, 10, 10), vibrato: 5, randomness: 0, fadeOut: true))
            .Append(gameObject.transform.parent.DOScale(new Vector3(0f, 0f, 0f), 0.25f));
        //transform.parent.transform.DOScale(new Vector3(10f, 10f, 10f), 1.5f)
        //    .OnComplete(() => transform.parent.transform.DOScale(new Vector3(80f, 80f, 80f), 0.25f));
    }

    public void Update()
    {
        if (gameObject.transform.parent.transform.localScale == Vector3.zero && flash == true)
        {
            flashFx.SetActive(true);
            flashSphereFx.SetActive(true);
            flash = false;
        }
    }
}
