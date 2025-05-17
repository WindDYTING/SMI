using System.Collections.Generic;

namespace SMI.Core
{
    public static class SelectorFactory
    {
        private static List<string> ValueSelectors = new()
        {
            "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > td",
            "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > td",
            "#div01 > center > form > table.hasBorder > tbody > tr:nth-child({0}) > td",
            "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > td"
        };

        private static List<string> FieldSelectors = new()
        {
            "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > th",
            "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > th",
            "#div01 > center > form > table.hasBorder > tbody > tr:nth-child({0}) > th",
            "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > th"
        };


        public static string GetValueSelector(int selectionKind)
        {
            if (selectionKind >= ValueSelectors.Count)
            {
                return "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > td";
            }
            return ValueSelectors[selectionKind];
        }

        public static string GetFieldSelector(int selectionKind)
        {
            if (selectionKind >= FieldSelectors.Count)
            {
                return "#div01 > center > table.hasBorder > tbody > tr:nth-child({0}) > th";
            }
            return FieldSelectors[selectionKind];
        }
    }
}
