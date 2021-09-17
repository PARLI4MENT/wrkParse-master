using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotAnalyst.Core.Events
{
    public class OnMessage : IEntity
    {
        public Guid Id { get; }
    }
}
