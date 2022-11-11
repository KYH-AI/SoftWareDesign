using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : LivingEntity
{
    private PlayerController_ playerController;
    public PlayerController_ PlayerController { get { return playerController; } }

    private int playerGold = 0;
    public int PlayerGold { get { return playerGold; } set { playerGold = value; } }

    private UnityEvent hitEvent;
    public UnityEvent HitEvent; //{ get { return hitEvent; } set { hitEvent = value; } }

    private void Start()
    {
        PlayerInit();
    }

    /// <summary>
    /// PlayerController �� Player �ʱ� ���� �ʱ�ȭ 
    /// </summary>
    private void PlayerInit()
    {
        BasicStatInit();
        playerController = GetComponent<PlayerController_>();
        playerController.PlayerControllerInit(this);
    }

    public override sealed void TakeDamage(int newDamage)
    {
        base.TakeDamage(newDamage);
        HitEvent.Invoke();

        // TODO : Player UI ü�� ������ ����
        // TODO : BuffEvent �۵�
      // BuffEvent?.Invoke();
    }

    protected override void OnDead()
    {
        
    }
}
