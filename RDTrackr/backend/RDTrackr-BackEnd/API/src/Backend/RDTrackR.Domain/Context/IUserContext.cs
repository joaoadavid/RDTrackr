namespace RDTrackR.Domain.Context
{
    public interface IUserContext
    {
        long UserId { get; }
        string UserName { get; }

        string Role { get; }
    }

    //public interface IUserContext
    //{
    //    Guid UserIdentifier { get; }
    //    string UserName { get; }
    //    string Role { get; }
    //}

}



