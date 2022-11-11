public class Define 
{
    /// <summary>
    /// 재상할 오디오 타입 (0 = BGM , 1 = 효과음)
    /// </summary>
    public enum SoundType
    {
        BGM = 0,
        SFX = 1,
    }

    /// <summary>
    /// 오브젝트 Tag 표시
    /// </summary>
    public enum StringTag
    {
        Enemy = 0,
        Player = 1,
    }

    /// <summary>
    /// 스킬 상태 표시
    /// </summary>
    public enum CurrentSkillState
    {
        ACTIVE = 0,
        COOL_TIME = 1,
    }

    /// <summary>
    /// 스테이지 상황 표시
    /// </summary>
    public enum Stage
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE
    }

    /// <summary>
    /// 생성힐 프리팹 타입 (Path)
    /// </summary>
    public enum PrefabType
    {
        Player_Skill,
        Boss_Skill,
        Monsters
    }
}
