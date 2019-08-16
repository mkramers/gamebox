using System;
using Aether.Physics2D.Dynamics;
using Aether.Physics2D.Dynamics.Contacts;

namespace PhysicsCore
{
    public class SeparationEventArgs : EventArgs
    {
        public SeparationEventArgs(Fixture _sender, Fixture _other, Contact _contact)
        {
            Sender = _sender;
            Other = _other;
            Contact = _contact;
        }

        public Fixture Sender { get; }
        public Fixture Other { get; }
        public Contact Contact { get; }
    }
}