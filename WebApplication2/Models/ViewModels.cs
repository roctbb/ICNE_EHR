using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class documentList
    {
        public int num { get; set; }
        public VisitDate visit { get; set; }
        public bool add { get; set; }
    }
    public class newPacient
    {
        public Pacient pacient { get; set; }
        public List<Doctor> doctors;
    }
    public class newReview
    {
        public Review review { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }

        public newReview()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newAnamnesis
    {
        public Anamnesis anamnesis { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        public List<AnamnesisEventType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newAnamnesis()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newDiagnosis
    {
        public Diagnosis diagnosis { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }

        public List<DiagnosisType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newDiagnosis()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newDebut
    {
        public Debut debut { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        public List<DebutType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newDebut()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newSyndrome
    {
        public Syndrome syndrome { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        public List<SyndromeType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newSyndrome()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newResearch
    {
        public Research research { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        public List<ResearchType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newResearch()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newAssigment
    {
        public Assigment assigment { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        public List<AssigmentType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newAssigment()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newNeurostatus
    {
        public Neurostatus neurostatus { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        public List<NeuroStatusType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newNeurostatus()
        {
            initialDate = DateTime.Today;
        }

    }
    public class newAnalysis
    {
        public Analysis analysis { get; set; }
        public int visitID { get; set; }
        public int? num { get; set; }
        public List<AnalysisType> eventTypes { get; set; }
        [DisplayName("Дата приема")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime initialDate { get; set; }
        public int pacientID { get; set; }
        public newAnalysis()
        {
            initialDate = DateTime.Today;
        }

    }

    public class newFile
    {
       
        [Required]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase FileAttach { get; set; }
        public int visitID { get; set; }
        public int pacientID { get; set; }
        public int? num { get; set; }
        public DateTime initialDate { get; set; }

        public newFile()
        {
            initialDate = DateTime.Today;
        }
    }



}