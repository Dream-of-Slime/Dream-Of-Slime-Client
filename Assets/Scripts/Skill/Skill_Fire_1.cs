using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slime
{
    public class Skill_Fire_1 : Skill
    {
        public List<GameObject> FireChild;
        public GameObject _skillsFirePrefab;
        private bool is_first = true;

        void OnEnable()
        {
            if (is_first)
            {
                is_first = false;
                for (int i = 0; i < SkillData._attackCount; i++)
                {
                    GameObject temp = Instantiate(_skillsFirePrefab, this.gameObject.transform);
                    temp.SetActive(false);
                    FireChild.Add(temp);
                }
                return;
            }

            makeFire();
        }

        public override void Update()
        {
            base.Update();

            transform.position += transform.up * (SkillData._speed * Time.deltaTime);
        }

        IEnumerator makeFire()
        {
            int count = 0;
            while (gameObject.activeSelf && count < FireChild.Count)
            {
                if (gameObject.transform.position.x >= ResolutionManager.ResolutionX * 0.5f) continue;
                if (gameObject.transform.position.x <= ResolutionManager.ResolutionX * 0.5f * -1) continue;
                if (gameObject.transform.position.y >= ResolutionManager.ResolutionY * 0.5f) continue;
                if (gameObject.transform.position.y <= ResolutionManager.ResolutionY * 0.5f * -1) continue;
                FireChild[count].transform.position = transform.position;
                FireChild[count].SetActive(true);
                count++;
                yield return new WaitForSeconds(SkillData._attackDuration);
            }
        }
    }
}