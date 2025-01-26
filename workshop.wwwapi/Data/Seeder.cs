using workshop.wwwapi.Models;
using workshop.wwwapi.Models.Interface;

namespace workshop.wwwapi.Data
{
    public class Seeder
    {
        private bool useRandom;
        private List<List<string>> patientFirstSurNames = new()
        {
            new()
            {
                "Bob",
                "Frans",
                "Olaf",
                "Jurgen",
                "Tord",
                "Mord",
                "Gunn",
                "Hilda",
                "Frej",
                "Anka",
            },
            new()
            {
                "Smedsson",
                "Vikingsson",
                "Grensson",
                "Stensson",
                "Grusson",
                "Svärdsson",
                "Trässon",
            }
        };
        private List<List<string>> doctorFirstSurNames = new()
        {
            new()
            {
                "Dr.Mario",
                "Dr.Eggman",
                "Dr.Cortex",
                "Dr.Bosconovitch",
                "Dr.Faust",
                "Dr.Chakwas",
                "Dr.Nefarious",
                "Dr.Birkin",
                "Dr.Wily",
                "Dr.Kaufmann",
            },
            new()
            {
                "from 'Dr. Mario'",
                "from 'Sonic the Hedgehog'",
                "from 'Crash Bandicoot'",
                "from 'Tekken'",
                "from 'Guilty Gear'",
                "from 'Mass Effect'",
                "from 'Ratchet & Clank'",
                "from 'Resident Evil'",
                "from 'Mega Man'",
                "from 'Silent Hill'",
            }
        };

        private List<Patient> _patients = new();
        private List<Doctor> _doctors = new();
        private List<Appointment> _appointments = new();
        private int nonRandIncr(int cur, int max)
        {
            return (cur + 1) % max;
        }
        private int RandIncr(Random r, int max)
        {
            return r.Next(max);
        }

        
        public Seeder(bool useRandom)
        {

            initNames<Patient>(useRandom, patientFirstSurNames, _patients);
            initNames<Doctor>(useRandom, doctorFirstSurNames, _doctors);
            initAppointments("2025-02-10T10:15:00", 1);
        }

        private void initAppointments(string startDate, int inverval)
        {

            DateTime initialDate = DateTime.Parse(startDate).ToUniversalTime(); // must be universal time, or else migration error


            for (int i = 0; i < _doctors.Count(); i++)
            {
                _appointments.Add(new Appointment()
                {
                    
                    DoctorId = _doctors[i % _doctors.Count()].Id,
                    PatientId = _patients[i % _patients.Count()].Id,
                    Booking = initialDate.AddDays((double)(i * inverval))
                });
            }
            for (int i = 0; i < _doctors.Count(); i += 2)
            {
                _appointments.Add(new Appointment()
                {
                    DoctorId = _doctors[i % _doctors.Count()].Id,
                    PatientId = _patients[(i + 1) % _patients.Count()].Id,
                    Booking = initialDate.AddDays((double)(i * inverval))
                });
            }
        }

        private void initNames<T>(bool useRandom, List<List<string>> listOfStringLists, List<T> entries) where T : ISeedable,  new()
        {
            Random _counter = new Random();
            for (int i = 1; i <= 15; i++)
            {

                var fnames = listOfStringLists[0];
                var fnamesLen = listOfStringLists[0].Count();
                var lnames = listOfStringLists[1];
                var lnamesLen = listOfStringLists[1].Count();
                if (useRandom)
                    entries.Add(new T
                    {
                        Id = i,
                        FullName = $"{fnames[RandIncr(_counter, fnamesLen)]} {lnames[RandIncr(_counter, lnamesLen)]}"
                    });
                else
                    entries.Add(new T
                    {
                        Id = i,
                        FullName = $"{fnames[nonRandIncr(i, fnamesLen)]} {lnames[nonRandIncr(i, lnamesLen)]}"
                    });
            }
        }

        public List<Patient> Patients { get => _patients; }
        public List<Doctor> Doctors { get => _doctors; }
        public List<Appointment> Appointments { get => _appointments; }
    }
}
