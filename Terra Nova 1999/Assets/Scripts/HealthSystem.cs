using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    [Header("Health UI")]
    [SerializeField] Image _healthBarImage;
    [SerializeField] Image _efectBarImage;
    [SerializeField] float _efectBarImageDelayer;

    [Header("        ")]
    [SerializeField] TextMeshProUGUI _healthText;
    [Header("        ")]

    [Header("Health Amount")]
     public float _maxHealth;
     public float _minHealth;
     public float _currentHealth;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_currentOxygen = _maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        _healthText.text = _currentHealth.ToString();

        if (_currentHealth >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        UIEffect();
       // SceneChange();

        if (Input.GetKeyDown(KeyCode.F))
        {
            HealthDecrease(10000);
        }
    }

    private void UIEffect()
    {
        if (_healthBarImage.fillAmount != _efectBarImage.fillAmount)
        {
            _efectBarImage.fillAmount = Mathf.Lerp(_efectBarImage.fillAmount, _healthBarImage.fillAmount, _efectBarImageDelayer);
        }
    }

/*    private void SceneChange()
    {
        if (_currentHealth <= _minHealth)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("AAAA");
        }
    }*/

    public void HealthDecrease(float healthdecrease)
    {
        _currentHealth -= healthdecrease;
        _healthBarImage.fillAmount = _currentHealth / _maxHealth;
    }
    public void HealthIncrease(float healthIncrease)
    {
        _currentHealth += healthIncrease;
        _healthBarImage.fillAmount = _currentHealth / _maxHealth;
    }

}
