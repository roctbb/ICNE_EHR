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
        [DisplayName("Поле анамнеза")]
        public String name { get; set; }
    }
    public class  DebutType
    {
        public int ID { get; set; }
        [DisplayName("Тип дебюта")]
        public String name { get; set; }
    }
    public class AnalysisType
    {
        public int ID { get; set; }
        [DisplayName("Вид анализа")]
        public String name { get; set; }
    }
    public class DiagnosisType
    {
        public int ID { get; set; }
        [DisplayName("Диагноз")]
        public String name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
    }
    public class ResearchType
    {
        public int ID { get; set; }
        [DisplayName("Исследование")]
        public String name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
    }
    public class MedicineType
    {
        public int ID { get; set; }
        [DisplayName("Лекарство")]
        public String name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
    }
    public class NeuroStatusType
    {
        public int ID { get; set; }
        [DisplayName("Невростатус")]
        public String name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
    }
    public class Medicine
    {
        public int ID { get; set; }
        [DisplayName("Название")]
        
        public String name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
        [DisplayName("Синонимы")]
        [DataType(DataType.MultilineText)]
        public String synonims { get; set; }
        [DisplayName("Показания")]
        [DataType(DataType.MultilineText)]
        public String testimony { get; set; }
        [DisplayName("Противопоказания")]
        [DataType(DataType.MultilineText)]
        public String contraindications { get; set; }
        [DisplayName("Тип")]
        
        public MedicineType type { get; set; }

    }
    public class SyndromeType
    {
        public int ID { get; set; }
        [DisplayName("Тип приступа")]
       
        public String name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
    }
    public class AssigmentType
    {
        public int ID { get; set; }
        [DisplayName("Тип лечения")]
        public String name { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
    }

    public class VisitDate
    {
        public int ID { get; set; }
        public int doctorID { get; set; }
        [DisplayName("Дата приема")]
        public DateTime date { get; set; }
        public List<Anamnesis> anamnesis { get; set; }
        public List<Debut> debutes { get; set; }
        public List<Diagnosis> diagnoses { get; set; }
        public List<Research> researches { get; set; }
        public List<Assigment> assigments { get; set; }
        public List<Neurostatus> neurostatuses { get; set; }
        public List<Review> reviews { get; set; }
        public List<Syndrome> syndromes { get; set; }
        public List<Analysis> analysis { get; set; }
    }
    public class Anamnesis
    {
        public int ID { get; set; }
        [DisplayName("Поле")]
       
        public AnamnesisEventType type { get; set; }
        [DisplayName("Комментарий")]
       
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }

    }
    public class Debut
    {
        public int ID { get; set; }
        [DisplayName("Приступ")]
    
        public DebutType type { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
        [DisplayName("Месяц")]
        
        public int? month { get; set; }
        [DisplayName("Год")]
        
        public int? year { get; set; }
        [DisplayName("Минуты")]
       
        public int? minutes { get; set; }
        [DisplayName("Секунды")]
        
        public int? seconds { get; set; }

    }
    public class Diagnosis
    {
        public int ID { get; set; }
        [DisplayName("Диагноз")]
      
        public DiagnosisType type { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }

    }
    public class Research
    {
        public int ID { get; set; }
        [DisplayName("Исследование")]
       
        public ResearchType type { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }

    }
    public class Assigment
    {
        public int ID { get; set; }
        [DisplayName("Лечение")]
        
        public AssigmentType type { get; set; }
        [DisplayName("Вес")]
        
        public decimal? weight { get; set; }
        [DisplayName("Доза")]
        
        public decimal? dose { get; set; }
        [DisplayName("Мг/Кг/Сутки")]
        
        public decimal? inADay { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }
        [DisplayName("Лекарство")]
        
        public String medicine { get; set; }
        [DefaultValue(1)]
        public int actual { get; set; }
        public DateTime cancelDate { get; set; }



    }
    public class Neurostatus
    {
        public int ID { get; set; }
        [DisplayName("Невростатус")]
       
        public NeuroStatusType type { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }

    }
    public class Analysis
    {
        public int ID { get; set; }
        [DisplayName("Анализ")]

        public AnalysisType type { get; set; }
        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public String description { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }

    }
    public class Review
    {
        public int ID { get; set; }
        [DisplayName("Текст")]
        [DataType(DataType.Html)]
        [AllowHtml]

        public String comments { get; set; }
    }
    public class Syndrome
    {
        public int ID { get; set; }
        [DisplayName("Тип приступа")]
        
        public SyndromeType type { get; set; }
        [DisplayName("Симптомы")]
        [DataType(DataType.MultilineText)]
        public String symptomes { get; set; }
        [DisplayName("Комментарий")]
        [DataType(DataType.MultilineText)]
        public String comments { get; set; }
        [DisplayName("Месяц")]
        
        public int? month { get; set; }
        [DisplayName("Лет")]
        
        public int? year { get; set; }
        [DisplayName("Неделя")]
        
        public int? week { get; set; }
        [DisplayName("День")]
        
        public int? day { get; set; }
        [DisplayName("Минут")]
        
        public int? minutes { get; set; }
        [DisplayName("Секунд")]
        
        public int? seconds { get; set; }

    }
    public class Doctor
    {
        public int ID { get; set; }
        public String name { get; set; }
        public String specialisation { get; set; }

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
        
        public Doctor doctor { get; set; }
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
            sex = Sex.M;
        }    
    }
    public class PacientDBContext : DbContext
    {
        public DbSet<Pacient> pacients { get; set; }
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
        public DbSet<Analysis> analysis { get; set; }
        public DbSet<AnalysisType> analysisTypes { get; set; }
        public DbSet<Syndrome> syndromes { get; set; }
        public DbSet<SyndromeType> syndromeTypes { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<VisitDate> visits { get; set; }
        public DbSet<Doctor> doctors { get; set; }

    }
}