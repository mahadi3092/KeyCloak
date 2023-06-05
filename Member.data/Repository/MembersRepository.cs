using Member.data.Interface;
using Member.data.Model;


namespace Member.data.Repository
{
    public class MembersRepository : IMembers
    {
        List<Members> lisMembers = new List<Members>
        {
            new Members{MemberId=1, FirstName="Kirtesh", LastName="Shah", Address="Vadodara" },
            new Members{MemberId=2, FirstName="Nitya", LastName="Shah", Address="Vadodara" },
            new Members{MemberId=3, FirstName="Dilip", LastName="Shah", Address="Vadodara" },
            new Members{MemberId=4, FirstName="Atul", LastName="Shah", Address="Vadodara" },
            new Members{MemberId=5, FirstName="Swati", LastName="Shah", Address="Vadodara" },
            new Members{MemberId=6, FirstName="Rashmi", LastName="Shah", Address="Vadodara" },
        };
        public List<Members> GetAllMember()
        {
            return lisMembers;
        }

        public Members GetMember(int id)
        {
            return lisMembers.FirstOrDefault(x => x.MemberId == id);
        }
    }
}
