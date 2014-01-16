//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18331
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

public abstract class IEvent<H> where H : IHandler
{
    /// <summary>
    /// Type of the event.
    /// </summary>
    /// <typeparam name="T">
    /// T is the interface handler.
    /// </typeparam>
    public class Type<T> : EventBus.Type
        where T : H
    {
        // Does nothing special.
    }

    /// <summary>
    /// Returns the type of the event.
    /// </summary>
    /// <returns>Returns the type of the event.</returns>
    public abstract Type<H> GetEventType ();

    /// <summary>
    /// Dispatch the specified p_handler. Call the method by passing to it "this".
    /// </summary>
    /// <param name="p_handler">P_handler.</param>
    public abstract void Dispatch (H p_handler);
}
