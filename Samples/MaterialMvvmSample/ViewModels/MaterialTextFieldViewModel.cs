using System.Collections.Generic;

namespace MaterialMvvmSample.ViewModels
{
    public class MaterialTextFieldViewModel : BaseViewModel
    {
        public MaterialTextFieldViewModel()
        {
        }

        public IList<string> Choices => new List<string>
        {
            "Ayala Corporation",
            "San Miguel Corporation",
            "YNGEN Holdings Inc.",
            "ERNI Development Center Philippines, Inc., Bern, Switzerland"
        };

    }
}
