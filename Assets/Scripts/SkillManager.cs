using Slime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    PlayerMove PM;
    Transform _player;

    public GameObject _skillItem;
    List<GameObject> _skillItemPool;

    public List<GameObject> _skillsFire;
    public List<GameObject> _skillsWind;
    public List<GameObject> _skillsLightning;
    Dictionary<string, List<GameObject>> _skills;
    Dictionary<string, List<List<GameObject>>> _skillPool;
    [SerializeField] Transform _skillParent;

    int SpawnOffset = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        PM = PlayerMove.instance;
        _player = PM.transform;

        _skillItemPool = new List<GameObject>();

        SkillItemGenerate(5);

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
        StartCoroutine("ItemActiveTest");
    }

    IEnumerator ActiveTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Delay_SkillActive("Fire", 0, 1));
        }
    }

    IEnumerator ItemActiveTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(Delay_SkillItemActive(1));
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
                    float random = Random.Range(-0.2f, 0.2f);
                    _skillPool[name][skill][i].transform.position = _player.position + _player.transform.forward * random;
                    _skillPool[name][skill][i].transform.rotation = _player.rotation;
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
            SkillGenerate(name, skill, amount - actived);
            StartCoroutine(Delay_SkillActive(name, skill, amount - actived));
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

    void SkillItemGenerate(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(_skillItem, _skillParent);
            temp.SetActive(false);
            _skillItemPool.Add(temp);
        }
    }

    public IEnumerator Delay_SkillItemActive(int amount)
    {
        int actived = 0;

        for (int i = 0; i < _skillItemPool.Count; i++)
        {
            if (!_skillItemPool[i].activeSelf)
            {
                int random = Random.Range(0, 3);
                int randomX = 0;
                int randomY = 0;
                int resX = ResolutionManager.ResolutionX;
                int resY = ResolutionManager.ResolutionY;

                if (random == 0)
                {
                    randomX = -SpawnOffset;
                    randomY = Random.Range(SpawnOffset, -SpawnOffset + ResolutionManager.ResolutionY);
                }
                else if (random == 1)
                {
                    randomX = Random.Range(SpawnOffset, -SpawnOffset + ResolutionManager.ResolutionX);
                    randomY = SpawnOffset + ResolutionManager.ResolutionY;
                }
                else if (random == 2)
                {
                    randomX = SpawnOffset + ResolutionManager.ResolutionX;
                    randomY = Random.Range(SpawnOffset, -SpawnOffset + ResolutionManager.ResolutionY);
                }
                else if (random == 3)
                {
                    randomX = Random.Range(SpawnOffset, -SpawnOffset + ResolutionManager.ResolutionX);
                    randomY = -SpawnOffset;
                }

                _skillItemPool[i].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, 0));
                _skillItemPool[i].GetComponent<SkillItemMove>()._wall = random;
                _skillItemPool[i].SetActive(true);
                actived++;
                if (actived >= amount)
                {
                    goto Point1;
                }
            }
            yield return new WaitForEndOfFrame();
        }

    Point1:

        if (actived < amount)
        {
            SkillItemGenerate(amount - actived);
            StartCoroutine(Delay_SkillItemActive(amount - actived));
        }
    }
}