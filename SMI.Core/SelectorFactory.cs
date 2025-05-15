using System.Collections.Generic;

namespace SMI.Core
{
    public static class SelectorFactory
    {
        private static List<string> ValueSelectors = new()
        {
            "#div01 > center > table.hasBorder > tbody > tr > td",
            "#div01 > center > table.hasBorder > tbody > tr > td",
            "#div01 > center > form > table.hasBorder > tbody > tr > td"
        };

        private static List<string> FieldSelectors = new()
        {
            "#div01 > center > table.hasBorder > tbody > tr > th",
            "#div01 > center > table.hasBorder > tbody > tr > th",
            "#div01 > center > form > table.hasBorder > tbody > tr > th"
        };


        public static string GetValueSelector(int selectionKind)
        {
            if (selectionKind >= ValueSelectors.Count)
            {
                return "#div01 > center > table.hasBorder > tbody > tr > td";
            }
            return ValueSelectors[selectionKind];
        }

        public static string GetFieldSelector(int selectionKind)
        {
            if (selectionKind >= FieldSelectors.Count)
            {
                return "#div01 > center > table.hasBorder > tbody > tr > th";
            }
            return FieldSelectors[selectionKind];
        }
    }
}
