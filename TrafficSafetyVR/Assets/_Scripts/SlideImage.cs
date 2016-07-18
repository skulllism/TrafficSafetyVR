using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class SlideImage : MonoBehaviour
{
    [SerializeField]
    private float _duration = 2.5f;

    private GameObject lastChild;
    private Image lastChildImage;


    void Start ()
	{
	    Slide();
	}

    void Slide()
    {
        lastChild = transform.GetChild(transform.childCount - 1).gameObject;
        lastChildImage = lastChild.GetComponent<Image>();
        lastChildImage.DOFade(0, _duration).OnComplete(SlideComplete);
    }

    void SlideComplete()
    {
        lastChild.transform.SetAsFirstSibling();
        lastChildImage.DOFade(1f, 0f);
        Slide();
    }
}
