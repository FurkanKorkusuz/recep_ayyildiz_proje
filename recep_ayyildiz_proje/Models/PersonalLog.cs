using System;

namespace recep_ayyildiz.Entities
{
    public class PersonalLog
    {
        public int ID { get; set; }
        public int PersonalID { get; set; }

        public byte State { get; set; }

        public DateTime  Date { get; set; }
    }



}
