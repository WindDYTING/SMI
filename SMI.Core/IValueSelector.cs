using PuppeteerSharp;

namespace SMI.Core {
    public interface IValueSelector {
        string GetValueSelectorString(int notion, int trIndex, IPage page);
    }

    public class DefaultValueSelector : IValueSelector
    {
        public virtual string GetValueSelectorString(int notion, int trIndex, IPage page)
        {
            return SelectorFactory.GetValueSelector(notion);
        }
    }
}
