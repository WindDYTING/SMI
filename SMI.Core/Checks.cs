using System;
using System.Diagnostics.CodeAnalysis;

namespace SMI.Core {
    internal static class Checks {

        [return:NotNull]
        public static T ThrowsIsNull<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj)); 
            return obj;
        }
    }
}
