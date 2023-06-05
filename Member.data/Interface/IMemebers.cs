using Member.data.Model;

namespace Member.data.Interface
{
    public interface IMembers
    {
        List<Members> GetAllMember();
        Members GetMember(int id);
    }
}
