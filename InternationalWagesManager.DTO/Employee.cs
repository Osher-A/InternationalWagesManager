using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternationalWagesManager.DTO
{
    public class Employee : INotifyPropertyChanged
    {
        private string _email;

        public int Id { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "First name is a required field!")]
        public string? FirstName { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Last name field must contain a min of '3' characters!")]
        public string? LastName { get; set; }
        [Required]
        public DateTime? DOB { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        [Display(Name = "Name")]
        public string? FullName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName))
                    return FirstName + " " + LastName;
                else
                    return null;
            }
        }

        public List<WorkConditions> WorkConditions { get; set; } = new List<WorkConditions>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}