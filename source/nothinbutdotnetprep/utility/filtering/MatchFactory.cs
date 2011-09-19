using System;
using System.Collections.Generic;

namespace nothinbutdotnetprep.utility.filtering
{
    public class MatchFactory<ItemToFilter, PropertyType> : ICreateMatchers<ItemToFilter, PropertyType>
    {
        Func<ItemToFilter, PropertyType> accessor;

        public MatchFactory(Func<ItemToFilter, PropertyType> accessor)
        {
            this.accessor = accessor;
        }

        public IMatchA<ItemToFilter> equal_to(PropertyType value)
        {
            return equal_to_any(value);
        }

        public IMatchA<ItemToFilter> equal_to_any(params PropertyType[] values)
        {
            return anonymous_match(x => new List<PropertyType>(values).Contains(accessor(x)));
        }

        public IMatchA<ItemToFilter> not_equal_to(PropertyType value)
        {
            return equal_to_any(value).not();
        }

        public IMatchA<ItemToFilter> anonymous_match(Condition<ItemToFilter> criteria)
        {
            return new AnonymousMatch<ItemToFilter>(criteria);
        }
    }
}