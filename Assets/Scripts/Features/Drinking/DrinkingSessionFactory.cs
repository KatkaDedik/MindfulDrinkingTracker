using System;

public class DrinkingSessionFactory
{
    public DrinkingSessionModel Create(SessionConfig config)
    {
        var session = new DrinkingSessionModel
        {
            StartDateTime = DateTime.Now,
            MaxDrinks = 0,
            DesiredMaxPromilePeak = 0f
        };

        switch (config.Goal)
        {
            case DrinkingGoal.StayInControl:
                if (config.TargetPromile.HasValue)
                    session.DesiredMaxPromilePeak =
                        config.TargetPromile.Value;
                break;

            case DrinkingGoal.LimitDrinks:
                if (config.MaxDrinks.HasValue)
                    session.MaxDrinks =
                        config.MaxDrinks.Value;
                break;

            case DrinkingGoal.DriveTomorrow:
                if (config.SoberByHour.HasValue)
                {
                    var targetTime = DateTime.Now.Date
                        .AddHours(config.SoberByHour.Value);

                    if (targetTime < DateTime.Now)
                        targetTime = targetTime.AddDays(1);

                    float beta = 0.12f;
                    float hours =
                        (float)(targetTime - DateTime.Now).TotalHours;

                    session.DesiredMaxPromilePeak =
                        beta * hours;
                }
                break;
        }

        return session;
    }
}
