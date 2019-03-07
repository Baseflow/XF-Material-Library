using System;
using System.Globalization;
using Xamarin.Forms;

namespace XF.Material.Forms.UI
{
    [TypeConverter(typeof(MaterialElevationTypeConverter))]
    public struct MaterialElevation
    {
        public int RestingElevation { get; }

        public int PressedElevation { get; }

        public MaterialElevation(int uniformElevation) : this(uniformElevation, uniformElevation) { }

        public MaterialElevation(int restingElevation, int pressedElevation)
        {
            this.RestingElevation = restingElevation;
            this.PressedElevation = pressedElevation;
        }

        public static implicit operator MaterialElevation(int uniformElevation) => new MaterialElevation(uniformElevation);
    }

    [Xamarin.Forms.Xaml.TypeConversion(typeof(MaterialElevation))]
    public class MaterialElevationTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value == null)
                throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialElevation)}");
            value = value.Trim();

            if (value.Contains(","))
            {
                var elevations = value.Split(',');

                switch (elevations.Length)
                {
                    case 1:
                        if (int.TryParse(elevations[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var uE))
                        {
                            return new MaterialElevation(uE);
                        }
                        break;
                    case 2:
                        if (int.TryParse(elevations[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var rE)
                            && int.TryParse(elevations[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var pE))
                        {
                            return new MaterialElevation(rE, pE);
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialElevation)}");
                }
            }

            else if (int.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var uE))
            {
                return new MaterialElevation(uE);
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialElevation)}");
            }

            throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialElevation)}");
        }
    }
}
