using System;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Maui;

namespace XF.Material.Maui.UI
{
    [TypeConverter(typeof(MaterialElevationTypeConverter))]
    public struct MaterialElevation
    {
        public double RestingElevation { get; }

        public double PressedElevation { get; }

        public MaterialElevation(double uniformElevation) : this(uniformElevation, uniformElevation) { }

        public MaterialElevation(double restingElevation, double pressedElevation)
        {
            RestingElevation = restingElevation;
            PressedElevation = pressedElevation;
        }

        public static implicit operator MaterialElevation(double uniformElevation) => new MaterialElevation(uniformElevation);
    }

    // TODO: changed to TypeConversion
    [TypeConverter(typeof(MaterialElevation))]
    public class MaterialElevationTypeConverter : TypeConverter
    {
        public static object ConvertFromInvariantString(string value)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialElevation)}");
            }

            value = value.Trim();

            if (value.Contains(","))
            {
                var elevations = value.Split(',');

                switch (elevations.Length)
                {
                    case 1:
                        if (double.TryParse(elevations[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var uE))
                        {
                            return new MaterialElevation(uE);
                        }
                        break;
                    case 2:
                        if (double.TryParse(elevations[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var rE)
                            && double.TryParse(elevations[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var pE))
                        {
                            return new MaterialElevation(rE, pE);
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialElevation)}");
                }
            }
            else if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var uE))
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
