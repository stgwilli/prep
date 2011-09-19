using System;

namespace nothinbutdotnetprep.utility.filtering
{
    public class ComparableMatchFactory<ItemToFilter, PropertyType> : IMatchFactory<ItemToFilter, PropertyType> where PropertyType : IComparable<PropertyType>
    {
        Func<ItemToFilter, PropertyType> accessor;
        IMatchFactory<ItemToFilter, PropertyType> match_factory;

        public ComparableMatchFactory(Func<ItemToFilter, PropertyType> accessor, IMatchFactory<ItemToFilter, PropertyType> match_factory)
        {
            this.accessor = accessor;
            this.match_factory = match_factory;
        }

        public IMatchA<ItemToFilter> greater_than(PropertyType value)
        {
            return new AnonymousMatch<ItemToFilter>(x => accessor(x).CompareTo(value) > 0);
        }

        public IMatchA<ItemToFilter> less_than(PropertyType value)
        {
            return new AnonymousMatch<ItemToFilter>(x => accessor(x).CompareTo(value) < 0);
        }

        public IMatchA<ItemToFilter> between(PropertyType start,PropertyType end)
        {
            return greater_than_or_equal_to(start).and(less_than_or_equal_to(end));
        }

        public IMatchA<ItemToFilter> greater_than_or_equal_to(PropertyType start)
        {
            return greater_than(start).or(equal_to(start));
        }

        public IMatchA<ItemToFilter> less_than_or_equal_to(PropertyType value)
        {
            return less_than(value).or(equal_to(value));
        }

        public IMatchA<ItemToFilter> equal_to(PropertyType value)
        {
            return match_factory.equal_to(value);
        }

        public IMatchA<ItemToFilter> equal_to_any(params PropertyType[] values)
        {
            return match_factory.equal_to_any(values);
        }

        public IMatchA<ItemToFilter> not_equal_to(PropertyType value)
        {
            return match_factory.not_equal_to(value);
        }
    }
}