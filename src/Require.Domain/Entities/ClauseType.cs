using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Require.Domain.Entities
{
    public class ClauseType
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;

        public Guid CabinetId { get; set; } = Guid.Empty;
        public virtual Cabinet Cabinet { get; set; }
    }
}
