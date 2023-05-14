using System.ComponentModel;

namespace SOZ_project.Models
{
    public class ReportModel
    {
        public int Id { get; set; }
        [DisplayName("Imie Nazwisko")]
        public string Name { get; set; }
        [DisplayName("Opis Zgłoszenia")]
        public string Description { get; set; }
        [DisplayName("Ostatnio widziana")]
        public DateTime LastSeen { get; set; }
        [DisplayName("Płeć")]
        public string Gender { get; set; }
        [DisplayName("Wiek")]
        public string Age { get; set; }

        public string Status { get; set; }

        public string Jpg { get; set; }
    }
}
