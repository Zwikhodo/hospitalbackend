using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystem.Domain.RefLists
{
    public enum RefListPatientType: int
    {
        // Requires immediate medical attention. Not able to give out personal details
        Emergency = 1,

        // Rehabilitation services, Surgeries, Childbirth, Serious Illnesses that require a patient to be monitored overnight in a hospital
        InPatient = 2,

        // Bloodwork and other lab tests, MRIs, X-Rays or any other types of imaging, Mammograms, Chemotherapy/radiation treatment, Consultations with a specialist physician, Emergency care that doesn’t require hospitalization
        OutPatient = 3
    }
}
