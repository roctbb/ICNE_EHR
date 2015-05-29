using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    //Meta models
    public class AnamnesisEventType
    {
        public int ID { get; set; }
        public String name { get; set; }
    }
    public class  DebutType
    {
        public int ID { get; set; }
        public String name { get; set; }
    }
    public class DiagnosisType
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }
    public class ResearchType
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }
    public class MedicineType
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }
    public class NeuroStatusType
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }
    public class Medicine
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String synonims { get; set; }
        public String testimony { get; set; }
        public String contraindications { get; set; }
        public MedicineType type { get; set; }

    }
    public class SyndromeType
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }
    public class AssigmentType
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String description { get; set; }
    }

    public class VisitDate
    {
        public int ID { get; set; }
        public int doctorID { get; set; }
        public DateTime date { get; set; }
        public List<Anamnesis> anamnesis { get; set; }
        public List<Debut> debutes { get; set; }
        public List<Diagnosis> diagnoses { get; set; }
        public List<Research> researches { get; set; }
        public List<Assigment> assigments { get; set; }
        public List<Neurostatus> neurostatuses { get; set; }
        public List<Review> reviews { get; set; }
        public List<Syndrome> syndromes { get; set; }
    }
    public class Anamnesis
    {
        public int ID { get; set; }
        public AnamnesisEventType type { get; set; }
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }

    }
    public class Debut
    {
        public int ID { get; set; }
        public DebutType type { get; set; }
        public String comments { get; set; }
        public String description { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public int? minutes { get; set; }
        public int? seconds { get; set; }

    }
    public class Diagnosis
    {
        public int ID { get; set; }
        public DiagnosisType type { get; set; }
        public String comments { get; set; }

    }
    public class Research
    {
        public int ID { get; set; }
        public ResearchType type { get; set; }
        public String description { get; set; }
        public String comments { get; set; }

    }
    public class Assigment
    {
        public int ID { get; set; }
        public AssigmentType type { get; set; }
        public decimal? weight { get; set; }
        public decimal? dose { get; set; }
        public decimal? inADay { get; set; }
        public String comments { get; set; }
        public String medicine { get; set; }


    }
    public class Neurostatus
    {
        public int ID { get; set; }
        public NeuroStatusType type { get; set; }
        public String description { get; set; }
        public String comments { get; set; }

    }
    public class Review
    {
        public int ID { get; set; }
        [DataType(DataType.Html)]
        [AllowHtml]
        [UIHint("tinymce_full")]
        public String comments { get; set; }
    }
    public class Syndrome
    {
        public int ID { get; set; }
        public SyndromeType type { get; set; }
        public String symptomes { get; set; }
        public String comments { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public int? week { get; set; }
        public int? day { get; set; }
        public int? minutes { get; set; }
        public int? seconds { get; set; }

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
        [DisplayName("ФИО")]
        public String name { get; set; }
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

        public Sex sex { get; set; }
        [DisplayName("Дата рождения")]
 
        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }
        [DisplayName("Мать")]
        public String mother { get; set; }
        [DisplayName("Отец")]
        public String father { get; set; }
        [DisplayName("Адрес проживания")]

        public String adress { get; set; }
        [DisplayName("Коментарии")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [UIHint("tinymce_full")]
        public String comments { get; set; }
        public List<VisitDate> visits { get; set; }

        public Pacient()
        {
        }    
    }
    public class PacientDBContext : DbContext
    {
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<AnamnesisEventType> anamnesisTypes { get; set; }
        public DbSet<Anamnesis> anamneses { get; set; }
        public DbSet<Debut> debutes { get; set; }
        public DbSet<DebutType> debuteTypes { get; set; }
        public DbSet<Diagnosis> diagnoses { get; set; }
        public DbSet<DiagnosisType> diagnosisTypes { get; set; }
        public DbSet<Research> researches { get; set; }
        public DbSet<ResearchType> researchTypes { get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<MedicineType> medicineTypes { get; set; }
        public DbSet<Neurostatus> neurostatuses { get; set; }
        public DbSet<NeuroStatusType> neuroStatusTypes { get; set; }
        public DbSet<Assigment> assigments { get; set; }
        public DbSet<AssigmentType> assigmentTypes { get; set; }
        public DbSet<Syndrome> syndromes { get; set; }
        public DbSet<SyndromeType> syndromeTypes { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<VisitDate> visits { get; set; }

    }
}