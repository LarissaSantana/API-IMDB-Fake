using System;

namespace IMDb.Domain.Utility
{
    public class RoleIdentify
    {
        public Guid Value { get; private set; }
        public string Name { get; private set; }

        private RoleIdentify(Guid value, string name)
        {
            Value = value;
            Name = name;
        }

        public static RoleIdentify Administrator { get; } =
            new RoleIdentify(Guid.Parse("e33a5da4-4c46-4f0e-8ef7-8d01a12f9884"), "Administrator");

        public static RoleIdentify Common { get; } =
            new RoleIdentify(Guid.Parse("04f1d204-4121-44ec-88bd-4b3d433fbaf6"), "Common");
    }
}
