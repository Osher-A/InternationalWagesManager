using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternationalWagesManager.DTO
{
    public class Employee : INotifyPropertyChanged
    {
        private string _email;

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set; }
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
        public string FullName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName))
                    return FirstName + " " + LastName;
                else
                    return null;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}