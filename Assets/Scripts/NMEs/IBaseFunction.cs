

public interface IBaseFunction  {

    void Patrol();

    void Attack();

    void Dead();

    void Chase();

    void Idle();

    //bool PlayerDetected();

    bool IsPlayerDetected{get;set;}

}
