using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object Asset/SkillData")]
public class SkillData : ScriptableObject
{
    public float _usingTime; // 지속시간
    public float _attackDuration; // 공격주기
    public int _attackCount; // 공격횟수
    public float _speed; // 스킬의 이동속도
}