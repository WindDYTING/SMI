using System;
using System.Reflection;
using SMI.Core.Annotations;
using SMI.Core.CrawlerStrategies;

namespace SMI.Core {
    public static class StrategiesFactory {
        public static ICrawlerStrategy GetDefaultStrategy(Type type)
        {
            if (type == typeof(KeyValueCollections))
            {
                var attr = typeof(KeyValueResultStrategy).GetCustomAttribute<CustomSelectorAttribute>();
                var fieldSelector = CreateSelector<IFieldSelector>(attr, x => x.FieldSelectorType);
                var valueSelector = CreateSelector<IValueSelector>(attr, x => x.ValueSelectorType);
                return new KeyValueResultStrategy(valueSelector, fieldSelector);
            }

            if (type == typeof(ImageResult))
                return new ImageResultStrategy();

            return null;

            TSelector CreateSelector<TSelector>(CustomSelectorAttribute attr, Func<CustomSelectorAttribute, Type?> getToType)
            {
                TSelector selector = default;
                Type? type = attr is not null ? getToType(attr) : null;
                if (type is not null)
                {
                    selector = (TSelector) Activator.CreateInstance(type);
                }

                return selector;
            }
        } 
    }
}
