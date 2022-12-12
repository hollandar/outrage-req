using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Require.Domain.Entities
{
    public enum PropertyKindEnum { None, SingleLine, MultipleLine, Number, SelectOne, SelectMany }
    public class Property
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TypeId { get; set; } = Guid.Empty;
        public virtual Type? Type { get; set; }
        public string Name { get; set; } = "property";
        public PropertyKindEnum Kind { get; set; } = PropertyKindEnum.None;
        public string Label { get; set; } = "Property";
        public string Help { get; set; } = "Help text";
        public PropertyMeta PropertyMeta { get; set; } = new();
    }

    public class PropertyMeta
    {
        public MetaSelect? Select { get; set; } = null;
    }

    public class MetaSelect
    {
        public MetaSelectValue[] Values { get; set; } = Array.Empty<MetaSelectValue>();
    }

    public class MetaValue
    {
        public string Value { get; set; }
    }

    public class MetaSelectValue : MetaValue
    {
        public string Key { get; set; }
    }
}
