namespace MaterialMvvmSample.ViewModels
{
    public class SecondViewModel : BaseViewModel
    {
        private string _jobTitle;
        public string JobTitle
        {
            get => _jobTitle;
            set => Set(ref _jobTitle, value);
        }

        public override void OnViewPushed(object navigationParameter = null)
        {
            var selectedJob = (TestModel)navigationParameter;
            JobTitle = selectedJob?.Title;
        }
    }
}
