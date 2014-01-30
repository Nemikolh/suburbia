// --------------------------------------------------------------- //
// 
// Project : Suburbia
// Authors : Nemikolh, Pierre mourlanne
// All Wrongs Reserved.
// --------------------------------------------------------------- //
using System;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : HandlerHandlerHasBeenDestroyed
{
    public EventBus ()
    {
        m_handlers = new Dictionary<Type, List<Pair<IHandler, bool>> > ();
        m_handlers_removed = new List<IHandler>();
        m_stack_depth = 0;
    }

    public abstract class Type
    {
        private static int m_nextHashCode = 0;
        private int m_index;

        public Type ()
        {
            m_index = m_nextHashCode++;
        }

        public override int  GetHashCode ()
        {
            return m_index;
        }

        public override bool Equals (System.Object p_obj)
        {
            if (p_obj == null)
                return false;
            if (p_obj as Type == null)
                return false;

            return this.m_index == (p_obj as Type).m_index;
        }
    }

    private Dictionary<Type, List<Pair<IHandler, bool>> > m_handlers;
    private List<IHandler> m_handlers_removed;
    private uint m_stack_depth;

    public void FireEvent<H> (IEvent<H> p_event)
            where H : class, IHandler
    {
        List<Pair<IHandler,bool>> list;
        // ------------------- DECLARE -------------------- //

        list = null;
        // --------------------- INIT --------------------- //

        if (m_handlers.ContainsKey (p_event.GetEventType ()) == false) {

            if (p_event.GetEventType ().Equals (EventHandlerHasBeenDestroyed.TYPE)) {
                p_event.Dispatch (this as H);
            }
            return;
        }

        m_stack_depth += 1;
        list = m_handlers [p_event.GetEventType ()];

        // Dispatch event among handler.
        foreach (var handler in list) {
            if (!handler.Value) {
                H _handler = handler.Key as H;
                p_event.Dispatch (_handler);
            }
        }

        m_stack_depth -= 1;

        CleanUpRemovedHandlers ();
    }

    public void AddHandler (EventHandlerHasBeenDestroyed.Type<HandlerHandlerHasBeenDestroyed> p_type, HandlerHandlerHasBeenDestroyed p_handler)
    {
        // Assert(false);
        // If it was C++11 code : enable_if<is_same<T, HandlerHasBeenDestroyed>> ...
        // static_assert(false)
    }

    public void AddHandler<H> (IEvent<H>.Type<H> p_type, H p_handler)
        where H : class, IHandler
    {
        if (m_handlers.ContainsKey (p_type)) {
            m_handlers [p_type].Add (new Pair<IHandler, bool> (p_handler, false));
        } else {
            m_handlers [p_type] = new List<Pair<IHandler, bool> > ();
            m_handlers [p_type].Add (new Pair<IHandler, bool> (p_handler, false));
        }
    }

    public void HandleHandlerHasBeenDestroyed (EventHandlerHasBeenDestroyed p_event)
    {
        MarkedAsRemoved(p_event.handler);
        this.m_handlers_removed.Add (p_event.handler);
        CleanUpRemovedHandlers ();
    }

    private void MarkedAsRemoved (IHandler p_handler)
    {
        foreach (var entry in m_handlers) {
            foreach (var item in entry.Value) {
                if (item.Key == p_handler) {
                    item.Value = true;
                    Debug.Log ("Handler marked as removed");
                }

            }
        }
    }

    private void CleanUpRemovedHandlers ()
    {
        if (this.m_stack_depth == 0 && this.m_handlers_removed.Count > 0) {

            // We remove the handlers.
            foreach (IHandler handler in m_handlers_removed) {

                int nb_values = 0;
                foreach (var entry in m_handlers) {
                    nb_values += entry.Value.RemoveAll (elem => elem.Key == handler);
                }
            }

            m_handlers_removed.Clear();
        }
    }
}


