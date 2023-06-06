using Slime;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
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

    public List<Sprite> _skillsIcons;

    Dictionary<string, List<GameObject>> _skills;
    Dictionary<string, List<List<GameObject>>> _skillPool;
    [SerializeField] Transform _skillParent;

    [HideInInspector] public List<GameObject> _activedItem;

    int SpawnOffset = 100;
    int _combo = 0;

    string _prevSkill = null;

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
        _activedItem = new List<GameObject>();

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

    private void Start()
    {
        ItemActiveTest();
    }

    IEnumerator ActiveTest()
    {
        //while (true)
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    StartCoroutine(Delay_SkillActive("Lightning", 1));
        //}
        yield return new WaitForSeconds(0.5f);
    }

    public void ItemActiveTest()
    {
        foreach (var item in _activedItem)
        {
            item.SetActive(false);
        }
        _activedItem.Clear();

        int actived = 0;
        int first = -1;
        int second = -1;

        while (actived < 3)
        {
            int random = Random.Range(0, _skillPool.Count);
            if (first == second && first == random) continue;

            if (random == 0)
            {
                SkillItemActive(actived, "Fire");
            }
            else if (random == 1)
            {
                SkillItemActive(actived, "Wind");
            }
            else if (random == 2)
            {
                SkillItemActive(actived, "Lightning");
            }
            if (actived == 0)
            {
                first = random;
            }
            else if (actived == 1)
            {
                second = random;
            }
            actived++;
        }
    }

    public IEnumerator Delay_SkillActive(string name, int amount)
    {
        int actived = 0;

        if (_prevSkill == name)
        {
            _combo = Mathf.Clamp(_combo + 1, 0, _skillPool[name].Count - 1);
        }
        else
        {
            _combo = 0;
            _prevSkill = name;
        }

        for (int i = 0; i < _skillPool[name][_combo].Count; i++)
        {
            if (!_skillPool[name][_combo][i].activeSelf)
            {
                if (name == "Fire")
                {
                    float random = Random.Range(-0.2f, 0.2f);
                    _skillPool[name][_combo][i].transform.position = _player.position + _player.transform.forward * random;
                    _skillPool[name][_combo][i].transform.rotation = _player.rotation;
                    _skillPool[name][_combo][i].SetActive(true);
                    actived++;
                    if (actived >= amount)
                    {
                        goto Point1;
                    }
                }
                else if (name == "Wind")
                {
                    if (_combo == 0)
                    {

                    }
                    else
                    {

                    }
                    _skillPool[name][_combo][i].SetActive(true);
                    actived++;
                    if (actived >= amount)
                    {
                        goto Point1;
                    }
                }
                else if (name == "Lightning")
                {
                    if (_combo == 0)
                    {

                    }
                    else
                    {

                    }
                    _skillPool[name][_combo][i].SetActive(true);
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
            SkillGenerate(name, _combo, amount - actived);
            StartCoroutine(Delay_SkillActive(name, amount - actived));
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

    public void SkillItemActive(int wall, string type)
    {
        for (int i = 0; i < _skillItemPool.Count; i++)
        {
            if (!_skillItemPool[i].activeSelf)
            {
                int randomX = 0;
                int randomY = 0;
                int resX = ResolutionManager.ResolutionX;
                int resY = ResolutionManager.ResolutionY;

                if (wall == 0)
                {
                    randomX = -SpawnOffset;
                    randomY = Random.Range(SpawnOffset, -SpawnOffset + resY);
                }
                else if (wall == 1)
                {
                    randomX = Random.Range(SpawnOffset, -SpawnOffset + resX);
                    randomY = SpawnOffset + resY;
                }
                else if (wall == 2)
                {
                    randomX = SpawnOffset + resX;
                    randomY = Random.Range(SpawnOffset, -SpawnOffset + resY);
                }
                else if (wall == 3)
                {
                    randomX = Random.Range(SpawnOffset, -SpawnOffset + resX);
                    randomY = -SpawnOffset;
                }

                _skillItemPool[i].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, 0));
                _skillItemPool[i].GetComponent<SkillItemMove>()._wall = wall;
                _skillItemPool[i].GetComponent<SkillItemMove>()._type = type;
                _skillItemPool[i].SetActive(true);
                _activedItem.Add(_skillItemPool[i]);
                return;
            }
        }
    }
}