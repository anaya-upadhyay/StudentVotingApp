using Microsoft.EntityFrameworkCore;

using VotingApp.Data;

namespace VotingApp.Models;

public static class SeedDb
{
    public static void InitializeDb(IServiceProvider serviceProvider)
    {
        using (var context = new VotingAppContext(serviceProvider.GetRequiredService<DbContextOptions<VotingAppContext>>()))
        {
            if (!context.Colleges.Any())
            {
                context.Colleges.AddRange(
                        new Campus
                        {
                            Name = "Aadikavi Bhanubhakta Campus",
                            Address = "Tanahun",
                            PhoneNumber = "065-560096",
                            Email = "info@aadikavicampus.edu.np",
                            Website = "www.aadikavicampus.edu.np"
                        },
                        new Campus
                        {
                            Name = "Balkumari Campus",
                            Address = "Chitwan",
                            PhoneNumber = "056-524700",
                            Email = "info@balkumaricollege.edu.np",
                            Website = "www.balkumaricollege.edu.np"
                        },
                        new Campus
                        {
                            Name = "Butwal Multiple Campus",
                            Address = "Butwal, Rupandehi",
                            PhoneNumber = "071-542834, 071-540134",
                            Email = "",
                            Website = "www.butwalmultiplecampus.edu.np"
                        },
                        new Campus
                        {
                            Name = "Dadeldhura Campus",
                            Address = "Dadeldhura, Mahakali",
                            PhoneNumber = "096-420189",
                            Email = "",
                            Website = ""
                        },
                        new Campus
                        {
                            Name = "Janajyoti Multiple Campus",
                            Address = "Lalbandi, Sarlahi",
                            PhoneNumber = "046-501436",
                            Email = "",
                            Website = ""
                        },
                        new Campus
                        {
                            Name = "Janamaitri Multiple Campus",
                            Address = "Kuleshwar, Kathmandu",
                            PhoneNumber = "01-4277202, 01-4826283",
                            Email = "",
                            Website = "www.janamaitri.edu.np"
                        },
                        new Campus
                        {
                            Name = "Kavre Multiple Campus",
                            Address = "Kavrepalanchok",
                            PhoneNumber = "11-661133",
                            Email = "info@kavrecampus.edu.np",
                            Website = "www.kavrecampus.edu.np"
                        },
                        new Campus
                        {
                            Name = "Mahendra Ratna Campus",
                            Address = "Tahachal, Kathmandu",
                            PhoneNumber = "01-4271709, 01-421728",
                            Email = "",
                            Website = ""
                        },
                        new Campus
                        {
                            Name = "Sanothimi Campus",
                            Address = "Sanothimi, Bhaktapur",
                            PhoneNumber = "",
                            Email = "",
                            Website = "sanothimicampus.edu.np"
                        },
                        new Campus
                        {
                            Name = "Sukuna Multiple Campus",
                            Address = "Indrapur, Morang",
                            PhoneNumber = "021-545617, 021-545717",
                            Email = "info@sukuna.edu.np",
                            Website = "www.sukuna.edu.np"
                        },
                        new Campus
                        {
                            Name = "Surkhet Campus",
                            Address = "Surkhet, Bheri",
                            PhoneNumber = "083-520298, 083-520125",
                            Email = "",
                            Website = ""
                        },
                        new Campus
                        {
                            Name = "Surya Narayan Satya Narayan Marwaita Multiple Campus",
                            Address = "Siraha",
                            PhoneNumber = "",
                            Email = "",
                            Website = ""
                        },
                        new Campus
                        {
                            Name = "Triyuga Janta Campus",
                            Address = "Gaighat",
                            PhoneNumber = "035-451435, 035-420304",
                            Email = "trijugacampus@gmail.com",
                            Website = ""
                        });

                context.SaveChanges();
            }

            if (!context.Parties.Any())
            {
                context.Parties.AddRange(
                    new Party
                    {
                        SystemName = "Nepali Congress",
                        DisplayName = "नेपाली काँग्रेस"
                    },
                    new Party
                    {
                        SystemName = "Nepal Communist Party (UML)",
                        DisplayName = "नेपाल कम्युनिष्ट पार्टी (एमाले)"
                    },
                    new Party
                    {
                        SystemName = "Nepal Communist Party (CPN)",
                        DisplayName = "नेपाल कम्युनिष्ट पार्टी (माअाेवादी)"
                    },
                    new Party
                    {
                        SystemName = "Independent",
                        DisplayName = "स्वतन्त्र"
                    });

                context.SaveChanges();
            }

            if (!context.Positions.Any())
            {
                context.Positions.AddRange(
                    new Position
                    {
                        SystemName = "President",
                        DisplayName = "अध्यक्ष"
                    },
                    new Position
                    {
                        SystemName = "Vice-President",
                        DisplayName = "उपाध्यक्ष"
                    },
                    new Position
                    {
                        SystemName = "Secretary",
                        DisplayName = "सचिव"
                    },
                    new Position
                    {
                        SystemName = "Treasurer",
                        DisplayName = "काेषाध्यक्ष"
                    });

                context.SaveChanges();
            }
        }
        
    }
}
