using PuppeteerSharp;

namespace SMI.Core {
    public interface IFieldSelector
    {
        string GetFieldSelectorString(int notion, int trIndex, IPage page);
    }

    public class DefaultFieldSelector : IFieldSelector
    {
        public virtual string GetFieldSelectorString(int notion, int trIndex, IPage page)
        {
            if (notion == (int)NotionKind.依證交法第43條之1第1項取得股份公告 && trIndex == 4)
            {
                return "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > th:nth-child(2)";
            }
            return SelectorFactory.GetFieldSelector(notion);
        }
    }
}