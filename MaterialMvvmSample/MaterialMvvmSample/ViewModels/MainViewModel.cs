using MaterialMvvmSample.Utilities.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Forms.Models;
using XF.Material.Forms.Ui.Dialogs;

namespace MaterialMvvmSample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IJobDialogService _dialogService;
        private ObservableCollection<TestModel> _models;

        public MainViewModel(IJobDialogService dialogService)
        {
            _dialogService = dialogService;

            this.Models = new ObservableCollection<TestModel>()
            {
                new TestModel
                {
                    Title = "Mobile Developer (Xamarin)"
                },
                                new TestModel
                {
                    Title = "Mobile Developer (Native Android)"
                },
                                                new TestModel
                {
                    Title = "Mobile Developer (Native iOS)"
                }
            };
            this.SelectedFilters = new List<int>();
        }

        public MaterialMenuItem[] Actions => new MaterialMenuItem[]
        {
            new MaterialMenuItem
            {
                Text = "Edit",
                //Image = "ic_error"
            },
            new MaterialMenuItem
            {
                Text = "Delete",
                //Image = "ic_clear"
            },
        };

        public string[] Filters => new string[] { "None", "Alhpabetical" };

        public string[] ListActions => new string[] { "Add job", "Sort"};

        public ICommand ListMenuCommand => new Command<MaterialMenuResult>(async (s) => await this.ListMenuSelected(s));

        public ICommand MenuCommand => new Command<MaterialMenuResult>(async (s) => await this.MenuSelected(s));

        public ICommand FiltersSelectedCommand => new Command<int[]>((s) =>
        {
            foreach(var _ in s)
            {
                System.Diagnostics.Debug.WriteLine(this.Filters[_]);
            }
        });

        public ObservableCollection<TestModel> Models
        {
            get => _models;
            set => this.Set(ref _models, value);
        }

        private List<int> _selectedFilters;
        public List<int> SelectedFilters
        {
            get => _selectedFilters;
            set => this.Set(ref _selectedFilters, value);
        }

        private async Task ListMenuSelected(MaterialMenuResult s)
        {
            if (s.Index == 0)
            {
                var result = await _dialogService.AddNewJob();

                if (this.Models.Any(m => m.Title == result))
                {
                    await _dialogService.AlertExistingJob(result);
                }
                else if (!string.IsNullOrEmpty(result))
                {
                    this.Models.Where(m => m.IsNew).ForEach(m => m.IsNew = false);

                    var model = new TestModel
                    {
                        Title = result,
                        IsNew = true
                    };

                    this.Models.Add(model);
                }
            }
            else if (s.Index == 1)
            {
                this.Models = new ObservableCollection<TestModel>(this.Models.OrderBy(m => m.Title));
            }

            else if(s.Index == 2)
            {
                this.SelectedFilters = new List<int>();
            }
        }

        private async Task MenuSelected(MaterialMenuResult i)
        {
            var model = this.Models.FirstOrDefault(m => m.Title == (string)i.Parameter);

            if (i.Index == 0)
            {
                var result = await _dialogService.EditJob(model.Title);

                if (!string.IsNullOrEmpty(result))
                {
                    model.Title = result;
                }
            }
            else if (i.Index == 1)
            {
                var confirmed = await _dialogService.DeleteJob(model.Title);

                if (confirmed)
                {
                    this.Models.Remove(model);

                    await MaterialDialog.Instance.SnackbarAsync("Job deleted", MaterialSnackbar.DurationShort);
                }
            }
        }
    }

    public class TestModel : PropertyChangeAware
    {
        private bool _isNew;
        private int _id;
        private string _title;

        public int Id
        {
            get => _id;
            set => this.Set(ref _id, value);
        }

        public string Title
        {
            get => _title;
            set => this.Set(ref _title, value);
        }

        public bool IsNew
        {
            get => _isNew;
            set => this.Set(ref _isNew, value);
        }
    }
}