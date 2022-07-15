using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _movingBar;
        
    [SerializeField] private Health _healthGameObject;

    [SerializeField] private float _loweringRatio;

    private bool _dead;

    private delegate void LerpingBar(float newFillAmoun);

    private LerpingBar _lerpingBar;

    private void Awake()
    {
        Health.UIChangedHPHandle += ChangeUI;

        _dead = false;

        _lerpingBar = null;
    }

    private void Update()
    {
        _lerpingBar?.Invoke(_healthGameObject.ConvertHP());
    }

    private void ChangeUI()
    {
        if (_healthGameObject.health == 0 && !_dead)
        {
            Debug.Log("Dead");
            _movingBar.fillAmount = 0;

            _dead = true;
            
            _healthGameObject.gameObject.GetComponent<Enemy>().Dead();
            
            Destroy(gameObject);
        }
        else
        {
            _lerpingBar = LerpBar;
        }
    }

    private void LerpBar(float newFillAmoun)
    {
        if (_movingBar.fillAmount == newFillAmoun)
        {
            _lerpingBar = null;
        }
        else if (_movingBar.fillAmount < newFillAmoun)
        {
            _movingBar.fillAmount = Mathf.Lerp(newFillAmoun, _movingBar.fillAmount, Time.deltaTime * _loweringRatio);
        }
        else
        {
            _movingBar.fillAmount = Mathf.Lerp(_movingBar.fillAmount, newFillAmoun, Time.deltaTime * _loweringRatio);
        }
    }

}
