namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anamnesis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comments = c.String(),
                        type_ID = c.Int(),
                        VisitDate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AnamnesisEventTypes", t => t.type_ID)
                .ForeignKey("dbo.VisitDates", t => t.VisitDate_ID)
                .Index(t => t.type_ID)
                .Index(t => t.VisitDate_ID);
            
            CreateTable(
                "dbo.AnamnesisEventTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Assigments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        weight = c.Decimal(precision: 18, scale: 2),
                        dose = c.Decimal(precision: 18, scale: 2),
                        inADay = c.Decimal(precision: 18, scale: 2),
                        comments = c.String(),
                        type_ID = c.Int(),
                        VisitDate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AssigmentTypes", t => t.type_ID)
                .ForeignKey("dbo.VisitDates", t => t.VisitDate_ID)
                .Index(t => t.type_ID)
                .Index(t => t.VisitDate_ID);
            
            CreateTable(
                "dbo.AssigmentTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Debuts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comments = c.String(),
                        mounth = c.Int(nullable: false),
                        year = c.Int(nullable: false),
                        minutes = c.Int(nullable: false),
                        seconds = c.Int(nullable: false),
                        type_ID = c.Int(),
                        VisitDate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DebutTypes", t => t.type_ID)
                .ForeignKey("dbo.VisitDates", t => t.VisitDate_ID)
                .Index(t => t.type_ID)
                .Index(t => t.VisitDate_ID);
            
            CreateTable(
                "dbo.DebutTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comments = c.String(),
                        type_ID = c.Int(),
                        Pacient_ID = c.Int(),
                        VisitDate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DiagnosisTypes", t => t.type_ID)
                .ForeignKey("dbo.Pacients", t => t.Pacient_ID)
                .ForeignKey("dbo.VisitDates", t => t.VisitDate_ID)
                .Index(t => t.type_ID)
                .Index(t => t.Pacient_ID)
                .Index(t => t.VisitDate_ID);
            
            CreateTable(
                "dbo.DiagnosisTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                        synonims = c.String(),
                        testimony = c.String(),
                        contraindications = c.String(),
                        type_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MedicineTypes", t => t.type_ID)
                .Index(t => t.type_ID);
            
            CreateTable(
                "dbo.MedicineTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Neurostatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        comments = c.String(),
                        type_ID = c.Int(),
                        VisitDate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NeuroStatusTypes", t => t.type_ID)
                .ForeignKey("dbo.VisitDates", t => t.VisitDate_ID)
                .Index(t => t.type_ID)
                .Index(t => t.VisitDate_ID);
            
            CreateTable(
                "dbo.NeuroStatusTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pacients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        doctor = c.Int(),
                        name = c.String(),
                        cart = c.String(),
                        phone = c.String(),
                        dateOfregistration = c.DateTime(nullable: false),
                        sex = c.Int(nullable: false),
                        birthday = c.DateTime(nullable: false),
                        mother = c.String(),
                        father = c.String(),
                        adress = c.String(),
                        comments = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VisitDates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        doctorID = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        Pacient_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pacients", t => t.Pacient_ID)
                .Index(t => t.Pacient_ID);
            
            CreateTable(
                "dbo.Researches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        comments = c.String(),
                        type_ID = c.Int(),
                        VisitDate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ResearchTypes", t => t.type_ID)
                .ForeignKey("dbo.VisitDates", t => t.VisitDate_ID)
                .Index(t => t.type_ID)
                .Index(t => t.VisitDate_ID);
            
            CreateTable(
                "dbo.ResearchTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comments = c.String(),
                        VisitDate_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VisitDates", t => t.VisitDate_ID)
                .Index(t => t.VisitDate_ID);
            
            CreateTable(
                "dbo.Syndromes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        symptomes = c.String(),
                        comments = c.String(),
                        mounth = c.Int(nullable: false),
                        year = c.Int(nullable: false),
                        week = c.Int(nullable: false),
                        day = c.Int(nullable: false),
                        minutes = c.Int(nullable: false),
                        seconds = c.Int(nullable: false),
                        type_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SyndromeTypes", t => t.type_ID)
                .Index(t => t.type_ID);
            
            CreateTable(
                "dbo.SyndromeTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Syndromes", "type_ID", "dbo.SyndromeTypes");
            DropForeignKey("dbo.VisitDates", "Pacient_ID", "dbo.Pacients");
            DropForeignKey("dbo.Reviews", "VisitDate_ID", "dbo.VisitDates");
            DropForeignKey("dbo.Researches", "VisitDate_ID", "dbo.VisitDates");
            DropForeignKey("dbo.Researches", "type_ID", "dbo.ResearchTypes");
            DropForeignKey("dbo.Neurostatus", "VisitDate_ID", "dbo.VisitDates");
            DropForeignKey("dbo.Diagnosis", "VisitDate_ID", "dbo.VisitDates");
            DropForeignKey("dbo.Debuts", "VisitDate_ID", "dbo.VisitDates");
            DropForeignKey("dbo.Assigments", "VisitDate_ID", "dbo.VisitDates");
            DropForeignKey("dbo.Anamnesis", "VisitDate_ID", "dbo.VisitDates");
            DropForeignKey("dbo.Diagnosis", "Pacient_ID", "dbo.Pacients");
            DropForeignKey("dbo.Neurostatus", "type_ID", "dbo.NeuroStatusTypes");
            DropForeignKey("dbo.Medicines", "type_ID", "dbo.MedicineTypes");
            DropForeignKey("dbo.Diagnosis", "type_ID", "dbo.DiagnosisTypes");
            DropForeignKey("dbo.Debuts", "type_ID", "dbo.DebutTypes");
            DropForeignKey("dbo.Assigments", "type_ID", "dbo.AssigmentTypes");
            DropForeignKey("dbo.Anamnesis", "type_ID", "dbo.AnamnesisEventTypes");
            DropIndex("dbo.Syndromes", new[] { "type_ID" });
            DropIndex("dbo.Reviews", new[] { "VisitDate_ID" });
            DropIndex("dbo.Researches", new[] { "VisitDate_ID" });
            DropIndex("dbo.Researches", new[] { "type_ID" });
            DropIndex("dbo.VisitDates", new[] { "Pacient_ID" });
            DropIndex("dbo.Neurostatus", new[] { "VisitDate_ID" });
            DropIndex("dbo.Neurostatus", new[] { "type_ID" });
            DropIndex("dbo.Medicines", new[] { "type_ID" });
            DropIndex("dbo.Diagnosis", new[] { "VisitDate_ID" });
            DropIndex("dbo.Diagnosis", new[] { "Pacient_ID" });
            DropIndex("dbo.Diagnosis", new[] { "type_ID" });
            DropIndex("dbo.Debuts", new[] { "VisitDate_ID" });
            DropIndex("dbo.Debuts", new[] { "type_ID" });
            DropIndex("dbo.Assigments", new[] { "VisitDate_ID" });
            DropIndex("dbo.Assigments", new[] { "type_ID" });
            DropIndex("dbo.Anamnesis", new[] { "VisitDate_ID" });
            DropIndex("dbo.Anamnesis", new[] { "type_ID" });
            DropTable("dbo.SyndromeTypes");
            DropTable("dbo.Syndromes");
            DropTable("dbo.Reviews");
            DropTable("dbo.ResearchTypes");
            DropTable("dbo.Researches");
            DropTable("dbo.VisitDates");
            DropTable("dbo.Pacients");
            DropTable("dbo.NeuroStatusTypes");
            DropTable("dbo.Neurostatus");
            DropTable("dbo.MedicineTypes");
            DropTable("dbo.Medicines");
            DropTable("dbo.DiagnosisTypes");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.DebutTypes");
            DropTable("dbo.Debuts");
            DropTable("dbo.AssigmentTypes");
            DropTable("dbo.Assigments");
            DropTable("dbo.AnamnesisEventTypes");
            DropTable("dbo.Anamnesis");
        }
    }
}
