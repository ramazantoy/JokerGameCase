using System;
using System.Collections.Generic;

namespace _Project.Scripts.Events
{
    public abstract class EventBus
    {
        public static Action<int, int> OnRollDices;

    }
}
