using CsvHelper.Configuration.Attributes;

namespace ParsingPrjc
{
    public class PartInfoClassMapOut1 : CsvHelper.Configuration.ClassMap<Part1>
    {
        public PartInfoClassMapOut1(bool requiredFields = true)
        {
            Map(m => m.Lastname).Name("Lastname");
            Map(m => m.Firstname).Name("Firstname");
            Map(m => m.Patronymic).Name("Patronymic");
            Map(m => m.Sex).Name("Sex");
            Map(m => m.Country).Name("Country");
            Map(m => m.Phone).Name("Phone");
            Map(m => m.Email).Name("Email");
        }
    }
    public class PartInfoClassMapOut2 : CsvHelper.Configuration.ClassMap<Part2>
    {
        public PartInfoClassMapOut2(bool requiredFields = true)
        {
            Map(m => m.Lastname).Name("Lastname");
            Map(m => m.Firstname).Name("Firstname");
            Map(m => m.Patronymic).Name("Patronymic");
            Map(m => m.Sex).Name("Sex");
            Map(m => m.Country).Name("Country");
            Map(m => m.Phone).Name("Phone");
            Map(m => m.Email).Name("Email");
        }
    }

    public class PartInfoClassMap1 : CsvHelper.Configuration.ClassMap<Part2>
    {
        public PartInfoClassMap1(bool requiredFields = true)
        {
            Map(m => m.Lastname).Name("last_name");
            Map(m => m.Firstname).Name("first_name");
            Map(m => m.Patronymic).Ignore(requiredFields);
            Map(m => m.Sex).Name("gender");
            Map(m => m.Country).Name("location");
            Map(m => m.Phone).Name("phone");
            Map(m => m.Email).Name("email");
        }
    }
    public class PartInfoClassMap2 : CsvHelper.Configuration.ClassMap<Part1>
    {
        public PartInfoClassMap2(bool requiredFields = true)
        {
            Map(m => m.Lastname).Name("Фамилия");
            Map(m => m.Firstname).Name("Имя");
            Map(m => m.Patronymic).Name("Отчество");
            Map(m => m.Sex).Ignore(true);
            Map(m => m.Country).Ignore(requiredFields);
            Map(m => m.Phone).Name("Telefon");
            Map(m => m.Email).Ignore(requiredFields);
        }
    }
    public class PartStubb
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
    }
    public class Part1
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
    
    public class Part2
    {
        [Name("last_name")]
        public string Lastname { get; set; }

        [Name("first_name")]
        public string Firstname { get; set; }

        public string Patronymic { get; set; }

        [Name("gender")]
        public string Sex { get; set; }

        [Name("location")]
        public string Country { get; set; }

        [Name("phone")]
        public string Phone { get; set; }

        [Name("email")]
        public string Email { get; set; }
    }
    
    public class Part3
    {
        [Name("last_name")]
        public string Lastname { get; set; }

        [Name("first_name")]
        public string Firstname { get; set; }

        public string Patronymic { get; set; }

        [Name("gender")]
        public string Sex { get; set; }

        [Name("location")]
        public string Country { get; set; }

        public string City { get; set; }

        [Name("phone")]
        public string Phone { get; set; }

        [Name("email")]
        public string Email { get; set; }
    }

    //public class CsvOut
    //{
    //    [Name("Index")]
    //    public uint Index { get; set; }
    //    [Name("Lastname")]
    //    public string Lastname { get; set; }
    //    [Name("Firstname")]
    //    public string Firstname { get; set; }
    //    [Name("Patronymic")]
    //    public string Patronymic { get; set; }
    //    [Name("Sex")]
    //    public string Sex { get; set; }
    //    [Name("Coutry")]
    //    public string Country { get; set; }
    //    [Name("Phone")]
    //    public string Phone { get; set; }
    //    [Name("Email")]
    //    public string Email { get; set; }
    //}
}
