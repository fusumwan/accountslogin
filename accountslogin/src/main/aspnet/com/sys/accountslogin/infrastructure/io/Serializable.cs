using System;

namespace accountslogin.src.main.aspnet.com.sys.accountslogin.infrastructure.io
{
    public abstract class Serializable
    {
        // This constructor ensures that only derived classes can be instantiated.
        protected Serializable()
        {
        }

        // You can define abstract methods that derived classes must implement if you need specific serialization behaviors.
        public abstract string Serialize();
        public abstract void Deserialize(string data);
    }
}
