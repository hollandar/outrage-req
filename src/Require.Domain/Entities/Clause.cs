using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Require.Domain.Entities
{
    public class Clause
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public virtual ClauseType? Type { get; set; }
        public Guid TypeId { get; set; } = Guid.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int DocumentOrder { get; set; } = 0;
        public Guid? DocumentId { get; set; }
        public virtual Document? Document { get; set; }
        public PropertyValues PropertyValues { get; set; } = new PropertyValues();
        public Guid? ParentClauseId { get; set; }
        public virtual Clause? ParentClause { get; set; }
    }

    public class PropertyValues
    {
        public PropertyValue[] Values { get; set; } = Array.Empty<PropertyValue>();
    }

    public class PropertyValue
    {
        public string Name { get; set; } = String.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
