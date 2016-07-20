using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class TSEventGazeTarget : TSBehavior
{
    public GameObject _gazeButton;
    private Image _gazeButtonImage;
    public Image _checkImage;

    public bool clear { private set; get; }

    void Start()
    {
        _gazeButtonImage = _gazeButton.GetComponent<Image>();
        _checkImage.gameObject.SetActive(false);
    }

    public void Clear()
    {
        clear = true;
        _gazeButton.SetActive(false);
        _gazeButtonImage.DOFade(0, 0.5f);
        _checkImage.gameObject.SetActive(true);
        _checkImage.DOFade(0, 0.5f).From().OnComplete(GazeCheck); ;
    }

    public void GazeButtonDisable()
    {
        _gazeButton.SetActive(false);
       

    }

    public void Reset()
    {
        _gazeButton.SetActive(true);
        _gazeButtonImage.DOFade(1, 0f);
        _checkImage.gameObject.SetActive(false);
    }

    public void GazeCheck()
    {
        _checkImage.DOFade(0, 0.5f).SetDelay(2f);
    }
}
