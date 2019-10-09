using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Material.Forms.UI
{
    [TypeConverter(typeof(MaterialHorizontalThicknessTypeConverter))]
    public struct MaterialHorizontalThickness
    {
        public MaterialHorizontalThickness(double left, double right)
        {
            Left = left;
            Right = right;
        }

        public MaterialHorizontalThickness(double uniformSize) : this(uniformSize, uniformSize)
        {
        }

        public bool IsUniformSize => Left == Right;

        public double Left { get; }

        public double Right { get; }

        public static implicit operator MaterialHorizontalThickness(double uniformSize) => new MaterialHorizontalThickness(uniformSize);
    }

    [TypeConversion(typeof(MaterialHorizontalThickness))]
    public class MaterialHorizontalThicknessTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value == null)
            {
                throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialHorizontalThickness)}");
            }

            value = value.Trim();

            if (value.Contains(","))
            {
                var elevations = value.Split(',');

                switch (elevations.Length)
                {
                    case 1:
                        if (int.TryParse(elevations[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var uE))
                        {
                            return new MaterialHorizontalThickness(uE);
                        }
                        break;
                    case 2:
                        if (int.TryParse(elevations[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var rE)
                            && int.TryParse(elevations[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var pE))
                        {
                            return new MaterialHorizontalThickness(rE, pE);
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialHorizontalThickness)}");
                }
            }
            else if (int.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var uE))
            {
                return new MaterialHorizontalThickness(uE);
            }
            else
            {
                throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialHorizontalThickness)}");
            }

            throw new InvalidOperationException($"Cannot convert {value} to {typeof(MaterialHorizontalThickness)}");
        }
    }
}
