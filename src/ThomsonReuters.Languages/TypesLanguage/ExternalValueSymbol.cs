using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using iSynaptic.Commons;
using iSynaptic.Commons.Collections.Generic;

namespace ThomsonReuters.Languages.TypesLanguage
{
    public class ExternalValueSymbol : ISemanticNode, IValueSymbol, INamespaceMember, IAnnotatable
    {
        private Maybe<TypeLookup> _Base;

        public ExternalValueSymbol(NamespaceSymbol parent, IEnumerable<Annotation> annotations, Identifier name, Maybe<TypeLookup> @base)
        {
            Annotations = Guard.NotNull(annotations, "annotations")
                .GroupBy(x => x.Name)
                .ToDictionary(x => x.Key, x => new ReadOnlyCollection<Annotation>(x.ToList()))
                .ToReadOnlyDictionary();

            Parent = Guard.NotNull(parent, "parent");
            Name = Guard.NotNull(name, "name");

            _Base = @base;
        }

        public Maybe<IValueSymbol> Base
        {
            get
            {
                return _Base
                    .Select(x => x.Type)
                    .Cast<IValueSymbol>();
            }
        }

        public QualifiedIdentifier FullName
        {
            get { return Parent.FullName + Name; }
        }

        public NamespaceSymbol Parent { get; private set; }
        Maybe<ISemanticNode> ISemanticNode.Parent { get { return Parent.ToMaybe<ISemanticNode>(); } }

        public Identifier Name { get; private set; }

        public ReadOnlyDictionary<Identifier, ReadOnlyCollection<Annotation>> Annotations { get; private set; }
    }
}