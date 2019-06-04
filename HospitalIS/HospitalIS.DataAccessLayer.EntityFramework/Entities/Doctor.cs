using HospitalISDBContext.Enums;
using System.Collections.Generic;

namespace HospitalISDBContext.Entities
{
    public class Doctor : User
    {
        public Specialization Specialization { get; set; }

        public virtual ICollection<DoctorToPatient> DoctorToPatients { get; set; } = new HashSet<DoctorToPatient>();

        public Doctor() : base()
        {
            this.AccessRights = AccessRights.Doctor;
        }
    }
}
