namespace MeSoftCase.Application.Features.Mediator.Results.AppUserResults
{
    public record AppUserListResult(
            List<AppUserListItemResult> Items
        );

    public record AppUserListItemResult(
            string Id,
            string UserName,
            string Email,
            DateTimeOffset? LockoutEnd,
            string Roles
        );
}
