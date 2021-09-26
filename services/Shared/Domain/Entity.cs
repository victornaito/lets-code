using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Domain
{
    public abstract class EntityGuid
    {
        protected EntityGuid()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }

        public abstract bool IsValid();
    }
}