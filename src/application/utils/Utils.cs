using System.Reflection;

public class Utils
{
    public static void MergeObjects<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source == null || destination == null)
            throw new ArgumentNullException("Merge of objects cannot be null");

        var sourceProperties = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var destinationProperties = typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var sourceProp in sourceProperties)
        {
            var destProp = Array.Find(destinationProperties, p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType);
            if (destProp != null && destProp.CanWrite)
            {
                var value = sourceProp.GetValue(source);
                destProp.SetValue(destination, value);
            }
        }
    }
}
