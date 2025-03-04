namespace RPS.Services.Game.Services.UpdateUserRatingEventSender;

public interface IUpdateUserRatingEventSender
{
    Task SendEventAsync(long userId, long addRating, CancellationToken cancellationToken = default);
}