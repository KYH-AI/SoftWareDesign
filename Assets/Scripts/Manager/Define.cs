public class Define 
{

    SoundType sound;

    /// <summary>
    /// ����� ����� Ÿ�� (0 = BGM , 1 = ȿ����)
    /// </summary>
    public enum SoundType
    {
        BGM = 0,
        SFX = 1,
    }

    /// <summary>
    /// ��ų ��밡�� Ȯ�� ( ACTIVE : ��ų ��밡��, COOL_TIME : ��ų ���Ұ���(��Ÿ��) )
    /// </summary>
    public enum CurrentSkillState
    {
        ACTIVE = 0,
        COOL_TIME = 1
    }

    /// <summary>
    /// Unity Tag ���ڿ� ������
    /// </summary>
    public enum StringTag
    {
        Enemy = 0,
        Player = 1
    }
}
