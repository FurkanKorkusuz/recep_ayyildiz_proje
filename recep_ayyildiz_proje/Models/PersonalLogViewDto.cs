using System;

namespace recep_ayyildiz_proje.Models
{
    public class PersonalLogViewDto
    {

        public int ID { get; set; }
        public int PersonalID { get; set; }

        public string State { get; set; } = "Giriş";

        public DateTime Date { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
