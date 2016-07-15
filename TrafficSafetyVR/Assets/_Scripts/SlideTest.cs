using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class SlideTest : MonoBehaviour
{
    [SerializeField]
    private float _Duration = 2.5f;

    private GameObject lastChild;
    private Image lastChildImage;


    void Start ()
	{
	    Slide();
	}

    void Update()
    {
        
    }

    void Slide()
    {
        lastChild = transform.GetChild(transform.childCount - 1).gameObject;
        lastChildImage = lastChild.GetComponent<Image>();
        lastChildImage.DOFade(0, _Duration).OnComplete(SlideComplete);
    }

    void SlideComplete()
    {
        lastChild.transform.SetAsFirstSibling();
        lastChildImage.DOFade(1f, 0f);
        Slide();
    }
}
