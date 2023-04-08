using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    PlayerMove PM;
    Transform _player;

    public List<GameObject> _skillsFire;
    public List<GameObject> _skillsWind;
    public List<GameObject> _skillsLightning;
    Dictionary<string, List<GameObject>> _skills;
    Dictionary<string, List<List<GameObject>>> _skillPool;
    [SerializeField] Transform _skillParent;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        PM = PlayerMove.instance;
        _player = PM.transform;

        _skills = new Dictionary<string, List<GameObject>>();
        _skillPool = new Dictionary<string, List<List<GameObject>>>();

        _skills.Add("Fire", _skillsFire);
        _skills.Add("Wind", _skillsWind);
        _skills.Add("Lightning", _skillsLightning);

        List<List<GameObject>> _tempFire = new List<List<GameObject>>();
        List<List<GameObject>> _tempWind = new List<List<GameObject>>();
        List<List<GameObject>> _tempLightning = new List<List<GameObject>>();

        for (int i = 0; i < _skillsFire.Count; i++)
        {
            _tempFire.Add(new List<GameObject>());
        }
        _skillPool.Add("Fire", _tempFire);
        for (int i = 0; i < _skillsFire.Count; i++)
        {
            SkillGenerate("Fire", i, 5);
        }

        for (int i = 0; i < _skillsWind.Count; i++)
        {
            _tempWind.Add(new List<GameObject>());
        }
        _skillPool.Add("Wind", _tempWind);
        for (int i = 0; i < _skillsWind.Count; i++)
        {
            SkillGenerate("Wind", i, 5);
        }

        for (int i = 0; i < _skillsLightning.Count; i++)
        {
            _tempLightning.Add(new List<GameObject>());
        }
        _skillPool.Add("Lightning", _tempLightning);
        for (int i = 0; i < _skillsLightning.Count; i++)
        {
            SkillGenerate("Lightning", i, 5);
        }

        StartCoroutine("ActiveTest");
    }

    IEnumerator ActiveTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Delay_SkillActive("Fire", 0, 1));
        }
    }

    public IEnumerator Delay_SkillActive(string name, int skill, int amount)
    {
        int actived = 0;

        for (int i = 0; i < _skillPool[name][skill].Count; i++)
        {
            if (!_skillPool[name][skill][i].activeSelf)
            {
                if (name == "Fire")
                {
                    if (skill == 0)
                    {
                        float random = Random.Range(-0.2f, 0.2f);
                        _skillPool[name][skill][i].transform.position = _player.position + _player.transform.forward * random;
                        _skillPool[name][skill][i].transform.rotation = _player.rotation;
                    }
                    else
                    {
                        _skillPool[name][skill][i].transform.position = _player.transform.position;
                    }
                    _skillPool[name][skill][i].SetActive(true);
                    actived++;
                    if (actived >= amount)
                    {
                        goto Point1;
                    }
                }
                else if (name == "Wind")
                {
                    if (skill == 0)
                    {

                    }
                    else
                    {

                    }
                    _skillPool[name][skill][i].SetActive(true);
                    actived++;
                    if (actived >= amount)
                    {
                        goto Point1;
                    }
                }
                else if (name == "Lightning")
                {
                    if (skill == 0)
                    {

                    }
                    else
                    {

                    }
                    _skillPool[name][skill][i].SetActive(true);
                    actived++;
                    if (actived >= amount)
                    {
                        goto Point1;
                    }
                }
                //yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForEndOfFrame();
        }

    Point1:

        if (actived < amount)
        {
            SkillGenerate(name, skill, amount);
            StartCoroutine(Delay_SkillActive(name, skill, amount));
        }
    }

    void SkillGenerate(string name, int skill, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(_skills[name][skill], _skillParent);
            temp.SetActive(false);
            _skillPool[name][skill].Add(temp);
        }
    }
}
