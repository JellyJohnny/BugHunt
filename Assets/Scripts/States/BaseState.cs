
using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(ModeManager c);

    public abstract void UpdateState(ModeManager c);
}
