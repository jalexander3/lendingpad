using Raven.Client;

namespace Data.Extensions
{
    public static class DocumentQueryExtensions
    {
        public static IDocumentQuery<T> AndIfHasFilter<T>(this IDocumentQuery<T> query, ref bool hasFilter)
        {
            if (hasFilter)
                return query.AndAlso();

            hasFilter = true;
            return query;
        }
    }
}
