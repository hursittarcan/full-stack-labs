using Domain;
using System.Globalization;

public class Percentage : ValueObject<Percentage>
{
    private readonly double _value;
    private readonly CultureInfo CULTURE_BE = new CultureInfo("nl-BE");

    public static implicit operator string(Percentage value) => value.ToString();
    public static implicit operator double(Percentage value) => value._value;
    public static implicit operator Percentage(double value) => new Percentage(value);

    public Percentage(double value)
    {
        Contracts.Require(value >= 0 && value <= 1, "Value must be between 1 and 0");
        _value = value;
    }

    public override string ToString()
    {
        //In case of 0.5, 1.0
        if ((_value * 100) % 1 == 0) return (_value * 100).ToString("N0") + "%";
        //In case of 0.005
        if ((_value * 1000) % 1 == 0) return $"0,{_value * 1000}%";
        //Anything else
        return $"{String.Format(CULTURE_BE, "{0:0.00}", _value * 100)}%";

    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _value;
    }
}