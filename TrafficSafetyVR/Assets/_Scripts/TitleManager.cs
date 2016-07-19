using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    public Image _logo;
    public float _sceneChangeTime;

	void Start ()
	{
	    _logo.DOFade(0f, 4f).From();
	    _logo.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 4f);
        Invoke("GoToMain", _sceneChangeTime);
	}

    private void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
