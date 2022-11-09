public class Define 
{

    SoundType sound;

    /// <summary>
    /// 재상할 오디오 타입 (0 = BGM , 1 = 효과음)
    /// </summary>
    public enum SoundType
    {
        BGM = 0,
        SFX = 1,
    }

    /// <summary>
    /// 스킬 사용가능 확인 ( ACTIVE : 스킬 사용가능, COOL_TIME : 스킬 사용불가능(쿨타임) )
    /// </summary>
    public enum CurrentSkillState
    {
        ACTIVE = 0,
        COOL_TIME = 1
    }

    /// <summary>
    /// Unity Tag 문자열 열거형
    /// </summary>
    public enum StringTag
    {
        Enemy = 0,
        Player = 1
    }
}
