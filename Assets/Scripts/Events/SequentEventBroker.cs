using System;
using System.Linq;
using AreYouFruits.Assertions;
using Growing.Utils;
using Growing.Utils.ExternalPolymorphism;
using Growing.Utils.SequentEvents;

namespace Growing.Events
{
    public sealed class SequentEventBroker<TMessageBase>
    {
        private readonly bool allowSameOrder;
        
        private readonly VirtualAction<TMessageBase> virtualAction;
        private readonly TypedDictionary<object> sequentActions;
        
        public SequentEventBroker(bool allowSameOrder)
        {
            this.allowSameOrder = allowSameOrder;
            
            virtualAction = new VirtualAction<TMessageBase>();
            sequentActions = new TypedDictionary<object>(DispatchType.Static);
        }

        public bool Invoke<TMessage>(TMessage message)
            where TMessage : TMessageBase
        {
            return virtualAction.Invoke(message);
        }

        public bool Subscribe<TMessage>(Action<TMessage> handler, int order = default)
            where TMessage : TMessageBase
        {
            if (sequentActions.Get<SequentDelegate<Action<TMessage>, int>>().TryGet(out var sequentAction))
            {
                return sequentAction.Add(handler, order);
            }

            sequentAction = new SequentDelegate<Action<TMessage>, int>(allowSameOrder: allowSameOrder);
            sequentAction.Add(handler, order).Expect(true);

            virtualAction.Register<TMessage>(sequentAction.Invoke).Expect(true);

            return true;
        }

        public bool Unsubscribe<TMessage>(Action<TMessage> handler, int order = default)
            where TMessage : TMessageBase
        {
            if (!sequentActions.Get<SequentDelegate<Action<TMessage>, int>>().TryGet(out var sequentAction))
            {
                return false;
            }

            sequentAction.Remove(handler, order).Expect(true);

            if (!sequentAction.All.Any())
            {
                sequentActions.Remove<SequentDelegate<Action<TMessage>, int>>().Expect(true);
            }

            return true;
        }
    }
    
    public sealed class SequentEventBroker<TMessageBase, T1>
    {
        private readonly bool allowSameOrder;

        private readonly VirtualAction<TMessageBase, T1> virtualAction;
        private readonly TypedDictionary<object> sequentActions;
        
        public SequentEventBroker(bool allowSameOrder)
        {
            this.allowSameOrder = allowSameOrder;
            
            virtualAction = new VirtualAction<TMessageBase, T1>();
            sequentActions = new TypedDictionary<object>(DispatchType.Static);
        }

        public bool Invoke<TMessage>(TMessage message, T1 p1)
            where TMessage : TMessageBase
        {
            return virtualAction.Invoke(message, p1);
        }

        public bool Subscribe<TMessage>(Action<TMessage, T1> handler, int order = default)
            where TMessage : TMessageBase
        {
            if (sequentActions.Get<SequentDelegate<Action<TMessage, T1>, int>>().TryGet(out var sequentAction))
            {
                return sequentAction.Add(handler, order);
            }

            sequentAction = new SequentDelegate<Action<TMessage, T1>, int>(allowSameOrder: allowSameOrder);
            sequentAction.Add(handler, order).Expect(true);

            sequentActions.Add(sequentAction);
            virtualAction.Register<TMessage>(sequentAction.Invoke).Expect(true);

            return true;
        }

        public bool Unregister<TMessage>(Action<TMessage, T1> handler, int order = default)
            where TMessage : TMessageBase
        {
            if (!sequentActions.Get<SequentDelegate<Action<TMessage, T1>, int>>().TryGet(out var sequentAction))
            {
                return false;
            }

            sequentAction.Remove(handler, order).Expect(true);

            if (!sequentAction.All.Any())
            {
                sequentActions.Remove<SequentDelegate<Action<TMessage, T1>, int>>().Expect(true);
                virtualAction.Unregister<TMessage>(sequentAction.Invoke).Expect(true);
            }

            return true;
        }
    }
}
