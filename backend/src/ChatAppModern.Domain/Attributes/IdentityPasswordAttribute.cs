namespace ChatAppModern.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class IdentityPasswordAttribute : ValidationAttribute
{
    private static readonly Regex _passwordRegex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[_\W]).+$");
    
    public override bool IsValid(object? value)
    {
        return value is not null && _passwordRegex.IsMatch(value.ToString() ?? string.Empty);
    }
}