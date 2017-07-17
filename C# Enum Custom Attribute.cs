public sealed class ReferencesAttribute : Attribute, IAttribute<string[]>
{
    private readonly string[] value;

    public ReferencesAttribute(params string[] value)
    {
        this.value = value;
    }

    public string[] Value
    {
        get { return this.value; }
    }
}

public static R GetAttributeValue<T, R>(IConvertible @enum)
{
    R attributeValue = default(R);

    if (@enum != null)
    {
        FieldInfo fi = @enum.GetType().GetField(@enum.ToString());

        if (fi != null)
        {
            T[] attributes = fi.GetCustomAttributes(typeof(T), false) as T[];

            if (attributes != null && attributes.Length > 0)
            {
                IAttribute<R> attribute = attributes[0] as IAttribute<R>;

                if (attribute != null)
                {
                    attributeValue = attribute.Value;
                }
            }
        }
    }

    return attributeValue;
}
