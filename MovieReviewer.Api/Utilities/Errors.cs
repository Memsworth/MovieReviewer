using FluentResults;

namespace MovieReviewer.Api.Utilities
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message)
        {
        }
    }


    public class DuplicateItemError : Error
    {
        public DuplicateItemError(string message) : base(message)
        {
        }
    }

    public class ItemIsDisabled : Error
    {
        public ItemIsDisabled(string message) : base(message)
        {
        }
    }
}
