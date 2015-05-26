using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Inspection
    {
        public int ID { get; set; }
        public DateTime date { get; set; }
        public int doctorId { get; set; }
        public String report { get; set; }
    }
    public class Monitoring
    {
        public int ID { get; set; }
        public DateTime date { get; set; }
        public int doctorId { get; set; }
        public String report { get; set; }
        public String type { get; set; }
    }
    public class Diagnosis
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String comments { get; set; }
        public List<Pacient> pacients { get; set; }

        public Diagnosis()
        {
            pacients = new List<Pacient>();
        }
    }
    public class Medicine
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String synonims { get; set; }
        public String testimony { get; set; }
        public String contraindications { get; set; }
        public List<Medicine> incompatibility { get; set; }
        public List<Diagnosis> diasises { get; set; }
        public Medicine() {
            incompatibility = new List<Medicine>();
            diasises = new List<Diagnosis>();
        }
    }
    public class Appointment
    {
        public int ID { get; set; }
        public Medicine medicine { get; set; }
        public Decimal dose { get; set; }
        public String comments { get; set; }
    }
    public class Treatment
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String results { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<Appointment> appoitments { get; set; }
        public Treatment()
        {
            appoitments = new List<Appointment>();
        }
    }
    public enum Sex
    {
        [Display(Name = "Противоречивый")]
        A,
        [Display(Name = "Женский")]
        F,
        [Display(Name = "Мужской")]
        M,
        [Display(Name = "Не применимо")]
        N,
        [Display(Name = "Другой")]
        O,
        [Display(Name = "Неизвестный")]
        U
    }
    public class Pacient
    {
        public int ID { get; set; }
        [DisplayName("Лечащий врач")]
        public int? doctor { get; set; }
        [DisplayName("Имя")]
        [Required]
        public String firstName { get; set; }
        [DisplayName("Фамилия")]
        [Required]
        public String lastName { get; set; }
        [DisplayName("Отчество")]
        public String secondName { get; set; }
        [DisplayName("Номер карты")]
        public String cart { get; set; }
        [DisplayName("Телефон")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public String phone { get; set; }
        [DisplayName("Дата регистрации в системе")]
        [DataType(DataType.Date)]
        public DateTime dateOfregistration { get; set; }
        [DisplayName("Пол")]
        [Required]
        public Sex sex { get; set; }
        [DisplayName("Дата рождения")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }
        [DisplayName("Мать")]
        public String mother { get; set; }
        [DisplayName("Отец")]
        public String father { get; set; }
        [DisplayName("Адрес проживания")]
        [Required]
        public String adress { get; set; }
        public List<Inspection> inspections { get; set; }
        public List<Monitoring> monitorings { get; set; }
        public List<Diagnosis> diagnoses { get; set; }
        public List<Treatment> historyOfTreatment { get; set; }
        public Treatment currentTreatment { get; set; }
        [DisplayName("Вес")]
        public Decimal weight { get; set; }

        public Pacient()
        {
            inspections = new List<Inspection>();
            monitorings = new List<Monitoring>();
            diagnoses = new List<Diagnosis>();
            historyOfTreatment = new List<Treatment>();
        }    
    }
    public class PacientDBContext : DbContext
    {
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Monitoring> Monitorings { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Appointment> Appoitments { get; set; }
    }
}